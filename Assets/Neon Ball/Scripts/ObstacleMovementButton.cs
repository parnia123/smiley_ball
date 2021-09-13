using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovementButton : MonoBehaviour {
    
    private float buttonYPos;
    private AudioSource obstacleButtonSound;
    public GameObject obstacle;

    void Start() {
        buttonYPos = transform.localPosition.y;
        obstacleButtonSound = GameObject.Find("ObstacleButtonSound").GetComponent<AudioSource> ();
    }

    void OnTriggerEnter2D(Collider2D col) {
        transform.localPosition = new Vector2(transform.localPosition.x, buttonYPos - 0.1f);
        if(!obstacleButtonSound.isPlaying) {
            obstacleButtonSound.Play();
        }
        if(obstacle.GetComponent<ObstacleLeftRightMovement> () != null) {
            obstacle.GetComponent<ObstacleLeftRightMovement> ().enabled = true;
        }else {
            obstacle.GetComponent<ObstacleShowHide> ().isOn = true;
        }
        
	}

	void OnTriggerExit2D(Collider2D col) {
        transform.localPosition = new Vector2(transform.localPosition.x, buttonYPos);
        GameObject.Find("ObstacleButtonSound").GetComponent<AudioSource> ().Play();
        if(obstacle.GetComponent<ObstacleLeftRightMovement> () != null) {
            obstacle.GetComponent<ObstacleLeftRightMovement> ().enabled = false;
        }else {
            obstacle.GetComponent<ObstacleShowHide> ().isOn = false;
        }
        
    }
}
