using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Racycast2DField
{
    public float Width => _width;
    [SerializeField] private float _width;
    public Vector2 Offset => _offset;
    [SerializeField] private Vector2 _offset;
    public Directions Direction => _direction;
    [SerializeField] private Directions _direction;
    [SerializeField] private Densities _density = Densities.Low;


    public void DrawGizmo(Vector2 origin, float distance)
    {
        Gizmos.color = Color.yellow;

        float step = (_width * 2) / ((int)_density - 1);

        switch (_direction)
        {
            case Directions.Up:
                // draw base line
                Gizmos.DrawLine(
                    new Vector3(origin.x + _offset.x - _width, origin.y + _offset.y),
                    new Vector3(origin.x + _offset.x + _width, origin.y + _offset.y));

                Gizmos.color = Color.red;

                for (int i = 0; i < (int)_density; i++)
                {
                    Gizmos.DrawLine(
                        new Vector2(
                            origin.x + Offset.x - _width + (step * i),
                            origin.y + Offset.y),
                        (Vector2.up * distance) + new Vector2(
                            origin.x + Offset.x - _width + (step * i),
                            origin.y + Offset.y));
                }
                break;

            case Directions.Down:
                // draw base line
                Gizmos.DrawLine(
                    new Vector3(origin.x + _offset.x - _width, origin.y + _offset.y),
                    new Vector3(origin.x + _offset.x + _width, origin.y + _offset.y));

                Gizmos.color = Color.red;

                for (int i = 0; i < (int)_density; i++)
                {
                    Gizmos.DrawLine(
                        new Vector2(
                            origin.x + Offset.x - _width + (step * i),
                            origin.y + Offset.y),
                        (Vector2.down * distance) + new Vector2(
                            origin.x + Offset.x - _width + (step * i),
                            origin.y + Offset.y));
                }
                break;

            case Directions.Left:
                // draw base line
                Gizmos.DrawLine(
                    new Vector3(origin.x + _offset.x, origin.y + _offset.y - _width),
                    new Vector3(origin.x + _offset.x, origin.y + _offset.y + _width));

                Gizmos.color = Color.red;

                for (int i = 0; i < (int)_density; i++)
                {
                    Gizmos.DrawLine(
                        new Vector2(
                            origin.x + Offset.x,
                            origin.y + Offset.y - _width + (step * i)),
                        (Vector2.left * distance) + new Vector2(
                            origin.x + Offset.x,
                            origin.y + Offset.y - _width + (step * i)));
                }
                break;

            case Directions.Right:
                // draw base line
                Gizmos.DrawLine(
                    new Vector3(origin.x + _offset.x, origin.y + _offset.y - _width),
                    new Vector3(origin.x + _offset.x, origin.y + _offset.y + _width));

                Gizmos.color = Color.red;

                for (int i = 0; i < (int)_density; i++)
                {
                    Gizmos.DrawLine(
                        new Vector2(
                            origin.x + Offset.x,
                            origin.y + Offset.y - _width + (step * i)),
                        (Vector2.right * distance) + new Vector2(
                            origin.x + Offset.x,
                            origin.y + Offset.y - _width + (step * i)));
                }
                break;

            default: break;
        }
    }

    /// <summary> Casts a set of rays from the origin in the set direction and return hit objects.</summary>
    public RaycastHit2D[] Cast(Vector2 origin, float distance, int layerMask)
    {
        RaycastHit2D[] hits = new RaycastHit2D[(int)_density];

        float step = (_width * 2) / ((int)_density - 1);

        switch (_direction)
        {
            case Directions.Up:
                for (int i = 0; i < (int)_density; i++)
                {
                    hits[i] = Physics2D.Raycast(
                        new Vector2(
                            origin.x + Offset.x - _width + (step * i),
                            origin.y + Offset.y),
                        Vector2.up,
                        distance,
                        layerMask);
                }
                break;
            case Directions.Down:
                for (int i = 0; i < (int)_density; i++)
                {
                    hits[i] = Physics2D.Raycast(
                        new Vector2(
                            origin.x + Offset.x - _width + (step * i),
                            origin.y + Offset.y),
                        Vector2.down,
                        distance,
                        layerMask);
                }
                break;
            case Directions.Left:
                for (int i = 0; i < (int)_density; i++)
                {
                    hits[i] = Physics2D.Raycast(
                        new Vector2(
                            origin.x + Offset.x,
                            origin.y + Offset.y - _width + (step * i)),
                        Vector2.left,
                        distance,
                        layerMask);
                }
                break;
            case Directions.Right:
                for (int i = 0; i < (int)_density; i++)
                {
                    hits[i] = Physics2D.Raycast(
                        new Vector2(
                            origin.x + Offset.x,
                            origin.y + Offset.y - _width + (step * i)),
                        Vector2.right,
                        distance,
                        layerMask);
                }
                break;

            default:
                throw new System.Exception("Raycast field has no valid direction.");
        }

        Debug.Log(hits);

        return hits;
    }

    /// <summary> Casts a set of rays from the origin in the set direction.</summary>
    public bool CastHitTag(Vector2 origin, float distance, string tag, int layerMask)
    {
        float step = (_width * 2) / ((int)_density - 1);

        switch (_direction)
        {
            case Directions.Up:
                for (int i = 0; i < (int)_density; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(
                        new Vector2(
                            origin.x + Offset.x - _width + (step * i),
                            origin.y + Offset.y),
                        Vector2.up,
                        distance,
                        layerMask);

                    if (hit.collider == null && !hit.collider.CompareTag(tag))
                        continue;

                    return true;
                }
                break;
            case Directions.Down:
                for (int i = 0; i < (int)_density; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(
                        new Vector2(
                            origin.x + Offset.x - _width + (step * i),
                            origin.y + Offset.y),
                        Vector2.down,
                        distance,
                        layerMask);

                    if (hit.collider == null && !hit.collider.CompareTag(tag))
                        continue;

                    return true;
                }
                break;
            case Directions.Left:
                for (int i = 0; i < (int)_density; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(
                        new Vector2(
                            origin.x + Offset.x,
                            origin.y + Offset.y - _width + (step * i)),
                        Vector2.left,
                        distance,
                        layerMask);

                    if (hit.collider == null && !hit.collider.CompareTag(tag))
                        continue;

                    return true;
                }
                break;
            case Directions.Right:
                for (int i = 0; i < (int)_density; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(
                        new Vector2(
                            origin.x + Offset.x,
                            origin.y + Offset.y - _width + (step * i)),
                        Vector2.right,
                        distance,
                        layerMask);

                    if (hit.collider == null && !hit.collider.CompareTag(tag))
                        continue;

                    return true;
                }
                break;

            default:
                throw new System.Exception("Raycast field has no valid direction.");
        }

        return false;
    }


    /// <summary> Casts a set of rays from the origin in the set direction.</summary>
    public bool CastHasHit(Vector2 origin, float distance, out float correction, int layerMask)
    {
        bool hasHit = false;

        correction = 0;

        float step = (_width * 2) / ((int)_density - 1);

        switch (_direction)
        {
            case Directions.Up:
                for (int i = 0; i < (int)_density; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(
                        new Vector2(
                            origin.x + Offset.x - _width + (step * i),
                            origin.y + Offset.y),
                        Vector2.up,
                        distance,
                        layerMask);

                    if (hit.collider == null)
                        continue;

                    correction = Mathf.Max(correction, hit.distance);
                    hasHit = true;
                }
                break;
            case Directions.Down:
                for (int i = 0; i < (int)_density; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(
                        new Vector2(
                            origin.x + Offset.x - _width + (step * i),
                            origin.y + Offset.y),
                        Vector2.down,
                        distance,
                        layerMask);

                    if (hit.collider == null)
                        continue;

                    correction = Mathf.Max(correction, hit.distance);
                    hasHit = true;
                }
                break;
            case Directions.Left:
                for (int i = 0; i < (int)_density; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(
                        new Vector2(
                            origin.x + Offset.x,
                            origin.y + Offset.y - _width + (step * i)),
                        Vector2.left,
                        distance,
                        layerMask);

                    if (hit.collider == null)
                        continue;

                    correction = Mathf.Max(correction, hit.distance);
                    hasHit = true;
                }
                break;
            case Directions.Right:
                for (int i = 0; i < (int)_density; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(
                        new Vector2(
                            origin.x + Offset.x,
                            origin.y + Offset.y - _width + (step * i)),
                        Vector2.right,
                        distance,
                        layerMask);

                    if (hit.collider == null)
                        continue;

                    correction = Mathf.Max(correction, hit.distance);
                    hasHit = true;
                }
                break;

            default:
                throw new System.Exception("Raycast field has no valid direction.");
        }

        return hasHit;
    }

    /// <summary> Casts a set of rays from the origin in the set direction.</summary>
    public bool CastHasHitTag(Vector2 origin, float distance, string tag, out float correction, int layerMask)
    {
        bool hasHit = false;

        correction = 0;

        float step = (_width * 2) / ((int)_density - 1);

        switch (_direction)
        {
            case Directions.Up:
                for (int i = 0; i < (int)_density; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(
                        new Vector2(
                            origin.x + Offset.x - _width + (step * i),
                            origin.y + Offset.y),
                        Vector2.up,
                        distance,
                        layerMask);

                    if (hit.collider == null && !hit.collider.CompareTag(tag))
                        continue;

                    correction = Mathf.Max(correction, hit.distance);
                    hasHit = true;
                }
                break;
            case Directions.Down:
                for (int i = 0; i < (int)_density; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(
                        new Vector2(
                            origin.x + Offset.x - _width + (step * i),
                            origin.y + Offset.y),
                        Vector2.down,
                        distance,
                        layerMask);

                    if (hit.collider == null && !hit.collider.CompareTag(tag))
                        continue;

                    correction = Mathf.Max(correction, hit.distance);
                    hasHit = true;
                }
                break;
            case Directions.Left:
                for (int i = 0; i < (int)_density; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(
                        new Vector2(
                            origin.x + Offset.x,
                            origin.y + Offset.y - _width + (step * i)),
                        Vector2.left,
                        distance,
                        layerMask);

                    if (hit.collider == null && !hit.collider.CompareTag(tag))
                        continue;

                    correction = Mathf.Max(correction, hit.distance);
                    hasHit = true;
                }
                break;
            case Directions.Right:
                for (int i = 0; i < (int)_density; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(
                        new Vector2(
                            origin.x + Offset.x,
                            origin.y + Offset.y - _width + (step * i)),
                        Vector2.right,
                        distance,
                        layerMask);

                    if (hit.collider == null && !hit.collider.CompareTag(tag))
                        continue;

                    correction = Mathf.Max(correction, hit.distance);
                    hasHit = true;
                }
                break;

            default:
                throw new System.Exception("Raycast field has no valid direction.");
        }

        return hasHit;
    }

    public enum Directions
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum Densities
    {
        Low = 3,

    }
}

public static class RaycastHit2dArrayExtention
{
    public static RaycastHit2D ReturnClosestHit(this RaycastHit2D[] hits)
    {
        RaycastHit2D closestHit = hits[0];

        for (int i = 1; i < hits.Length; i++)
        {
            if (hits[i].distance < closestHit.distance)
                closestHit = hits[i];
        }

        return closestHit;
    }

    public static bool HitTag(this RaycastHit2D[] hits, string tagName)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider == null)
                continue;
            if (hits[i].collider.CompareTag(tagName))
                return true;
        }
        return false;
    }

    public static bool HitTag(this RaycastHit2D[] hits, string tagName, out float correction)
    {
        bool hasHit = false;
        correction = 0;

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider == null)
                continue;

            if (hits[i].collider.CompareTag(tagName))
            {
                correction = Mathf.Max(correction, hits[i].distance);
                hasHit = true;

                Debug.Log($"Hit {tagName}");
            }
        }

        return hasHit;
    }
}
