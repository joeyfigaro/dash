
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	private ColorChangerScript colorChanger;

	void Start() {
		colorChanger = GetComponent<ColorChangerScript>();
		colorChanger.colorNew = Random.Range(0, 3);
	}

	void Update()
	{
		if(Input.GetKey("1")) colorChanger.colorNew = 0;
		if(Input.GetKey("2")) colorChanger.colorNew = 1;
		if(Input.GetKey("3")) colorChanger.colorNew = 2;

		colorChanger.colorMaintenance();
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
			if(enemyColorChanger.colorNew != colorChanger.colorNew) Destroy(gameObject);
		}
	}
}