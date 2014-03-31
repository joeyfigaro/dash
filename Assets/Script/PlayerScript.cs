
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	private ColorChangerScript colorChanger;
	public int gatesPassed = 0;

	void Start() {
		colorChanger = GetComponent<ColorChangerScript>();
		colorChanger.colorNew = Random.Range(0, 3);
		calculateSpeed();
	}

	void Update()
	{
		if(Input.GetKey("1")) colorChanger.colorNew = 0;
		if(Input.GetKey("2")) colorChanger.colorNew = 1;
		if(Input.GetKey("3")) colorChanger.colorNew = 2;

		colorChanger.colorMaintenance();
	}

	void calculateSpeed() {
		ScrollingScript scrolling = gameObject.GetComponent<ScrollingScript>();
		scrolling.direction.x = .5f + Mathf.Sqrt(gatesPassed / 10f);
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

			if(enemyColorChanger.colorNew != colorChanger.colorNew) {
				if(collision.gameObject.transform.rotation.eulerAngles.z != 0) {
					BoxCollider2D collider = collision.gameObject.GetComponent<BoxCollider2D>();
					collider.isTrigger = false;
				} else Destroy(gameObject);
			}
			else {
				gatesPassed++;
				calculateSpeed();
			}
		}
	}
}