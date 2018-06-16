using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class SoloGameManager : MonoBehaviour
{
    public Text replayText;
    public GameObject confettis;
    public Text score;
    public Text HighScoreText;
    public GameObject PauseMenu;
    public Slider MusicSlider;
    public Slider SFXSlider;
    int normalPlayerSpeed = 4;
    public GameObject SpeedBoostPrefab;
    public Text AdRespopnseText;
    public GameObject AdButton;
    public GameObject ReviveButton;
    public static GameObject GameOver;
    public Text P1_GameOverTextGO;
    public static string P1_GameOverText;
    public static string replaytext;
    public static GameObject GameOn;
    string gameId = "1452701";
    int lastScore;
    List<float> BoosterXPos = new List<float>();

    void Start()
    {
        Advertisement.Initialize(gameId);
		lastScore = PlayerPrefs.GetInt("CurrentScore");
        confettis.SetActive(false);
        HighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
        Time.timeScale = 1;
        normalPlayerSpeed = 4;
        Time.timeScale = 1;
        GameOn = GameObject.FindGameObjectWithTag("GameOn");
        GameOver = GameObject.FindGameObjectWithTag("GameOver");
        GameOver.SetActive(false);
        GameOn.SetActive(true);
        BoosterXPos.Add(-5f);
        BoosterXPos.Add(5f);
        InvokeRepeating("SpawnSpeedBoost", 10, Random.Range(10, 15));
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVol");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");
        AdButton.SetActive(false);
        ReviveButton.SetActive(false);
    }
	public void ShowRewardedVideo()
	{
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleShowResult;

		Advertisement.Show("video", options);
	}

	void HandleShowResult(ShowResult result)
	{
		if (result == ShowResult.Finished)
		{
			Debug.Log("Video completed - Offer a reward to the player");
            Destroy(AdButton);
			Application.LoadLevel(Application.loadedLevelName);

		}
		else if (result == ShowResult.Skipped)
		{
			Debug.LogWarning("Video was skipped - Do NOT reward the player");
			AdButton.SetActive(true);

			AdRespopnseText.text = "Video was skipped :( ";

		}
		else if (result == ShowResult.Failed)
		{
			AdButton.SetActive(true);

			AdRespopnseText.text = "Video failed to show";

		}
	}
    void Update()
    {
        replayText.text = replaytext;
        P1_GameOverTextGO.text = P1_GameOverText;
        if (SoloP1_Controls.Health > 0)
        {
            score.text = "Your Score: " + PlayerPrefs.GetInt("CurrentScore").ToString();
        }
        else
        {
            score.text = "Your Score: " + lastScore.ToString();

        }
        if (PlayerPrefs.GetInt("CurrentScore") > PlayerPrefs.GetInt("HighScore"))
        {
            confettis.SetActive(true);
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("CurrentScore"));
            HighScoreText.text = "New High Score! : " + PlayerPrefs.GetInt("HighScore").ToString();
        }

        if(P1_GameOverText == "You Lose!") {
            ReviveButton.SetActive(true);
        }
    }


    public void changeMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVol", MusicSlider.value);
    }

    public void changeSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXVol", SFXSlider.value);
    }

    public void ShowPause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void HidePauses()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }

    public void RestartLevel()
    {
        if(P1_GameOverText == "You Lose!") {
            PlayerPrefs.SetInt("CurrentScore", 0);
		}
		Application.LoadLevel(Application.loadedLevelName);

	}

    public void GoHome()
    {
        PlayerPrefs.SetInt("CurrentScore", 0);
        Application.LoadLevel("Start");
    }

    void SpawnSpeedBoost()
    {
        GameObject SpeedBoostClone;
        SpeedBoostClone = Instantiate(SpeedBoostPrefab, new Vector3(BoosterXPos[Random.Range(0, BoosterXPos.Count)], Random.Range(0.5f, -0.65f), 0), Quaternion.identity) as GameObject;
    }
}
