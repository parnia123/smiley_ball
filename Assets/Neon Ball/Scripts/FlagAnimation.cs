using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAnimation : MonoBehaviour {

	public GameObject flag;
	private float yPosition = 0; 
	private float timer = 0;
	private float startPosition = 0;

	void Start () {
		yPosition = flag.transform.localPosition.y;
		startPosition = yPosition;
	}

	void Update () {
		timer += Time.deltaTime;
		if(timer >= 0.01f && yPosition < startPosition + 1.3f) {
			timer = 0;
			yPosition+= 0.01f;
			flag.transform.localPosition = new Vector2 (flag.transform.localPosition.x, yPosition);
		}
	}
}
