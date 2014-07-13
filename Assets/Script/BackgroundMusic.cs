using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {
	public GameObject music;

	void Start () {
		GameObject G = GameObject.FindGameObjectWithTag("GameController");	

		if(!G) {
			Instantiate(music, Vector3.zero, Quaternion.identity);
		}
	}
}
