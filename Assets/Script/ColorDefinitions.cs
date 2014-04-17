using UnityEngine;
using System.Collections;

public class ColorDefinitions : MonoBehaviour {
	public static ColorDefinitions Instance;

	public static Color[] colors = {
		new Color(255, 255, 255),
		new Color(255, 0, 0),
		new Color(0, 255, 0),
		new Color(0, 0, 255)
	};

	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of ColorDefinitions!");
		}
		Instance = this;
	}
}