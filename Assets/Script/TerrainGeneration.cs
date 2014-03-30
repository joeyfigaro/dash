using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGeneration : MonoBehaviour {



	// Use this for initialization
	void Start () {
		mGates.Add(Resources.Load("gateRed"));
		mGates.Add(Resources.Load("gateBlue"));
		mGates.Add(Resources.Load("gateGreen"));
	}

	public List<Object> mGates = new List<Object>();

	private void generateGate() {
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, 0)
			).x;
		
		
		int gateOffset = Random.Range(0, mGates.Count);
		
		Vector3 position = Camera.main.transform.position;
		position.x = rightBorder;
		position.y = -1.12f;
		position.z = 1;
		
		Instantiate(mGates[gateOffset], position, Camera.main.transform.rotation);
	}

	// Update is called once per frame
	void Update () {
		if(Random.Range (0, 100) >= 95) generateGate();
	}
}
