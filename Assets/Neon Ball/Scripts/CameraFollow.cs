using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject player;
	private Vector3 velocity = Vector3.zero;
	private Vector3 playerPosition;
	void Update () {
		if (player == null) return;
		if (player.transform.position.x < 0) {
			playerPosition = new Vector3 (0, player.transform.position.y + 1, -10);
		} else {
			playerPosition = new Vector3 (player.transform.position.x, player.transform.position.y + 1, -10);
		}
		transform.position = Vector3.SmoothDamp (transform.position, playerPosition, ref velocity, 0.5f);	
	}
}
