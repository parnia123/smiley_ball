using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using GoogleMobileAds.Api;

public class Menus : MonoBehaviour {

	public GameObject pauseUI;
	public GameObject levelSelectUI;
	public GameObject mainMenuUI;
	public GameObject statsMenuUI;
	public GameObject optionsMenuUI;
	public GameObject resetDataConfirmationDialog;
	public GameObject soundOnOffText;

	private AudioSource buttonSound;

	private InterstitialAd interstitial;

	void Start() {
		buttonSound = GameObject.Find ("ButtonSound").GetComponent<AudioSource> ();

		//MobileAds.Initialize(initStatus => { });
		//RequestInterstitial();
	}

	
	private void RequestInterstitial() {
		#if UNITY_ANDROID
			string adUnitId = "ca-app-pub-3940256099942544/1033173712";
		#elif UNITY_IPHONE
			string adUnitId = "ca-app-pub-3940256099942544/4411468910";
		#else
			string adUnitId = "unexpected_platform";
		#endif

		// Initialize an InterstitialAd.
		this.interstitial = new InterstitialAd(adUnitId);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		this.interstitial.LoadAd(request);
	}

	public void MainMenu() {
		buttonSound.Play ();
		Time.timeScale = 1;
		Invoke("LoadMainMenuScene", 0.2f);
	}

	private void LoadMainMenuScene() {
		SceneManager.LoadScene ("MainMenu");
	}

	public void MainMenuUI() {
		buttonSound.Play ();
		GetComponent<MenuTransitionAnimation> ().enabled = true;
		GetComponent<MenuTransitionAnimation> ().menu = 0;
	}

	public void LevelSelectMenu() {
		buttonSound.Play ();
		GetComponent<MenuTransitionAnimation> ().enabled = true;
		GetComponent<MenuTransitionAnimation> ().menu = 1;
	}

	public void StatsMenuUI() {
		buttonSound.Play ();
		GetComponent<MenuTransitionAnimation> ().enabled = true;
		GetComponent<MenuTransitionAnimation> ().menu = 2;
	}

	public void OptionsMenuUI() {
		buttonSound.Play ();
		GetComponent<MenuTransitionAnimation> ().enabled = true;
		GetComponent<MenuTransitionAnimation> ().menu = 3;
	}

	public void ShowOptionsMenu() {
		optionsMenuUI.SetActive(true);
		mainMenuUI.SetActive (false);
		if(AudioListener.volume == 1) {
			 soundOnOffText.GetComponent<Text> ().text = "SOUND ON";
		}else{
			soundOnOffText.GetComponent<Text> ().text = "SOUND OFF";
		}
	}

	public void SoundOnOff() {
		if(AudioListener.volume == 1) {
			 AudioListener.volume = 0;
			 soundOnOffText.GetComponent<Text> ().text = "SOUND OFF";
		}else{
			AudioListener.volume = 1;
			soundOnOffText.GetComponent<Text> ().text = "SOUND ON";
		}
		buttonSound.Play();
	}

	public void ShowStatsMenu() {
		statsMenuUI.SetActive(true);
		mainMenuUI.SetActive (false);
	}

	public void ShowMainMenu() {
		mainMenuUI.SetActive (true);
		levelSelectUI.SetActive (false);
		statsMenuUI.SetActive(false);
		optionsMenuUI.SetActive(false);
	}

	public void ShowLevelSelectMenu() {
		levelSelectUI.SetActive (true);
		mainMenuUI.SetActive (false);
	}

	public void ShowResetDataConfirmationDialog() {
		buttonSound.Play ();
		resetDataConfirmationDialog.SetActive(true);
	}

	public void HideResetDataConfirmationDialog() {
		buttonSound.Play ();
		resetDataConfirmationDialog.SetActive(false);
	}

	public void ResetAllData() {
		buttonSound.Play ();
		PlayerPrefs.DeleteAll();
		resetDataConfirmationDialog.SetActive(false);
	}


	public void LevelSelect () {
		buttonSound.Play ();
		PlayerPrefs.SetInt("PlayedGames", PlayerPrefs.GetInt("PlayedGames") + 1);
		SceneManager.LoadScene (EventSystem.current.currentSelectedGameObject.name);
	}

	public void Restart() {
		buttonSound.Play ();
		Time.timeScale = 1;
		Invoke ("LevelRestart", 0.3f);
	}

	public void GameOver() {
		Invoke("ShowGameOverUI", 1f);
	}

	private void ShowGameOverUI() {
		GameObject.Find ("GameOverUI").GetComponent<RectTransform> ().localScale = new Vector2 (1, 1);
		//if (this.interstitial.IsLoaded()) {
		//	this.interstitial.Show();
		//}
	}

	private void LevelRestart() {
		PlayerPrefs.SetInt("PlayedGames", PlayerPrefs.GetInt("PlayedGames") + 1);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void Pause() {
		buttonSound.Play ();
		Time.timeScale = 0;
		pauseUI.SetActive (true);
	}

	public void Continue() {
		buttonSound.Play ();
		Time.timeScale = 1;
		pauseUI.SetActive (false);
	}

	public void NextLevel() {
		buttonSound.Play ();
		Time.timeScale = 1;
		Invoke ("LoadNextLevel", 0.3f);
	}

	private void LoadNextLevel() {
		int level = Int32.Parse(SceneManager.GetActiveScene ().name);
		level++;
		if(level >= 21)
        {
			return;
        }

		SceneManager.LoadScene (level.ToString());
	}

	public void ExitTheGame() {
		buttonSound.Play ();
		Application.Quit();
	}
}
