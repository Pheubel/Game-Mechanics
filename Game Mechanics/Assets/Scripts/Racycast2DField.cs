using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Racycast2DField
{
    public float Distance => _distance;
    [SerializeField] private float _distance;
    public float Width => _width;
    [SerializeField] private float _width;
    public int RayCount => _rayCount;
    [Range(1, 10)]
    [SerializeField] private int _rayCount;
    public Vector2 Offset => _offset;
    [SerializeField] private Vector2 _offset;
    public Vector2 Direction => _direction;
    [SerializeField] private Vector2 _direction;

    public void DrawGizmo(Vector2 origin)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector2(-_offset.y*(_width/2),_offset.x *(_width/2)) + origin, new Vector2(_offset.y * (_width / 2), -_offset.x * (_width / 2)) + origin );


        Gizmos.color = Color.red;
        for (int i = 0; i < _rayCount; i++)
        {
            
        }
    }
}
