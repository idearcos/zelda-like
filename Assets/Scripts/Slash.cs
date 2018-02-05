using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour {

	[Tooltip("Seconds to wait before destroying the object")]
	public float secondsUntilDestruction;

	[HideInInspector]
	public Vector2 mov;

	public float speed;
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (mov.x, mov.y, 0) * speed * Time.deltaTime;
	}

	IEnumerator OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Object") {
			yield return new WaitForSeconds (secondsUntilDestruction);
			Destroy (gameObject);
		} else if (col.tag != "Player" && col.tag != "Attack") {
			Destroy (gameObject);
		}
	}
}
