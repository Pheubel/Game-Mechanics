using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float MovementAcceleration;
    public float JumpPower;
    public float Gravity;
    [SerializeField] private MobilityStates _mobilityState;
    [Range(0, 1), Tooltip("Determines how much energy is conserved when walking along the ground.")]
    public float Friction;

    [SerializeField] private float _velocityX;
    [SerializeField] private float _velocityY;

    [SerializeField] private float _maxVelocityX;
    [SerializeField] private float _maxVelocityY;

    [Tooltip("casts ray downward to check ground!")]
    [SerializeField] private float _downwardsRayDistance;

    [SerializeField] private LayerMask WorldMask;
    
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(Vector2.down * _downwardsRayDistance));
    }

    // Update is called once per frame
    void Update()
    {
        /* x speed aanpassen
         * state checken voor y spul
         * x & y clampen naar max waardes
         * translate naar nieuwe positie
         * corrigeer naar juite positie
         */



        _velocityX += (MovementAcceleration * Input.GetAxis("Horizontal"));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _downwardsRayDistance,WorldMask);

        float correctX = 0, correctY = 0;

        if(hit.collider != null)
        {
            if (_velocityY < 1 && hit.collider.CompareTag("WorldLayout"))
            {
                _mobilityState = MobilityStates.OnGround;
                _velocityY = 0;
            }
        }
        else
        {
            _mobilityState = MobilityStates.InAir;
        }

        switch (_mobilityState)
        {
            case MobilityStates.OnGround:
                _velocityX *= Friction;
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

        _velocityX = Mathf.Clamp(_velocityX, -_maxVelocityX, _maxVelocityX);
        _velocityY = Mathf.Clamp(_velocityY, -_maxVelocityY, _maxVelocityY);

        transform.position += new Vector3(_velocityX + correctX, +correctY) * Time.deltaTime;
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
