using UnityEngine;
using System.Collections;

public class Playlist : MonoBehaviour {
	public static Playlist Instance;

	public AudioClip rhythm;
	public AudioClip sunThrob;
	public AudioClip gatePassed;

	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of Playlist!");
		}
		Instance = this;
	}
}
