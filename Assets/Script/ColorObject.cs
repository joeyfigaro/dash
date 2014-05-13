using UnityEngine;
using System.Collections;

public class ColorObject : MonoBehaviour {
	public int color = 0;

	public Color realColor() {
		return ColorDefinitions.colors[color];
	}
}
