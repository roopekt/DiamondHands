using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockGraph : MonoBehaviour
{
    public GameObject target;
    public float drawInterval = 1f;
    public int totalLines = 10;

    List<Vector2> linePoints = new();
    List<LineRenderer> lineSegments = new();

    public ObjectPool pool;
    public GameObject lineUpPrefab;
    public GameObject lineDnPrefab;

    private void Start()
    {
        BeginDrawingLine();
    }
    public void BeginDrawingLine()
    {
        if (target != null && lineCoroutine == null)
        {
            lineCoroutine = StartCoroutine(DrawLineCoroutine());
        }
    }
    public void StopDrawingLine()
    {
        if (lineCoroutine != null)
        {
            StopCoroutine(lineCoroutine);
        }
    }
    void DrawLineBetweenPoints(Vector3 start, Vector3 end)
    {
        GameObject pooledLine;
        if (end.y > start.y)
        {
            pooledLine = pool.PoolItem(lineUpPrefab);
        }
        else
        {
            pooledLine = pool.PoolItem(lineDnPrefab);
        }
        pooledLine.transform.position = Vector3.zero;
        if (pooledLine.TryGetComponent(out LineRenderer line))
        {
            line.SetPositions(new[] { start, end });
            lineSegments.Add(line);
            LimitCheck();
        }
    }
    void LimitCheck()
    {
        if (lineSegments.Count > totalLines)
        {
            pool.DeactivateObject(lineSegments[0].gameObject);
            lineSegments.RemoveAt(0);
        }
    }
    Coroutine lineCoroutine;
    IEnumerator DrawLineCoroutine()
    {
        linePoints.Clear();
        linePoints.Add(target.transform.position);
        lineSegments.Clear();

    drawLine:
        yield return new WaitForSeconds(drawInterval);
        Vector3 next = target.transform.position;
        DrawLineBetweenPoints(linePoints[linePoints.Count - 1], next);
        linePoints.Add(next);
        goto drawLine;
    }
}
