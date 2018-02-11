using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEditor;

public class LpcSprite : MonoBehaviour {

	public Texture2D walkSpriteSheet;
	public Texture2D slashSpriteSheet;
	SpriteRenderer spriteRenderer;
	Sprite[] walkSprites;
	Sprite[] slashSprites;

	public void SetWalkSprite(int index) {
		if (walkSprites != null && index < walkSprites.Length) {
			spriteRenderer.sprite = walkSprites [index];
		} else {
			spriteRenderer.sprite = null;
		}
	}

	public void SetSlashSprite(int index) {
		if (slashSprites != null && index < slashSprites.Length) {
			spriteRenderer.sprite = slashSprites [index];
		}
	}

	void Awake() {
		//Assert.IsNotNull (walkSpriteSheet);
		//Assert.IsNotNull (slashSpriteSheet);
	}

	// Use this for initialization
	void Start () {
		// Remove the directories before "Resources", as well as the extension of the file
		if (walkSpriteSheet != null) {
			string spriteSheetPath = AssetDatabase.GetAssetPath (walkSpriteSheet);
			walkSprites = Resources.LoadAll<Sprite> (spriteSheetPath.Substring(17, spriteSheetPath.Length - 21));
		}

		if (slashSpriteSheet != null) {
			string spriteSheetPath = AssetDatabase.GetAssetPath (slashSpriteSheet);
			slashSprites = Resources.LoadAll<Sprite> (spriteSheetPath.Substring (17, spriteSheetPath.Length - 21));
		}

		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/
}
