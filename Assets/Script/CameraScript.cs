using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public GameObject player;

	public float cameraFieldInitial = 3f;
	public float cameraFieldMax = 6f;

	private PlayerScript ps;

	void Start () {
		ps = player.GetComponent<PlayerScript>();
		transform.position = getAdjustedPosition();
	}

	private Vector3 getAdjustedPosition() {
		Vector3 pos = player.transform.position;
		pos.z = transform.position.z;

		return pos;
	}

	void Update () {
		float speedRatio = .5f;

		Vector3 pos = transform.position;
		Vector3 vel = new Vector3(0, 0, 0);
		float cameraLeftOffset = 0;
		float cameraBottomOffset = 0;

		if(player != null) {
			pos = getAdjustedPosition();
			vel = player.rigidbody2D.velocity;
			cameraLeftOffset = (
				Camera.main.ViewportToWorldPoint(
				new Vector3(0, 0, player.transform.position.z)
				).x - 
				Camera.main.ViewportToWorldPoint(
				new Vector3(-1, 0, player.transform.position.z)
				).x) / 1.5f;
			cameraBottomOffset = (
				Camera.main.ViewportToWorldPoint(
				new Vector3(0, 0, player.transform.position.z)
				).y -
				Camera.main.ViewportToWorldPoint(
				new Vector3(0, -1, player.transform.position.z)
				).y) / 2;
		}

//		float speedRatio = ps.baseSpeed / vel.x;
//		if(float.IsNaN(speedRatio)) speedRatio = 1;
//		if(float.IsInfinity(speedRatio)) speedRatio = 1;
//		if(speedRatio == 0) speedRatio = 1;

		float newField = cameraFieldInitial + (cameraFieldMax - (speedRatio * cameraFieldMax));

		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, newField, Time.deltaTime * 5f);

		pos.x = pos.x + (cameraLeftOffset - (speedRatio * cameraLeftOffset));
		pos.x = Mathf.Lerp(transform.position.x, pos.x, Time.deltaTime * ps.baseSpeed);

//		pos.y = pos.y + (cameraBottomOffset - (speedRatio * cameraBottomOffset));
//		pos.y = Mathf.Lerp(transform.position.y, pos.y, Time.deltaTime * 5f);
		pos.y = 4.9f;
		transform.position = pos;
	}
}
