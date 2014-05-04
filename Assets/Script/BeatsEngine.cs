using UnityEngine;
using System.Collections;

public class BeatsEngine : MonoBehaviour {
	public static BeatsEngine Instance;

	public float bpm = 60f;
	public bool bpmIncreases = true;

	private int beats;

	public GameObject sun;

	private SunScript sunScript;
	private TerrainGeneration terrainGeneration;
	private ColorDefinitions colorDefinitions;
	
	public int beatsPerSubthrob = 1;
	public int sunLeadBeats = 1;
	public int beatsPerGate = 4;
	private bool generateGates = false;

	private int nextColor = 0;

	// Use this for initialization
	void Start () {
		sunScript = sun.GetComponent<SunScript>();
		terrainGeneration = GetComponent<TerrainGeneration>();
		StartCoroutine(generateBeats());
	}

	void OnGUI(){
		GUI.Label(new Rect(0, 0, Screen.width,Screen.height), bpm.ToString());
	}

	public void gateDestroyed() {
		if(bpmIncreases) bpm++;
		MakeSound(Playlist.Instance.gatePassed);
	}

	IEnumerator generateBeats() {
		for( float timer = 0; timer <= 60f / bpm; timer += Time.deltaTime)
			yield return 0;

		beats++;

		if(generateGates && ((beats + sunLeadBeats) % beatsPerGate == 0)) {
			nextColor += Random.Range (1, ColorDefinitions.colors.Length - 1);
			if(nextColor >= ColorDefinitions.colors.Length) nextColor -= ColorDefinitions.colors.Length - 1;

			sunScript.color = nextColor;
		}
		if(generateGates && (beats % beatsPerGate == 0)) {
			terrainGeneration.generateGate(nextColor);
		}

		if(beats % beatsPerSubthrob == 0) {
			MakeSound(Playlist.Instance.sunThrob);
			sunScript.throb();
		}

		foreach(BeatAspect aspect in Playlist.Instance.beatAspects) {
			if((beats >= aspect.beatStart) && (beats % aspect.beatsPer == 0)) MakeSound(aspect.clip);
		}

		StartCoroutine(generateBeats());
	}

	public void engage() {
		generateGates = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void MakeSound(AudioClip originalClip)
	{
		// As it is not 3D audio clip, position doesn't matter.
		AudioSource.PlayClipAtPoint(originalClip, transform.position);
	}

	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of BeatsEngine!");
		}
		Instance = this;
	}
}
