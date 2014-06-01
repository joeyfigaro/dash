using UnityEngine;
using System.Collections;

public class TileScript : ColorObject {
	void Update () {
		destroyIfOffscreen();
	}
}
