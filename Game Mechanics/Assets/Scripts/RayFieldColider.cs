using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayFieldColider : MonoBehaviour {

    [SerializeField] private LayerMask WorldMask;

    [SerializeField] private Racycast2DField _rayFieldUp;
    [SerializeField] private Racycast2DField _rayFieldDown;
    [SerializeField] private Racycast2DField _rayFieldLeft;
    [SerializeField] private Racycast2DField _rayFieldRight;

    private void OnDrawGizmosSelected()
    {
        _rayFieldUp.DrawGizmo(transform.position);
        _rayFieldDown.DrawGizmo(transform.position);
        _rayFieldLeft.DrawGizmo(transform.position);
        _rayFieldRight.DrawGizmo(transform.position);
    }

    public RaycastHit2D[] CastUp(float distance)
    {
        return _rayFieldUp.Cast(transform.position, distance, WorldMask);
    }

    public RaycastHit2D[] CastDown(float distance)
    {
        return _rayFieldDown.Cast(transform.position,distance,WorldMask);
    }

    public RaycastHit2D[] CastLeft(float distance)
    {
        return _rayFieldLeft.Cast(transform.position, distance, WorldMask);
    }

    public RaycastHit2D[] CastRight(float distance)
    {
        return _rayFieldRight.Cast(transform.position, distance, WorldMask);
    }
}
