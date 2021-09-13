using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public GameObject jumpSoundGameObject;
	private AudioSource jumpSound;
	private Rigidbody2D rb;
	private bool jump;
	public GameObject explosion;
	private bool moveLeft = false;
	private bool moveRight = false;

	void Start() {
		jump = true;
		rb = GetComponent<Rigidbody2D> ();
		var spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.color = new Color(1, 0.7f, 0.9f, 1);
		jumpSound = jumpSoundGameObject.GetComponent<AudioSource> ();
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && jump && Time.timeScale == 1) {
			jump = true;
			PlayerJump();
		}
		
		if (Input.GetKey(KeyCode.LeftArrow)) {
			moveLeft = true;
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow)) {
			moveLeft = false;
		}
		if(Input.GetKey (KeyCode.RightArrow)){
			moveRight = true;	
		}
		if (Input.GetKeyUp(KeyCode.RightArrow)) {
			moveRight =false;
		}
		if(rb.velocity.y <= 0)
			Physics2D.IgnoreLayerCollision (8, 9, false);
	}

	void FixedUpdate() {
		if(moveLeft) {
			rb.velocity = new Vector2 (0, rb.velocity.y);
			rb.position = new Vector2 (rb.position.x - 0.1f, rb.position.y);
			transform.Rotate (0, 0, transform.rotation.z + 10);
		}
		if(moveRight) {
			rb.velocity = new Vector2 (0, rb.velocity.y);
			rb.position = new Vector2 (rb.position.x + 0.1f, rb.position.y);
			transform.Rotate (0, 0, transform.rotation.z - 10);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag.Equals("LevelEnd")) {
			GameObject.Find ("LevelEndSound").GetComponent<AudioSource> ().Play ();
			GameObject.Find ("Player").GetComponent<PlayerMovement> ().enabled = false;
			Invoke("ShowLevelEndMenu", 2f);
			GameObject.Find("Flag").GetComponent<FlagAnimation> ().enabled = true;
			int currLevel = Int32.Parse(SceneManager.GetActiveScene().name);
			print(currLevel + " " + PlayerPrefs.GetInt("levelUnlock", 0));

			if(PlayerPrefs.GetInt("levelUnlock", 0) <= currLevel) {
				PlayerPrefs.SetInt("levelUnlock", currLevel + 1);
			}
		}else if(col.gameObject.tag.Equals("GameOverObstacle")) {
			GameObject.Find("ExplosionSound").GetComponent<AudioSource> ().Play ();
			GameObject.Find("GameManager").GetComponent<Menus> ().GameOver();
			explosion.transform.parent = null;
			explosion.SetActive(true);
			Destroy (this.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag.Equals ("Ground")) {
			jump = false;
			Physics2D.IgnoreLayerCollision (8, 9, true);
		}
    }

	void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.tag.Equals ("Ground")) {
			jump = true;
		}
    }

	public void PlayerJump() {
		if(jump) {
			jump = false;
			Vector3 cam = Camera.main.transform.up;
			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce (cam * 500);
			jumpSound.Play ();
			PlayerPrefs.SetInt("NumberOfJumps", PlayerPrefs.GetInt("NumberOfJumps") + 1);
		}
	}

	public void StartMovingLeft() {
		moveLeft = true;
	}

	public void EndMovingLeft() {
		moveLeft = false;
	}

	public void StartMovingRight() {
		moveRight = true;
	}

	public void EndMovingRight() {
		moveRight = false;
	}

	private void ShowLevelEndMenu() {
		GameObject.Find ("LevelEndUI").GetComponent<RectTransform> ().localScale = new Vector2 (1, 1);
	}
}
