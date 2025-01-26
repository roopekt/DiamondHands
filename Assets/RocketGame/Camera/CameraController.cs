using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Serializable]
    public class FollowableObject {
        public Transform transform;
        public float padding = 2f;
    }

    public List<FollowableObject> targets;
    public List<Camera> cameras;
    public float MinSize = 5f;
    public float MaxSize = 80f;

    private class AABB {
        public Vector2 min;
        public Vector2 max;

        public static AABB NewAroundPoint(Vector2 center, float radius) {
            var delta = 0.5f * new Vector2(radius, radius);
            return new AABB {
                min = center - delta,
                max = center + delta
            };
        }

        public Vector2 GetCenter() {
            return (min + max) * 0.5f;
        }

        public Vector2 GetSize() {
            return max - min;
        }
    }

    private static AABB CombineAABBs(IEnumerable<AABB> AABBs, float maxHeight) {
        var minX = float.PositiveInfinity;
        var minY = float.PositiveInfinity;
        var maxX = float.NegativeInfinity;
        var maxY = float.NegativeInfinity;
        bool isFirstIteration = true;
        foreach (var aabb in AABBs) {
            float newMinX = Mathf.Min(minX, aabb.min.x);
            float newMinY = Mathf.Min(minY, aabb.min.y);
            float newMaxX = Mathf.Max(maxX, aabb.max.x);
            float newMaxY = Mathf.Max(maxY, aabb.max.y);

            if (!isFirstIteration) {
                float height = maxY - minY;
                float newHeight = newMaxY - newMinY;
                float t = Mathf.InverseLerp(height, newHeight, maxHeight);//if maxHeight is beyond the range, t=1

                newMinX = Mathf.Lerp(minX, newMinX, t);
                newMinY = Mathf.Lerp(minY, newMinY, t);
                newMaxX = Mathf.Lerp(maxX, newMaxX, t);
                newMaxY = Mathf.Lerp(maxY, newMaxY, t);
            }

            minX = newMinX;
            minY = newMinY;
            maxX = newMaxX;
            maxY = newMaxY;

            isFirstIteration = false;
        }

        return new AABB {
            min = new Vector2(minX, minY),
            max = new Vector2(maxX, maxY)
        };
    }

    void Update()
    {
        var aabb = CombineAABBs(targets.Select(target => AABB.NewAroundPoint(target.transform.position, target.padding)), MaxSize);

        var aspectRatio = cameras[0].aspect;//width / height
        var sizeRequiredForHeightMatch = aabb.GetSize().y * 0.5f;
        var sizeRequiredForWidthMatch = aabb.GetSize().x / aspectRatio * 0.5f;
        var requiredCameraSize = Mathf.Max(sizeRequiredForHeightMatch, sizeRequiredForWidthMatch);

        var cameraSize = Mathf.Max(MinSize, requiredCameraSize);
        var cameraPosition = (Vector3)aabb.GetCenter();
        cameraPosition.z = -10;

        foreach (var camera in cameras) {
            camera.orthographicSize = cameraSize;
            camera.transform.position = cameraPosition;
        }
    }
}
