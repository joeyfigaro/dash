using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoodadGroup : Doodad {
	
	public GameObject[] doodads;
	private Doodad[] scripts;
	private bool[,] terrainTrack;

	private int offsetX = 0;
	public bool fillToCameraEdge = true;
	private float rightBorderBackground;

	void Awake () {
		List<Doodad> list = new List<Doodad>();
		foreach(GameObject doodad in doodads) {
			list.Add(doodad.GetComponent<Doodad>());
		}
		scripts = list.ToArray();

		avoidRegistration = true;
	}

	public new void setSize(Vector3 newSize) {
		base.setSize(newSize);
		terrainTrack = new bool[Mathf.RoundToInt(size.x), Mathf.RoundToInt(size.z)];
		offsetX = 0;
	}

	void FixedUpdate() {
		rightBorderBackground = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, 150)
			).x;
		generateTerrainLoop();
	}

	private void generateTerrainLoop() {
		while((fillToCameraEdge || offsetX < size.x) && offsetX < rightBorderBackground + Mathf.RoundToInt(size.x / 2)) {
			Vector3 position = new Vector3(offsetX - Mathf.RoundToInt(size.x / 2), 0, 0);
			while(position.z < size.z) {
				if(!terrainTrack[offsetX % Mathf.RoundToInt(size.x), Mathf.RoundToInt(position.z)]) {
					Doodad doodad = getRandomDoodad();
					if(doodad != null) generateDoodad(doodad, position);
				}
				position.z++;
			}
			offsetX++;
		}
	}

	private void generateDoodad(Doodad doodad, Vector3 pos) {
		doodad.initialize();
		int availableHeight = 0;
		while((pos.z + availableHeight < size.z) && !terrainTrack[offsetX % Mathf.RoundToInt(size.x), Mathf.RoundToInt(pos.z + availableHeight)])
			availableHeight++;
		
		if(doodad.foreground && (pos.z > 0)) return;
		else if(doodad.underground && (pos.z > 0)) return;
		else if(pos.z == 0) return;
		
		float scale = doodad.getRandomScale();
		Vector2 doodadSize = doodad.getSizeAtScale(scale);
		if(doodadSize.y <= availableHeight) {
			instantiateA(doodad, pos, scale);

			for(int width = 0; width < size.x; width++) {
				for(int height = 0; height < doodadSize.y; height++) {
					terrainTrack[(offsetX + width) % Mathf.RoundToInt(size.x), Mathf.RoundToInt(pos.z + height)] = width < doodadSize.x;
				}
			}
		}
	}

	public GameObject instantiateA(Doodad doodad, Vector3 pos, float scale) {
		GameObject doodadClone = doodad.instantiateAt(pos, scale);
		doodadClone.transform.parent = transform;

		return doodadClone;
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
