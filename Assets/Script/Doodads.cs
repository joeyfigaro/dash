using UnityEngine;
using System.Collections.Generic;

public class Doodads : MonoBehaviour {
	public static Doodads Instance;
	
	public float minDoodadRange = 10f;
	public float maxDoodadRange = 40f;
	public GameObject[] doodads;
	private DoodadScript[] scripts;

	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of Doodads!");
		}
		Instance = this;

		List<DoodadScript> list = new List<DoodadScript>();
		foreach(GameObject doodad in doodads) {
			list.Add(doodad.GetComponent<DoodadScript>());
		}
		scripts = list.ToArray();
	}

	public DoodadScript randomDoodadScript() {
		DoodadScript generatedDoodadScript = null;
		foreach(DoodadScript script in scripts) {
//			if(generatedDoodadScript == null) {
			if(((generatedDoodadScript == null) ||
				(script.rarity >= generatedDoodadScript.rarity)) &&
				(Random.Range(0, script.rarity) == 0)) {
				generatedDoodadScript = script;
			}
			if(script.force) generatedDoodadScript = script;
		}
		return generatedDoodadScript;
	}
}
