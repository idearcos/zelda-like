using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEditor;

public class LpcSprite : MonoBehaviour {

	public Texture2D spriteSheet;
	SpriteRenderer spriteRenderer;
	Sprite[] sprites;

	public void UpdateSprite(int index) {
		spriteRenderer.sprite = sprites [index];
	}

	void Awake() {
		Assert.IsNotNull (spriteSheet);
	}

	// Use this for initialization
	void Start () {
		// Remove the directories before "Resources", as well as the extension of the file
		string spriteSheetPath = AssetDatabase.GetAssetPath (spriteSheet);
		sprites = Resources.LoadAll<Sprite> (spriteSheetPath.Substring(17, spriteSheetPath.Length - 21));

		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/
}
