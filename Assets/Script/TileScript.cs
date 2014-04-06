using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {
	private ColorChangerScript colorChanger;

	void Start() {
		colorChanger = GetComponent<ColorChangerScript>();
		colorChanger.color = Random.Range(0, 3);
	}

	void Update () {
		if ((transform.position.x < Camera.main.transform.position.x) && (renderer.IsVisibleFrom(Camera.main) == false))
		{
			Destroy(gameObject);
		}
	}
}
