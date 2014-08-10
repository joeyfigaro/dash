using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGeneration : MonoBehaviour {

	public GameObject gate;
	public GameObject tile;
	public GameObject background;
	public GameObject terrain;
	public GameObject ground;
	public GameObject groundPlaceholder;

	private float rightBorderForeground;
	private float rightBorderBackground;
	private float leftBorderBackground;

	private float fieldDepth;
	private float fieldZMax;
	private float fieldStart;

	private float terrainY;
	private float lastGroundX;
	private float lastTileX;

	private int terrainTrackXMax = 10;
	private int terrainTrackYMax = 40;
	private bool[,] terrainTrack;
	private int terrainTrackOffset;

	private int groundTilesHeight;

	void Start () {
		fieldDepth = groundPlaceholder.renderer.bounds.size.z;
		fieldStart = groundPlaceholder.transform.position.z - (groundPlaceholder.renderer.bounds.size.z / 2);
		fieldZMax = groundPlaceholder.transform.position.z + (groundPlaceholder.renderer.bounds.size.z / 2);
		terrainY = groundPlaceholder.transform.position.y;
		Destroy(groundPlaceholder);

		calculateBorders();

		lastGroundX = leftBorderBackground - ground.renderer.bounds.size.x;
		lastTileX = leftBorderBackground - tile.renderer.bounds.size.x;

		groundTilesHeight = Mathf.FloorToInt(fieldDepth / ground.renderer.bounds.size.y - 1);
		terrainTrackYMax += groundTilesHeight;
		terrainTrack = new bool[terrainTrackXMax, terrainTrackYMax];

		generateTerrainLoop();
	}

	private void generateTerrainLoop() {
		while(lastGroundX <= (rightBorderBackground - ground.renderer.bounds.size.x)) {

			Vector3 position = new Vector3(lastGroundX, terrainY, fieldStart + (ground.renderer.bounds.size.y / 2f));
			for(int groundTile = 0; groundTile < groundTilesHeight; groundTile++) {
				if(groundTile < groundTilesHeight) generateGround(position);
				if(!terrainTrack[terrainTrackOffset, groundTile]) {
					Doodad doodad = Doodads.Instance.getRandomDoodad();
					generateDoodad(doodad, groundTile, ground.renderer.bounds.size.y * (groundTile + 1));
				}
				position.z += ground.renderer.bounds.size.y;
			}
			generateTile();

			terrainTrackOffset = (terrainTrackOffset + 1) % terrainTrackXMax;
			lastGroundX += ground.renderer.bounds.size.x;
		}
	}

	private void generateGround(Vector3 position) {
		GameObject groundClone = Instantiate(ground, position, Quaternion.Euler(-90, 0, 0)) as GameObject;
		groundClone.transform.parent = terrain.transform;
	}

	private void generateTile() {
		Vector3 position = new Vector3(lastTileX, terrainY - (tile.renderer.bounds.size.y / 2f), fieldStart);
		GameObject tileClone = Instantiate(tile, position, Quaternion.Euler(0, 0, 0)) as GameObject;
		tileClone.transform.parent = terrain.transform;
		lastTileX += tile.renderer.bounds.size.x;
	}

	public void generateGate() {
		GameObject gateClone = Instantiate(gate, new Vector3(
			rightBorderForeground + (2 * gate.renderer.bounds.size.x),
			terrainY + (gate.renderer.bounds.size.y / 2f),
			fieldStart
		), Quaternion.Euler(0, 0, 0)) as GameObject;
		gateClone.transform.parent = terrain.transform;
	}
	
	private void generateDoodad(Doodad doodad, int groundTile, float z) {
		if(doodad != null) {
			int availableHeight = 1;
			while((groundTile + availableHeight < groundTilesHeight) && !terrainTrack[terrainTrackOffset, groundTile + availableHeight])
				availableHeight++;

			if(doodad.inGround && groundTile != 0) return;

			// These break the rules.
			// TODO: terrain generation should start z at 0 and "return"
			// if an inground doodad was chosen and z != fieldstart.
			// Append to above statement.
			if(doodad.inGround) z = fieldStart;
			// TODO: terrain generation should start z at 0 and "return"
			// if a foreground doodad was chosen and z != 0.
			if(doodad.foreground) z = 0;

			GameObject doodadClone = doodad.instantiateAt(new Vector3(
				lastTileX,
				terrainY,
				z
				), doodad.gameObject.transform.rotation);

			float ratio = doodadClone.renderer.bounds.size.x / ground.renderer.bounds.size.x;
			float scale = Random.Range(doodad.minScale, doodad.maxScale);

			doodadClone.transform.localScale /= ratio / scale;

			float postScaleYOffset = 0;
			if(doodad.inGround) {
				postScaleYOffset = -1 * ((scale * ground.renderer.bounds.size.y) / 2);
			}

			doodadClone.transform.position += new Vector3((scale * ground.renderer.bounds.size.x) / 2, postScaleYOffset, 0);
			doodadClone.transform.parent = background.transform;

			if(groundTile > groundTilesHeight) doodadClone.renderer.sortingOrder = -3;

			Vector2 size = new Vector2(Mathf.Ceil(scale), 1);
			for(int width = 0; width < terrainTrackXMax; width++) {
				for(int height = 0; height < size.y; height++) {
					terrainTrack[(terrainTrackOffset + width) % terrainTrackXMax, groundTile + height] = width < size.x;
				}
			}
		}
	}

	private void calculateBorders() {
		rightBorderForeground = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, fieldStart)
			).x;
		leftBorderBackground = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, fieldZMax)
			).x;
		rightBorderBackground = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, fieldZMax)
			).x;
	}

	void Update() {
		calculateBorders();
	}

	void FixedUpdate() {
		generateTerrainLoop();
	}
}
