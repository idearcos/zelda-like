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

	// Attack stuff
	public GameObject rockPrefab;
	[Tooltip("Attack speed in seconds between consecutive attacks")]
	public float attackSpeed;
	bool attacking;
	Vector3 target;

	public int maxHP = 3;
	public int currentHP;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

		initialPosition = transform.position;

		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();

		currentHP = maxHP;
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

		// Wake up if asleep and just saw the player
		if (seesPlayer) anim.SetTrigger("sawPlayer");

		// If asleep, waking up or falling asleep, can't move
		if (!anim.GetBool ("awake")) {
			return;
		}

		target = seesPlayer ? player.transform.position : initialPosition;

		float distance = Vector3.Distance (target, transform.position);
		Vector3 dir = (target - transform.position).normalized;

		anim.SetFloat ("movX", dir.x);
		anim.SetFloat ("movY", dir.y);

		if (seesPlayer && (distance < attackRadius)) {
			// Attack player
			anim.SetBool ("attacking", true);
			if (!attacking) {
				StartCoroutine(Attack(attackSpeed));
			}
		} else {
			anim.SetBool ("attacking", false);

			// Move towards player
			rb2d.MovePosition (transform.position + dir * speed * Time.deltaTime);
		}

		if (!seesPlayer && distance < 0.02f) {
			transform.position = target;
			anim.SetBool ("awake", false);
		}
	}

	IEnumerator Attack(float seconds) {
		attacking = true;

		if (target != initialPosition && rockPrefab != null) {
			Instantiate (rockPrefab, transform.position, transform.rotation);

			yield return new WaitForSeconds (seconds);
		}

		attacking = false;
	}

	public void Attacked() {
		if (--currentHP <= 0) {
			Destroy (gameObject);
		}
	}

	void OnGUI() {
		Vector2 pos = Camera.main.WorldToScreenPoint (transform.position);

		GUI.Box (new Rect (pos.x - 20, Screen.height - pos.y - 80, 40, 24),
			currentHP + "/" + maxHP);
	}
}
