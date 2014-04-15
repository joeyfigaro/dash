using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {
	void Start() {
	}

	void Update () {
		if ((transform.position.x < (Camera.main.transform.position.x - 50)) && (renderer.IsVisibleFrom(Camera.main) == false))
		{
			Destroy(gameObject);
		}
	}
}
