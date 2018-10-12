#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float MovementAcceleration;
    public float JumpPower;
    public float Gravity;
    [SerializeField] private MobilityStates _mobilityState;
    [Range(0, 1), Tooltip("Determines how much energy is conserved when walking along the ground.")]
    public float Friction;


    [SerializeField] private float _velocityX;
    [SerializeField] private float _velocityY;

    [SerializeField] private float _maxVelocityX;
    [SerializeField] private float _maxVelocityY;

    [SerializeField] private LayerMask WorldMask;

    [SerializeField] private Racycast2DField _rayFieldUp;
    [SerializeField] private Racycast2DField _rayFieldDown;
    [SerializeField] private Racycast2DField _rayFieldLeft;
    [SerializeField] private Racycast2DField _rayFieldRight;

    private void OnDrawGizmosSelected()
    {
        if (_velocityX > 0)
            _rayFieldRight.DrawGizmo(transform.position, _velocityX * Time.deltaTime);
        else if (_velocityX < 0)
            _rayFieldLeft.DrawGizmo(transform.position, Mathf.Abs(_velocityX) * Time.deltaTime);

        if (_velocityY > 0)
            _rayFieldUp.DrawGizmo(transform.position, _velocityY * Time.deltaTime);
        else if (_velocityY < 0)
            _rayFieldDown.DrawGizmo(transform.position, Mathf.Abs(_velocityY) * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.position;

        _velocityX += (MovementAcceleration * Input.GetAxis("Horizontal"));
        _velocityX *= Friction;

        switch (_mobilityState)
        {
            case MobilityStates.OnGround:

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _mobilityState = MobilityStates.InAirSlowDescent;
                    _velocityY = JumpPower;
                }
                break;

            case MobilityStates.InAirSlowDescent:
                _velocityY -= Gravity / 2;

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    _mobilityState = MobilityStates.InAir;
                }
                break;
            case MobilityStates.InAir:
                _velocityY -= Gravity;
                break;
        }

        var deltaVelocity = new Vector2(_velocityX = Mathf.Clamp(_velocityX, -_maxVelocityX, _maxVelocityX),
        _velocityY = Mathf.Clamp(_velocityY, -_maxVelocityY, _maxVelocityY)) * Time.deltaTime;

        float correctX = 0, correctY = 0;

        if (_velocityX > 0)
        {
            RaycastHit2D[] rightHit = _rayFieldRight.Cast(currentPosition, deltaVelocity.x, WorldMask);

            if (rightHit.HitTag(Helper.WorldLayOut, out correctX))
            {
                _velocityX = 0;
            }

            else
            {
                correctX = deltaVelocity.x;
            }
        }

        else if (_velocityX < 0)
        {
            RaycastHit2D[] leftHit = _rayFieldLeft.Cast(currentPosition, Mathf.Abs(deltaVelocity.x), WorldMask);

            if (leftHit.HitTag(Helper.WorldLayOut, out correctX))
            {
                _velocityX = 0;
                correctX *= -1;
            }

            else
            {
                correctX = deltaVelocity.x;
            }
        }

        if (_velocityY > 0)
        {
            RaycastHit2D[] upHit = _rayFieldUp.Cast(currentPosition, deltaVelocity.y, WorldMask);

            if (upHit.HitTag(Helper.WorldLayOut, out correctY))
            {
                _mobilityState = MobilityStates.InAir;
                _velocityY = 0;
            }

            else
            {
                correctY = deltaVelocity.y;
            }
        }

        RaycastHit2D[] downHit = _rayFieldDown.Cast(currentPosition, Mathf.Abs(deltaVelocity.y), WorldMask);

        if (_velocityY < 0 && downHit.HitTag(Helper.WorldLayOut, out correctY))
        {
            _mobilityState = MobilityStates.OnGround;
            _velocityY = 0;
            correctY *= -1;
        }

        else
        {
            if (_velocityY == 0)
                _mobilityState = MobilityStates.InAir;
            correctY = deltaVelocity.y;
        }


        transform.position = currentPosition + new Vector2(correctX, correctY);
    }

    public enum MobilityStates
    {
        InAirSlowDescent,
        InAir,
        OnGround,
        OnWall
    }

    public enum ContactState
    {
        Vurnable,
        Damaged,
        Invincible
    }

}
