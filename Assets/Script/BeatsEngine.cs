﻿using UnityEngine;
using System.Collections;

public class BeatsEngine : MonoBehaviour {
	public static BeatsEngine Instance;

	public float bpm = 60f;
	public bool bpmIncreases = true;

	private int beats;

	public GameObject sun;

	private SunScript sunScript;
	private ArrayList tintables = new ArrayList();
	private TerrainGeneration terrainGeneration;
	private ColorDefinitions colorDefinitions;
	
	public int beatsPerSubthrob = 1;
	public int sunLeadBeats = 1;
	public int beatsPerGate = 4;
	private bool generateGates = false;

	private int nextColor = 0;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		sunScript = sun.GetComponent<SunScript>();
		terrainGeneration = GetComponent<TerrainGeneration>();
		StartCoroutine(generateBeats());
	}

	public void registerTintable(ColorObject tintable) {
		tintables.Add(tintable);
	}

	public void unregisterTintable(ColorObject tintable) {
		tintables.Remove(tintable);
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
			foreach(ColorObject tintMe in tintables) {
				tintMe.color = nextColor;
			}
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
		audioSource.PlayOneShot(originalClip);
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
