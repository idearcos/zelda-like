using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float visionRadius;
	public float attackRadius;
	public float speed;

	GameObject player;

	Vector3 initialPosition;

	Animator anim;
	Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

		initialPosition = transform.position;

		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast (
			                   transform.position,
			                   player.transform.position - transform.position,
			                   visionRadius,
			                   1 << LayerMask.NameToLayer ("Default")
		                   );

		bool seesPlayer = (hit.collider != null) && (hit.collider.tag == "Player");

		Vector3 target = seesPlayer ? player.transform.position : initialPosition;

		float distance = Vector3.Distance (target, transform.position);
		Vector3 dir = (target - transform.position).normalized;

		anim.SetFloat ("movX", dir.x);
		anim.SetFloat ("movY", dir.y);

		if (seesPlayer && (distance < attackRadius)) {
			// Attack player
			//anim.Play ("Enemy Walk", -1, 0);
		} else {
			// Move towards player
			rb2d.MovePosition (transform.position + dir * speed * Time.deltaTime);

			//anim.speed = 1;
			anim.SetBool ("walking", true);
		}

		if (distance < 0.02f) {
			transform.position = target;
			anim.SetBool ("walking", false);
		}
	}
}
