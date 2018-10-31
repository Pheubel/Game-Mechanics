#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RayFieldColider))]
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

    private RayFieldColider _coliderController;

    void Awake()
    {
        _coliderController = GetComponent<RayFieldColider>();
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
                _velocityY -= (Gravity / 2) * Time.deltaTime;

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    _mobilityState = MobilityStates.InAir;
                }
                break;
            case MobilityStates.InAir:
                _velocityY -= Gravity * Time.deltaTime;
                break;
        }

        var deltaVelocity = new Vector2(_velocityX = Mathf.Clamp(_velocityX, -_maxVelocityX, _maxVelocityX),
        _velocityY = Mathf.Clamp(_velocityY, -_maxVelocityY, _maxVelocityY)) * Time.deltaTime;

        float correctX = 0, correctY = 0;


        if (_velocityX > 0)
        {
            RaycastHit2D[] rightHit = _coliderController.CastRight(deltaVelocity.x);

            if (rightHit.HitTag(Helper.Tags.WorldLayOut, out correctX))
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
            RaycastHit2D[] leftHit = _coliderController.CastLeft(Mathf.Abs(deltaVelocity.x));

            if (leftHit.HitTag(Helper.Tags.WorldLayOut, out correctX))
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
            RaycastHit2D[] upHit = _coliderController.CastUp(deltaVelocity.y);

            if (upHit.HitTag(Helper.Tags.WorldLayOut, out correctY))
            {
                _mobilityState = MobilityStates.InAir;
                _velocityY = 0;
            }

            else
            {
                correctY = deltaVelocity.y;
            }
        }

        else if (_velocityY == 0)
        {
            RaycastHit2D[] downHit = _coliderController.CastDown(Mathf.Abs(deltaVelocity.y));

            TopContactHandlerBase contact;

            if (!downHit.HitTag(Helper.Tags.WorldLayOut))
            {
                _mobilityState = MobilityStates.InAir;
            }

            else if ((contact = downHit.HitObject<TopContactHandlerBase>()) == null)
            {
                contact.ContactUp(this);
            }
        }

        else
        {
            RaycastHit2D[] downHit = _coliderController.CastDown(Mathf.Abs(deltaVelocity.y));

            if (_velocityY < 0 && downHit.HitTag(Helper.Tags.WorldLayOut, out correctY))
            {
                _mobilityState = MobilityStates.OnGround;
                _velocityY = 0;
                correctY *= -1;
            }
            else
            {
                correctY = deltaVelocity.y;
            }
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
