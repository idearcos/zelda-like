using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : MonoBehaviour {

	public float speed = 4f;

	Vector2 mov;
	Animator animator;
	Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		CaptureMovement ();
		UpdateAnimation ();

	}

	void FixedUpdate() {
		rb2d.MovePosition (rb2d.position + mov * speed * Time.deltaTime);
	}

	void CaptureMovement() {
		mov = new Vector2 (
			Input.GetAxisRaw("Horizontal"),
			Input.GetAxisRaw("Vertical")
		);
	}

	void UpdateAnimation() {
		if (mov != Vector2.zero) {
			animator.SetFloat ("movX", mov.x);
			animator.SetFloat ("movY", mov.y);
			animator.SetBool ("walking", true);
		} else {
			animator.SetBool ("walking", false);
		}
	}
}
