using UnityEngine;
using System.Collections;

public class BeatEngine : MonoBehaviour {
	private float bpm = 60f;
	private int beats;

	public GameObject sun;

	private SunScript sunScript;
	private TerrainGeneration terrainGeneration;
	private ColorDefinitions colorDefinitions;

	private int beatsPerGate = 4;
	private bool generateGates = false;

	// Use this for initialization
	void Start () {
		sunScript = sun.GetComponent<SunScript>();
		terrainGeneration = GetComponent<TerrainGeneration>();
		StartCoroutine(music());
	}

	IEnumerator music() {
		for( float timer = 0; timer <= 60f / bpm; timer += Time.deltaTime)
			yield return 0;

		beats++;

		if(generateGates && (beats % beatsPerGate == 0)) {
			int color = Random.Range (1, ColorDefinitions.colors.Length);
			terrainGeneration.generateGate(color);
			sunScript.color = color;
		}

		sunScript.throb();
		StartCoroutine(music());
	}

	public void engage() {
		generateGates = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
