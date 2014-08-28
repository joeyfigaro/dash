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

	private int groundTilesHeight;

	private DoodadGroup backgroundDoodad;

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

		backgroundDoodad = background.GetComponent<DoodadGroup>();
		backgroundDoodad.setSize(new Vector3(groundPlaceholder.renderer.bounds.size.x, 0, fieldZMax));
		generateTerrainLoop();
	}

	private void generateTerrainLoop() {
		while(lastGroundX <= (rightBorderBackground - ground.renderer.bounds.size.x)) {
			Vector3 position = new Vector3(lastGroundX, terrainY, fieldStart + (ground.renderer.bounds.size.y / 2f));
			for(int groundTile = 0; groundTile < groundTilesHeight; groundTile++) {
				generateGround(position);
				position.z += ground.renderer.bounds.size.y;
			}
			generateTile();
			lastGroundX += ground.renderer.bounds.size.x;
		}
	}

	private void generateGround(Vector3 position) {
		GameObject groundClone = ObjectPool.instance.GetObjectForType(ground.name, false);
		groundClone.transform.position = position;
		groundClone.transform.rotation = Quaternion.Euler(-90, 0, 0);
		groundClone.name = ground.name;
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
