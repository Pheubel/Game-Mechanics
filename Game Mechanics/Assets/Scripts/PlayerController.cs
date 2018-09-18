using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float MovementSpeed;
    public float JumpPower;
    private new Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        

        rigidbody.velocity += 
            new Vector2(
                Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime * MovementSpeed, 
                Input.GetKeyDown(KeyCode.Space) ? JumpPower : 0
                );

    }

    public enum MobilityState
    {
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
