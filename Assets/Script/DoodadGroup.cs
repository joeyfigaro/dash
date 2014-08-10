using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoodadGroup : DoodadScript {
	
	public GameObject[] doodads;
	private DoodadScript[] scripts;

	void Awake () {
		List<DoodadScript> list = new List<DoodadScript>();
		foreach(GameObject doodad in doodads) {
			list.Add(doodad.GetComponent<DoodadScript>());
		}
		scripts = list.ToArray();
	}

	public DoodadScript randomDoodadScript() {
		DoodadScript generatedDoodadScript = null;
		foreach(DoodadScript script in scripts) {
			if(((generatedDoodadScript == null) ||
			    (script.rarity >= generatedDoodadScript.rarity)) &&
			   (Random.Range(0, script.rarity * 4) == 0)) {
				generatedDoodadScript = script;
			}
			if(script.force) generatedDoodadScript = script;
		}
		return generatedDoodadScript;
	}
}
