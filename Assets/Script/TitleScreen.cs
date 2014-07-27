using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {
	public GameObject player;
	public Texture2D dashLogoTexture;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.y = player.transform.position.y;

		transform.position = pos;
		if((Input.inputString.Length > 0) || (Input.touchCount > 0)) Application.LoadLevel("Level");
	}

	void OnGUI () {
		GUI.Label (new Rect (Screen.width / 2 - 256, Screen.height / 2 - 256, 512, 256), dashLogoTexture);
	}
}
