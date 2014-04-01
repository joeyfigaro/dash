using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour {
	private ColorChangerScript colorChanger;

	void Start () {
		colorChanger = GetComponent<ColorChangerScript>();
		colorChanger.colorNew = Random.Range(0, 3);
	}

	void Update () {
		if (renderer.IsVisibleFrom(Camera.main) == false)
		{
			Destroy(gameObject);
		}
	}
}
