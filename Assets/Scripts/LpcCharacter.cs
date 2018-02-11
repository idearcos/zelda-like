using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LpcCharacter : MonoBehaviour {

	public void UpdateSprites(int index) {
		foreach (LpcSprite lpcSprite in GetComponentsInChildren<LpcSprite>()) {
			lpcSprite.UpdateSprite (index);
		}
	}

	// Use this for initialization
	/*void Start () {
		
	}*/
	
	// Update is called once per frame
	/*void Update () {
		
	}*/
}
