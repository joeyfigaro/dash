﻿using UnityEngine;
using System.Collections;

public class CityScript : ColorObject {

	// Use this for initialization
	void Start () {
		registerWithTintSource();
	}
	
	// Update is called once per frame
	void Update () {
		changeRenderedColor();
	}
}
