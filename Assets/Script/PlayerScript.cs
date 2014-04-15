
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public int gatesPassed = 0;
	public float baseSpeed = 0;

	private int color = 0;
	private ParticleSystem trail;

	void Start() {
		color = 0;
		trail = GetComponent<ParticleSystem>();
	}

	void Update()
	{
		if(Input.GetKey("1")) color = 0;
		if(Input.GetKey("2")) color = 1;
		if(Input.GetKey("3")) color = 2;
		
		updateColor();
	}

	void FixedUpdate()
	{
		calculateSpeed();
	}

	private void updateColor() {
		switch(color) {
		case 0:
			trail.startColor = new Color(255, 0, 0);
			break;
		case 1:
			trail.startColor = new Color(0, 255, 0);
			break;
		case 2:
			trail.startColor = new Color(0, 0, 255);
			break;
		}
	}

	void OnGUI(){
		GUI.Label(new Rect(0,0,Screen.width,Screen.height), rigidbody2D.velocity.x.ToString());
	}

	private void calculateSpeed() {
		rigidbody2D.velocity = new Vector2(baseSpeed +
		                                   (baseSpeed * Mathf.Sqrt(2f * gatesPassed)), rigidbody2D.velocity.y);
	}

	void OnDestroy() {
		transform.parent.gameObject.AddComponent<GameOverScript>();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		GateScript gate = collision.gameObject.GetComponent<GateScript>();
		if (gate != null)
		{
			ColorChangerScript enemyColorChanger = collision.gameObject.GetComponent<ColorChangerScript>();

			if(color != enemyColorChanger.color) {
				if(collision.gameObject.transform.rotation.eulerAngles.z != 0) {
					BoxCollider2D collider = collision.gameObject.GetComponent<BoxCollider2D>();
					collider.isTrigger = false;
				}// else Destroy(gameObject);
			}
			else {
				gatesPassed++;
			}
			Destroy(collision.gameObject);
		}
	}
}