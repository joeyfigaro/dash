
using UnityEngine;
using System.Collections.Generic;

public class PlayerScript : ColorObject
{
	public int gatesPassed = 0;
	public float baseSpeed = 0;

	private TrailRenderer trail;

	void Start() {
		trail = GetComponent<TrailRenderer>();
	}

	void Update()
	{
		if ((transform.position.y < (-10)) && (renderer.IsVisibleFrom(Camera.main) == false))
		{
			Destroy(gameObject);
		}

		setColor(0);
		if(Input.GetKey("1")) setColor(1);
		if(Input.GetKey("2")) setColor(2);
		if(Input.GetKey("3")) setColor(3);

		if(getColor().Equals(ColorDefinitions.colors[0]) && (Input.touchCount > 0)) {

			Touch touch = Input.GetTouch(0);
			if(touch.position.y <= Screen.height * .33f) setColor(3);
			else if(touch.position.y <= Screen.height * .66f) setColor(2);
			else setColor(1);
		}

		updateColor();
	}

	void FixedUpdate()
	{
		calculateSpeed();
	}

	private void updateColor() {
		trail.Colors[Color[0]] = getColor();
	}

	private void calculateSpeed() {
		if(BeatsEngine.Instance != null) rigidbody2D.velocity = new Vector2(baseSpeed * (BeatsEngine.Instance.bpm / 60), rigidbody2D.velocity.y);
	}

	void OnDestroy() {
		transform.parent.gameObject.AddComponent<GameOverScript>();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		ColorObject enemyColorObject = collision.gameObject.GetComponent<ColorObject>();

		if(enemyColorObject != null) {
			if(!getColor().Equals(enemyColorObject.getColor ())) {
				if(collision.gameObject.transform.rotation.eulerAngles.z != 0) {
					BoxCollider2D collider = collision.gameObject.GetComponent<BoxCollider2D>();
					collider.isTrigger = false;
				}// else Destroy(gameObject);
			}
			else {
				gatesPassed++;
				BeatsEngine.Instance.gateDestroyed();
			}
			//Destroy(collision.gameObject);
		}
	}
}