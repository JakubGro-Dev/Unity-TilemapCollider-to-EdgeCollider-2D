using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CompositeCollider2D))]
public class UglyEdgeCollider : MonoBehaviour
{
    void Awake()
    {
        TilemapCollider2D tile = GetComponent<TilemapCollider2D>();
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        CompositeCollider2D compo = GetComponent<CompositeCollider2D>();

        if (rigid == null)
        {
            rigid = gameObject.AddComponent<Rigidbody2D>();
        }

        if (compo == null)
        {
            compo = gameObject.AddComponent<CompositeCollider2D>();
            compo.isTrigger = true;
        }

        if (tile == null)
        {
            tile = gameObject.AddComponent<TilemapCollider2D>();
            tile.compositeOperation = Collider2D.CompositeOperation.Merge;
        }

        if (compo.pathCount <= 0)
        {
            Debug.Log("Error. CompositeCollider didnt generated any path.");
            UnityEditor.EditorApplication.isPlaying = false;
            return;
        }

        int pointCount = compo.GetPathPointCount(0);
        Vector2[] points = new Vector2[pointCount + 1];
        compo.GetPath(0, points);
        points[pointCount] = points[0];
        EdgeCollider2D edge = gameObject.AddComponent<EdgeCollider2D>();
        edge.points = points;
        Destroy(tile);
        Destroy(compo);
        Destroy(rigid);
    }
}