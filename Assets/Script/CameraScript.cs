using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public GameObject player;

	public float cameraFieldInitial = 3f;
	public float cameraFieldMax = 6f;

	void Start () {
		transform.position = getAdjustedPosition();
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

		float newField = cameraFieldInitial + (cameraFieldMax - (speedRatio * cameraFieldMax));

		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, newField, Time.deltaTime * 5f);

		pos.x = pos.x + (cameraLeftOffset - (speedRatio * cameraLeftOffset));
		pos.y = 4.9f;
		transform.position = pos;
	}
}
