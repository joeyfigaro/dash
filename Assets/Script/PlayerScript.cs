
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
		if(Input.GetKey("1")) color = 1;
		if(Input.GetKey("2")) color = 2;
		if(Input.GetKey("3")) color = 3;
		
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