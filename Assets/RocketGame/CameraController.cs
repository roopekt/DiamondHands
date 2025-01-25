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
    public Camera camera;
    public float MinSize = 5f;

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

    private static AABB CombineAABBs(IEnumerable<AABB> AABBs) {
        var minX = float.PositiveInfinity;
        var minY = float.PositiveInfinity;
        var maxX = float.NegativeInfinity;
        var maxY = float.NegativeInfinity;
        foreach (var aabb in AABBs) {
            minX = Mathf.Min(minX, aabb.min.x);
            minY = Mathf.Min(minY, aabb.min.y);
            maxX = Mathf.Max(maxX, aabb.max.x);
            maxY = Mathf.Max(maxY, aabb.max.y);
        }

        return new AABB {
            min = new Vector2(minX, minY),
            max = new Vector2(maxX, maxY)
        };
    }

    void Update()
    {
        var aabb = CombineAABBs(targets.Select(target => AABB.NewAroundPoint(target.transform.position, target.padding)));

        var aspectRatio = camera.aspect;//width / height
        var sizeRequiredForHeightMatch = aabb.GetSize().y * 0.5f;
        var sizeRequiredForWidthMatch = aabb.GetSize().x / aspectRatio * 0.5f;
        var requiredCameraSize = Mathf.Max(sizeRequiredForHeightMatch, sizeRequiredForWidthMatch);

        camera.orthographicSize = Mathf.Max(MinSize, requiredCameraSize);
        var cameraPosition = (Vector3)aabb.GetCenter();
        cameraPosition.z = -10;
        camera.transform.position = cameraPosition;
    }
}
