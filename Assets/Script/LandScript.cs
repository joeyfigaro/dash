using UnityEngine;
using System.Collections;

public class LandScript : ColorObject {

	// Use this for initialization
	void Start () {
		gameObject.renderer.castShadows = true;
		gameObject.renderer.receiveShadows = true;
	}
	
	// Update is called once per frame
	void Update () {
		poolIfOffscreen();
	}
}
