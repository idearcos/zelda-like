using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
	public float smoothTime = 0.5f;

	Transform target;
	float tLX, tLY, bRX, bRY;

	Vector2 velocity;

	// Use this for initialization
	void Awake () {
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Start() {
		Screen.SetResolution (800, 800, true);
	}
	
	// Update is called once per frame
	void Update () {
		if (!Screen.fullScreen || Camera.main.aspect != 1) {
			Screen.SetResolution (800, 800, true);
		}

		if (Input.GetKey ("escape"))
			Application.Quit ();

		float posX = Mathf.Round (Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothTime) * 100) / 100;
		float posY = Mathf.Round (Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smoothTime) * 100) / 100;

		transform.position = new Vector3 (
			Mathf.Clamp(posX, tLX, bRX),
			Mathf.Clamp(posY, bRY, tLY),
			transform.position.z
		);
	}

	public void SetBound(GameObject map) {
		Tiled2Unity.TiledMap config = map.GetComponent<Tiled2Unity.TiledMap> ();
		float cameraSize = Camera.main.orthographicSize;

		tLX = map.transform.position.x + cameraSize;
		tLY = map.transform.position.y - cameraSize;
		bRX = map.transform.position.x + config.NumTilesWide - cameraSize;
		bRY = map.transform.position.y - config.NumTilesHigh + cameraSize;

		FastMove ();
	}

	public void FastMove() {
		transform.position = new Vector3 (
			target.position.x,
			target.position.y,
			transform.position.z
		);
	}
}
