using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour {

	public float secondsBeforePlay;

	Animator anim;
	Coroutine manager;
	bool charged = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	public void AuraStart() {
		manager = StartCoroutine (Manager ());
		anim.Play ("aura_idle");
	}

	public void AuraStop() {
		StopCoroutine (manager);
		anim.Play ("aura_idle");
		charged = false;
	}
	
	public IEnumerator Manager() {
		yield return new WaitForSeconds (secondsBeforePlay);
		anim.Play ("aura_play");
		charged = true;
	}

	public bool IsCharged() {
		return charged;
	}
}
