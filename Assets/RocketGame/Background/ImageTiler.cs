using System.Collections.Generic;
using UnityEngine;

public class ImageTiler : MonoBehaviour
{
    public GameObject tilePrefab;
    public Vector2 offset = Vector2.zero;
    public bool horizontalOnly = false;
    public int tileCountLimit = 10_000;

    private Vector2 tileSize;
    private Camera camera;
    private Dictionary<Vector2Int, GameObject> tiles;

    public Vector2 GetWorldPosition(Vector2Int scaledPosition) {
        return new Vector2(scaledPosition.x * tileSize.x, scaledPosition.y * tileSize.y) - offset + tileSize/2;
    }

    void Start()
    {
        tileSize = (Vector2)tilePrefab.GetComponent<SpriteRenderer>().bounds.size;
        camera = Camera.main;
        tiles = new Dictionary<Vector2Int, GameObject>();
    }

    void Update()
    {
        var cameraRect = GetCameraRect();
        var cameraMinCorner = GetCameraMinCorner(cameraRect);
        var cameraMaxCorner = GetCameraMaxCorner(cameraRect);
        var coordinateSet = new HashSet<Vector2Int>();
        for (int x = cameraMinCorner.x; x <= cameraMaxCorner.x; x++) {
            for (int y = cameraMinCorner.y; y <= cameraMaxCorner.y; y++) {
                if (horizontalOnly && y != 0) continue;

                var coordinate = new Vector2Int(x, y);
                coordinateSet.Add(coordinate);

                if (!tiles.ContainsKey(coordinate)) {
                    AddTile(coordinate);
                }
            }
        }

        var keys = new List<Vector2Int>(tiles.Keys);
        foreach (var key in keys) {
            if (!coordinateSet.Contains(key)) {
                RemoveTile(key);
            }
        }
    }

    void AddTile(Vector2Int position) {
        if (tiles.Count >= tileCountLimit) return;
        
        var tile = Instantiate(tilePrefab, GetWorldPosition(position), Quaternion.identity, transform);
        tiles.Add(position, tile);
    }

    void RemoveTile(Vector2Int position) {
        Destroy(tiles[position]);
        tiles.Remove(position);
    }

    Rect GetCameraRect() {
        Vector2 cameraSize = new Vector2(camera.aspect, 1) * camera.orthographicSize * 2;
        Vector2 cameraPosition = (Vector2)camera.transform.position + offset;
        return new Rect(
            cameraPosition - cameraSize * 0.5f,
            cameraSize
        );
    }

    Vector2Int GetCameraMinCorner(Rect cameraRect) {
        Vector2 scaled = new Vector2(cameraRect.min.x / tileSize.x, cameraRect.min.y / tileSize.y);
        return new Vector2Int(Mathf.FloorToInt(scaled.x), Mathf.FloorToInt(scaled.y));
    }

    Vector2Int GetCameraMaxCorner(Rect cameraRect) {
        Vector2 scaled = new Vector2(cameraRect.max.x / tileSize.x, cameraRect.max.y / tileSize.y);
        return new Vector2Int(Mathf.FloorToInt(scaled.x), Mathf.FloorToInt(scaled.y));
    }
}
