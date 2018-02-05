using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

	public string destroyState;

	public float secondsUntilDisable;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	IEnumerator OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Attack") {
			anim.Play (destroyState);
			yield return new WaitForSeconds (secondsUntilDisable);

			foreach (Collider2D c in GetComponents<Collider2D>()) {
				c.enabled = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo (0);

		if (stateInfo.IsName (destroyState) && stateInfo.normalizedTime >= 1) {
			Destroy (gameObject);
		}
	}
}
