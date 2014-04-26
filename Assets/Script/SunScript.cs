using UnityEngine;
using System.Collections;

public class SunScript : ColorObject {

	private Vector3 throbStart;
	private Vector3 throbEnd;
	private Vector3 throbGoal;

	public float throbFactor = 1.5f;

	private Quaternion newAngle = Quaternion.Euler(0, 0, 0);

	void Start() {
		throbStart = transform.localScale;
		throbEnd = new Vector3(throbStart.x * throbFactor, throbStart.y * throbFactor, throbStart.z);

		throbGoal = throbStart;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = Vector3.Lerp(transform.localScale, throbGoal, Time.deltaTime * 20);
		gameObject.renderer.material.color = realColor();//Color.Lerp(gameObject.renderer.material.color, realColor(), Time.deltaTime * 10);

		transform.rotation = Quaternion.Lerp(transform.rotation, newAngle, Time.deltaTime * 5);
	}

	public void throb() {
		StartCoroutine(throbber());
	}

	IEnumerator throbber() {
		newAngle = Quaternion.Euler(0, 0, Random.Range(0, 90));

		for(float time = 0f; time <= .25; time += Time.deltaTime)
			yield return 0;

		throbGoal = throbEnd;

		for(float time = 0f; time <= .25; time += Time.deltaTime)
			yield return 0;

		throbGoal = throbStart;
	}
}
