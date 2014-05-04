using UnityEngine;
using System.Collections;

public class Playlist : MonoBehaviour {
	public static Playlist Instance;

	public AudioClip sunThrob;
	public AudioClip gatePassed;

	[SerializeField]
	public BeatAspect[] beatAspects;

	public string[] nickname;
	public AudioClip[] beat;
	public int[] beatStart;
	public int[] beatDelay;

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
