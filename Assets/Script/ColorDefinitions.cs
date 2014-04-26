using UnityEngine;
using System.Collections;

public class ColorDefinitions : MonoBehaviour {
	public static ColorDefinitions Instance;

	public static Color[] colors = {
		new Color(1, 1, 1),
		new Color(1, 0, 0),
		new Color(0, 1, 0),
		new Color(0, 0, 1)
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