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

	public float timeBetweenDoodads = 2f;

	private int doodadIndex = 0;
	private float rightBorder;
//	private float leftBorder;
	private float rightBorderBackground;
	private float leftBorderBackground;
	private float terrainY;

	private float fieldDepth;
	private float fieldZMax;
	private float fieldStart;

	private GateScript gateScript;

	void Start () {
		gateScript = gate.GetComponent<GateScript>();

		fieldDepth = groundPlaceholder.renderer.bounds.size.z;
		fieldStart = groundPlaceholder.transform.position.z - (groundPlaceholder.renderer.bounds.size.z / 2);
		fieldZMax = groundPlaceholder.transform.position.z + (groundPlaceholder.renderer.bounds.size.z / 2);
		terrainY = groundPlaceholder.transform.position.y;
		Destroy(groundPlaceholder);

		calculateBorders();

		lastGroundX = leftBorderBackground - ground.renderer.bounds.size.x;
		lastTileX = leftBorderBackground - tile.renderer.bounds.size.x;

		generateTerrain();
		StartCoroutine(generateDoodad());
	}

	public float getTerrainY() {
		return terrainY;
	}

	private float lastGroundX;
	private float lastTileX;
	private void generateTerrain() {
		Vector3 position;
		GameObject tileClone;

		while((lastGroundX - rightBorderBackground <= ground.renderer.bounds.size.x) || (
			lastTileX - rightBorderBackground <= tile.renderer.bounds.size.x)) {
			if(lastGroundX - rightBorderBackground <= ground.renderer.bounds.size.x) {
				position = new Vector3(lastGroundX, terrainY, fieldStart + (ground.renderer.bounds.size.y / 2f));
				GameObject groundClone;
				for(int groundTile = 0; groundTile <= (fieldDepth / ground.renderer.bounds.size.y - 1); groundTile++) {
					groundClone = Instantiate(ground, position, Quaternion.Euler(-90, 0, 0)) as GameObject;
					groundClone.transform.parent = terrain.transform;
					position.z += ground.renderer.bounds.size.y;
				}
				lastGroundX += ground.renderer.bounds.size.x;
			}

			if(lastTileX - rightBorderBackground <= tile.renderer.bounds.size.x) {
				position = new Vector3(lastTileX, terrainY, fieldStart);
				tileClone = Instantiate(tile, position, Quaternion.Euler(0, 0, 0)) as GameObject;
				tileClone.transform.parent = terrain.transform;
				lastTileX += tile.renderer.bounds.size.x;
			}
		}
	}

	public void generateGate() {
		GameObject gateClone = Instantiate(gate, new Vector3(
			rightBorder + (2 * gate.renderer.bounds.size.x),
			terrainY + ((gate.renderer.bounds.size.y  + tile.renderer.bounds.size.y) / 2f),
			fieldStart
		), Quaternion.Euler(0, 0, 0)) as GameObject;
		gateClone.transform.parent = terrain.transform;
	}
	
	IEnumerator generateDoodad() {
		for( float timer = timeBetweenDoodads; timer >= 0; timer -= Time.deltaTime)
			yield return 0;

		DoodadScript doodadScript = Doodads.Instance.randomDoodadScript();

		float z = 0;
		if(!doodadScript.foreground) z = Doodads.Instance.getRandomOffset(doodadIndex);

		float yOffset = doodadScript.gameObject.renderer.bounds.size.y / 2f;
		if(doodadScript.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.center.y != 0) yOffset = 0;


		GameObject doodadClone = Instantiate(doodadScript.gameObject, new Vector3(
			calculateRightBorderAtZ(z) + doodadScript.gameObject.renderer.bounds.size.x, 
			terrainY + yOffset,
			z
			), doodadScript.gameObject.transform.rotation) as GameObject;
		doodadClone.transform.parent = background.transform;

		StartCoroutine(generateDoodad());
	}

	private float calculateRightBorderAtZ(float z) {
		return Camera.main.ViewportToWorldPoint(
				new Vector3(1, 0, z)
			).x;
	}

	private void calculateBorders() {
		rightBorder = Camera.main.ViewportToWorldPoint(
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
		generateTerrain();
	}
}
