using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoodadGroup : Doodad {
	
	public GameObject[] doodads;
	private Doodad[] scripts;

	void Awake () {
		List<Doodad> list = new List<Doodad>();
		foreach(GameObject doodad in doodads) {
			list.Add(doodad.GetComponent<Doodad>());
		}
		scripts = list.ToArray();
	}

	public Doodad getRandomDoodad() {
		Doodad generatedDoodad = null;
		foreach(Doodad script in scripts) {
			if(((generatedDoodad == null) ||
			    (script.rarity >= generatedDoodad.rarity)) &&
			   (Random.Range(0, script.rarity * 4) == 0)) {
				generatedDoodad = script;
			}
			if(script.force) generatedDoodad = script;
		}
		return generatedDoodad;
	}
}
