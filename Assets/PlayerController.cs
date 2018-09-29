using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float maxSpeed = 10f;
	bool faceRight = true;

	Animator animator;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	public float jumpForce = 700f;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (grounded && Input.GetKey(KeyCode.Space)) {
			animator.SetBool ("Ground", false);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		}
	}

	void FixedUpdate ()
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		animator.SetBool ("Ground", grounded);

		animator.SetFloat ("vSpeed", GetComponent<Rigidbody2D> ().velocity.y);

		float move = Input.GetAxis ("Horizontal");
		GetComponent<Rigidbody2D> ().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		animator.SetFloat ("Speed", Mathf.Abs (move));

		Debug.Log ("Ground:" + animator.GetBool("Ground"));

		if (move > 0f && !faceRight) {
			Flip();
		} else if (move < 0f && faceRight) {
			Flip();
		}
	}

	void Flip() {
		faceRight = !faceRight;
		Vector3 flipper = transform.localScale;
		flipper.x *= -1;
		transform.localScale = flipper;
	}

}
