using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LpcCharacter : MonoBehaviour {

	public void UpdateSprites(int index) {
		foreach (LpcSprite lpcSprite in GetComponentsInChildren<LpcSprite>()) {
			lpcSprite.SetWalkSprite (index);
		}
	}

	public void SetSlashSprites(int index) {
		foreach (LpcSprite lpcSprite in GetComponentsInChildren<LpcSprite>()) {
			lpcSprite.SetSlashSprite (index);
		}
	}

	// Use this for initialization
	/*void Start () {
		
	}*/
	
	// Update is called once per frame
	/*void Update () {
		
	}*/
}
