using UnityEngine;
using System.Collections;

public class LandScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.renderer.castShadows = true;
		gameObject.renderer.receiveShadows = true;
	}
	
	// Update is called once per frame
	void Update () {
		if ((transform.position.x < (Camera.main.transform.position.x - 50)) && (renderer.IsVisibleFrom(Camera.main) == false))
		{
			Destroy(gameObject);
		}
	}
}
