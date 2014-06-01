using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if((Input.inputString.Length > 0) || (Input.touchCount > 0)) Application.LoadLevel("Level");
	}
}
