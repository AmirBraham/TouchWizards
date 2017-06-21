using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour {
    public static bool gameOver;
    public GameObject PauseMenu;
    public Slider MusicSlider;
    public Slider SFXSlider;
    int p1taps=0;
    int p2taps = 0;
    int normalPlayerSpeed = 4;
    float lastUpdate;
    float tapDuelCheck;
	float timer = 0.0f;
    public Image timerCircle;
    public GameObject tappos;
	public GameObject SpeedBoostPrefab;
	public static  GameObject GameOver;
	public static  Text  P1_GameOverText;
	public static Text P2_GameOverText;
	public static GameObject GameOn;
    public int timeAmount = 10;
	public static  float timeLeft ;
	public static bool TimeUp;
    List<float> BoosterXPos = new List<float>();
    public float DuelDuration;
    List<string> GameObjectTags = new List<string>();
    GameObject BG;
    int Replays_num = 0;
    Wizard Blue_Wizard ;

    public GameObject shield;

    


    void Awake()
    {
        
        Blue_Wizard = GameObject.FindGameObjectWithTag("Player_1").AddComponent<Wizard>();
        Blue_Wizard.GenerateControls();
        Blue_Wizard.setShieldPrefab(shield);
        Blue_Wizard.setNumberOfShields(2);
        //Blue_Wizard.setShieldPoint(new Vector2(0,0));
        Blue_Wizard.GenerateShield ();

    }
	void Start() {
        Blue_Wizard.setHealth(1);
        Blue_Wizard.setMoveSpeed(4);
        Blue_Wizard.gameObject.AddComponent<Rigidbody2D>();
        Debug.Log(SpeedBoostPrefab.ToString());
        gameOver = false;
        Time.timeScale = 1;
        p1taps = p2taps = 0;
		normalPlayerSpeed = 4;
		Time.timeScale=1;
		GameOn = GameObject.FindGameObjectWithTag("GameOn");
		GameOver = GameObject.FindGameObjectWithTag("GameOver");
        P1_GameOverText = GameObject.FindGameObjectWithTag("P1_GameOverText").GetComponent<Text>();
        P2_GameOverText = GameObject.FindGameObjectWithTag("P2_GameOverText").GetComponent<Text>();
        GameOver.SetActive(false);
		GameOn.SetActive(true);
		BoosterXPos.Add(-5f);
		BoosterXPos.Add(5f);
		InvokeRepeating("SpawnSpeedBoost",10,Random.Range(10,15));
		TimeUp = false;
        timeLeft = timeAmount;
        lastUpdate = Time.time;
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVol");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");
        GameObjectTags.AddRange (new string[]{"P1_Shield","P2_Shield","P1_Bullet","P2_Bullet","SpeedBooster"});
        BG = GameObject.FindGameObjectWithTag("BG");
    }


	void Update(){
        Blue_Wizard.Movement();
        Blue_Wizard.Loop();
		if(timeLeft > 0) {
            timerCircle.fillAmount = timeLeft / timeAmount;
            tappos.SetActive(false);
            tapDuelCheck = Time.time;
			timeLeft -= Time.deltaTime;
		}else if(!gameOver) {
                DuelTime ();
                tappos.SetActive(true);
                timerCircle.fillAmount = (( (int) DuelDuration - (Time.time -tapDuelCheck))/DuelDuration);
                countTaps();
                TimeUp = true;
                GameOn.SetActive(false);
                float difference = p1taps - p2taps;
                
                if (Time.time-tapDuelCheck<DuelDuration) { 
                    if (Time.time-lastUpdate > 0.2f)
                    {
                        tappos.transform.DOMove (new Vector3(tappos.transform.position.x,tappos.transform.position.y+(difference+Random.Range(-0.5f,0.5f))/10,tappos.transform.position.z), 0.2f).SetEase(Ease.OutQuad);
                    lastUpdate = Time.time;
                    p1taps = p2taps = 0;

                    }
                }else {
                GameOver.SetActive(true);
                Time.timeScale = 0;
                if (tappos.transform.position.y > 0)
                    {
                        P1_GameOverText.text = "You Win!";
                        P2_GameOverText.text = "You Lose!";
                    }
                    else
                    {
                        P1_GameOverText.text = "You Lose!";
                        P2_GameOverText.text = "You Win!";
                    }

                }
        }
		if(P1_Controls.MoveSpeed!= P2_Controls.MoveSpeed) {
				timer+=Time.deltaTime;
		}
		if(timer >=10f) {
				P1_Controls.MoveSpeed = P2_Controls.MoveSpeed = normalPlayerSpeed;
				timer = 0;
			}
		
		
		}
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
    private void countTaps()
    {
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                if (Input.GetTouch(i).position.y > (Screen.height / 2) )
                {
                    p2taps++;
                }
                else
                {
                    p1taps++;
                }
            }
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
    
     public void ShowReplayAd()
              {
                if (Advertisement.IsReady())
                {
                    var options = new ShowOptions { resultCallback = HandleShowResult };
                  Advertisement.Show(options);
                }
              }

     private void HandleShowResult(ShowResult result)
              {
                switch (result)
                {
                  case ShowResult.Finished:
                    Debug.Log("The ad was successfully shown.");
                    PlayerPrefs.SetInt("Replays_num",0);

                    break;
                  case ShowResult.Skipped:
                    Debug.Log("The ad was skipped before reaching the end.");
                    break;
                  case ShowResult.Failed:
                    Debug.LogError("The ad failed to be shown.");
                    break;
                }
              }

    public void RestartLevel () {
        
        PlayerPrefs.SetInt("Replays_num",PlayerPrefs.GetInt("Replays_num",0)+1);
        if(PlayerPrefs.GetInt("Replays_num") >= 4) {
            ShowReplayAd();
        } else {
             Application.LoadLevel(Application.loadedLevelName);
        }
                print(PlayerPrefs.GetInt("Replays_num"));

    }
    public void GoHome()
    {
        Application.LoadLevel("Start");
    }

    void SpawnSpeedBoost () {
			GameObject SpeedBoostClone;
			SpeedBoostClone = Instantiate(SpeedBoostPrefab, new Vector3 (BoosterXPos[Random.Range(0,BoosterXPos.Count)],Random.Range(0.5f,-0.65f),0),Quaternion.identity) as GameObject;
		}
    void DuelTime () {
        BG.GetComponent<SpriteRenderer>().color = new Color(0.5607f,0.5607f,0.5607f);
        for(int i = 0;i< GameObjectTags.Count;i++ ) {
            GameObject[] Objects =  GameObject.FindGameObjectsWithTag(GameObjectTags[i]);
            foreach (GameObject item in Objects) {
                Destroy(item);
            }
        }
    }
}
