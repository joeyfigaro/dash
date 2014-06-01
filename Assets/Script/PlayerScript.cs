
using UnityEngine;
using System.Collections.Generic;

public class PlayerScript : ColorObject
{
	public int gatesPassed = 0;
	public float baseSpeed = 0;

	private ParticleSystem trail;

	void Start() {
		trail = GetComponent<ParticleSystem>();
	}

	void Update()
	{
		color = 0;
		if(Input.GetKey("1")) color = 1;
		if(Input.GetKey("2")) color = 2;
		if(Input.GetKey("3")) color = 3;

		if((color == 0) && (Input.touchCount > 0)) {

			Touch touch = Input.GetTouch(0);
			if(touch.position.y <= Screen.height * .33f) color = 3;
			else if(touch.position.y <= Screen.height * .66f) color = 2;
			else color = 1;
		}

		updateColor();
	}

	void FixedUpdate()
	{
		calculateSpeed();
	}

	private void updateColor() {
		trail.startColor = realColor();
	}

	private void calculateSpeed() {
		rigidbody2D.velocity = new Vector2(baseSpeed * (BeatsEngine.Instance.bpm / 60), rigidbody2D.velocity.y);
	}

	void OnDestroy() {
		transform.parent.gameObject.AddComponent<GameOverScript>();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		ColorObject enemyColorObject = collision.gameObject.GetComponent<ColorObject>();

		if(enemyColorObject != null) {
			if(color != enemyColorObject.color) {
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