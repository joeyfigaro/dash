using UnityEngine;
using System.Collections;

public class SunScript : ColorObject {

	private Vector3 throbStart;
	private Vector3 throbEnd;
	private Vector3 throbGoal;

	private ArrayList tintables = new ArrayList();

	public float throbFactor = 1.5f;

	private Quaternion newAngle = Quaternion.Euler(0, 0, 0);
	
	private int nextColor = 0;

	private Vector3 startPos;
	public Vector3 destPos;
	public GameObject terrain;
	private TerrainGeneration terrainGen;

	private State state = State.set;

	enum State {
		setting, set, rising, throbbing
	}

	void Awake() {
		terrainGen = terrain.GetComponent<TerrainGeneration>();
	}

	void Start() {
		startPos = transform.localPosition;

		throbStart = transform.localScale;
		throbEnd = new Vector3(throbStart.x * throbFactor, throbStart.y * throbFactor, throbStart.z);
		throbGoal = throbStart;

		Update ();
		rise ();
	}

	public void registerTintable(ColorObject tintable) {
		tintables.Add(tintable);
	}
	
	public void unregisterTintable(ColorObject tintable) {
		tintables.Remove(tintable);
	}
	
	public void randomizeColor()
	{
		nextColor += Random.Range (1, ColorDefinitions.colors.Length - 1);
		if (nextColor >= ColorDefinitions.colors.Length)
			nextColor -= ColorDefinitions.colors.Length - 1;
		setColor(nextColor);
	}

	private void updateTintables() {
		foreach (ColorObject tintMe in tintables) {
			tintMe.setColor(getColor());
		}
	}

	void rise() {
		destPos = startPos;
		setColor(new Color(0, 0, 0));
		state = State.rising;
	}
	
	void Update () {
		switch(state) {
		case State.set:
			transform.position = transform.position + new Vector3(0, terrainGen.getTerrainY() - (transform.renderer.bounds.size.y / 2), 0);
			break;
		case State.throbbing:
			transform.localScale = Vector3.Lerp(transform.localScale, throbGoal, Time.deltaTime * 20);
			transform.rotation = Quaternion.Lerp(transform.rotation, newAngle, Time.deltaTime * 5);

			break;
		case State.rising:
			// Due to loading lag, this causes a large initial jump
//			transform.localPosition = Vector3.Lerp(transform.localPosition, destPos, Time.deltaTime);
//			if(destPos.y - transform.position.y < .001) state = State.throbbing;

			transform.localPosition = Vector3.MoveTowards(transform.localPosition, destPos, Time.deltaTime * 50);
			if(transform.localPosition.Equals(destPos)) state = State.throbbing;

			setColor(Color.Lerp(gameObject.renderer.material.color, ColorDefinitions.colors[nextColor], Time.deltaTime));

			break;
		}
		changeRenderedColor();
		updateTintables();
		Camera.main.backgroundColor = gameObject.renderer.material.color / 1.25f;
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
