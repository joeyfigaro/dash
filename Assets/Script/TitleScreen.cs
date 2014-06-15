using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {
	public GameObject player;


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
}
