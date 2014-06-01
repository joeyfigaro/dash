using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public GameObject player;

	public float cameraFieldInitial = 3f;
	public float cameraFieldMax = 6f;

	private Vector3 posDestOffset = new Vector3(0, 0, 0);
	private Quaternion rotDestOffset = new Quaternion(0, 0, 0, 0);	private Vector3 posCurOffset = new Vector3(0, 0, 0);
	private Quaternion rotCurOffset = new Quaternion(0, 0, 0, 0);


	void Start () {
		transform.position = getAdjustedPosition();
	}

	public void hyperMode() {
		Debug.Log ("Hypermode");
		posDestOffset = new Vector3(-1, -1, 0);
		rotDestOffset = Quaternion.Euler(0, 45, 0);
	}

	private Vector3 getAdjustedPosition() {
		Vector3 pos = player.transform.position;
		pos.z = transform.position.z;

		return pos;
	}

	void LateUpdate () {
		float speedRatio = .5f;

		Vector3 pos = transform.position;
		float cameraLeftOffset = 0;

		if(player != null) {
			pos = getAdjustedPosition();
			cameraLeftOffset = (
				Camera.main.ViewportToWorldPoint(
				new Vector3(0, 0, player.transform.position.z)
				).x - 
				Camera.main.ViewportToWorldPoint(
				new Vector3(-1, 0, player.transform.position.z)
				).x) / 1.5f;
		}

//		float newField = cameraFieldInitial + (cameraFieldMax - (speedRatio * cameraFieldMax));

//		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, newField, Time.deltaTime * 5f);

//		pos.x = pos.x + (cameraLeftOffset - (speedRatio * cameraLeftOffset));
		pos.y = 4.9f;

		transform.position = pos;

		pos.z = Camera.main.transform.position.z;
		Camera.main.transform.position = pos;

		posCurOffset = Vector3.Lerp(posCurOffset, posDestOffset, Time.deltaTime);
		Camera.main.transform.position += posCurOffset;

		Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, rotDestOffset, 10 * Time.deltaTime);
	}
}
