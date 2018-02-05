using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour {

	public float speed = 4f;

	Animator anim;
	Rigidbody2D rb2d;
	Vector2 mov;

	CircleCollider2D attackColider;

	public GameObject initialMap;
	public GameObject slashPrefab;

	bool preventMovement = false;

	Aura aura;

	void Awake() {
		Assert.IsNotNull (initialMap);
		Assert.IsNotNull (slashPrefab);
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();

		attackColider = transform.GetChild (0).GetComponent<CircleCollider2D> ();
		attackColider.enabled = false;

		Camera.main.GetComponent<MainCamera> ().SetBound (initialMap);

		aura = transform.GetChild (1).GetComponent<Aura> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMove ();
		UpdateAnim ();
		UpdateAttack ();
		UpdateChargedAttack ();

		if (preventMovement) {
			mov = Vector2.zero;
		}
	}

	void FixedUpdate() {
		rb2d.MovePosition (rb2d.position + mov * speed * Time.deltaTime);
	}

	void UpdateMove() {
		mov = new Vector2 (
			Input.GetAxisRaw("Horizontal"),
			Input.GetAxisRaw("Vertical")
		);
	}

	void UpdateAnim() {
		if (mov != Vector2.zero) {
			anim.SetFloat ("movX", mov.x);
			anim.SetFloat ("movY", mov.y);
			anim.SetBool ("walking", true);
		} else {
			anim.SetBool ("walking", false);
		}
	}

	void UpdateAttack() {
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo (0);
		bool attacking = stateInfo.IsName ("Player Attack");

		if (Input.GetKeyDown ("space") && !attacking) {
			anim.SetTrigger ("attacking");
		}

		if (mov != Vector2.zero) {
			attackColider.offset = new Vector2 (mov.x / 2, mov.y / 2);
		}

		if (attacking) {
			float playbackTime = stateInfo.normalizedTime;
			if (playbackTime > 0.33 && playbackTime < 0.66) {
				attackColider.enabled = true;
			} else {
				attackColider.enabled = false;
			}

		}
	}

	void UpdateChargedAttack() {
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo (0);
		bool chargingAttack = stateInfo.IsName ("Player Charge");

		if (Input.GetKeyDown (KeyCode.LeftAlt)) {
			anim.SetTrigger ("charging");
			aura.AuraStart ();
		} else if (Input.GetKeyUp (KeyCode.LeftAlt)) {
			anim.SetTrigger ("attacking");

			if (aura.IsCharged ()) {
				float angle = Mathf.Atan2 (anim.GetFloat ("movY"), anim.GetFloat ("movX")) * Mathf.Rad2Deg;

				GameObject slashObj = Instantiate(slashPrefab, transform.position,
					Quaternion.AngleAxis(angle, Vector3.forward));

				Slash slash = slashObj.GetComponent<Slash> ();
				slash.mov.x = anim.GetFloat ("movX");
				slash.mov.y = anim.GetFloat ("movY");
			}

			aura.AuraStop ();

			StartCoroutine(EnableMovementAfter(0.4f));
		}
			
		if (chargingAttack) {
			preventMovement = true;
		}
	}

	IEnumerator EnableMovementAfter(float seconds) {
		yield return new WaitForSeconds (seconds);
		preventMovement = false;
	}
}
