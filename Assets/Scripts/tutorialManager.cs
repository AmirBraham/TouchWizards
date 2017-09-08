using UnityEngine;
<<<<<<< HEAD

public class tutorialManager : MonoBehaviour
{
    public GameObject NextButton;
    public GameObject BeforeButton;

    public GameObject DefaultContent;
    public GameObject LMultiContent;

    void Start()
    {
        BeforeButton.SetActive(false);
=======
using System.Linq;
using UnityEngine.UI;

public class tutorialManager : MonoBehaviour
{
	public Sprite[] normalsteps;
	public Sprite[] multisteps;
	Sprite[] steps;
	int stepcounter;


    void Start()
    {
		stepcounter = 0;
>>>>>>> V2
        if (Application.loadedLevelName == "SinglePlayer")
        {
            if (PlayerPrefs.HasKey("SoloFirstTime"))
            {
                Destroy(gameObject);
            }
            else
            {
                PlayerPrefs.SetInt("SoloFirstTime", 1);
<<<<<<< HEAD
            }
            BeforeButton.SetActive(false);
            NextButton.SetActive(false);
=======
				steps = normalsteps;
            }
>>>>>>> V2
        }
        if (Application.loadedLevelName == "LocalMultiplayer")
        {
            if (PlayerPrefs.HasKey("LMultiFirstTime"))
            {
                Destroy(gameObject);
            }
            else
            {
                PlayerPrefs.SetInt("LMultiFirstTime", 1);
<<<<<<< HEAD
=======
				steps = normalsteps.Concat(multisteps).ToArray();
				Debug.Log (steps);
>>>>>>> V2
            }
        }
    }

<<<<<<< HEAD
    public void Close()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    void Update()
    {
        Time.timeScale = 0;
    }

    public void Next()
    {
        BeforeButton.SetActive(true);
        NextButton.SetActive(false);
        DefaultContent.SetActive(false);
        LMultiContent.SetActive(true);
    }

    public void Before()
    {
        BeforeButton.SetActive(false);
        NextButton.SetActive(true);
        DefaultContent.SetActive(true);
        LMultiContent.SetActive(false);
=======
    void Update()
    {
        Time.timeScale = 0;
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			if(touch.phase==TouchPhase.Began){
				stepcounter++;
			}
		}
		if(Input.GetButton("Fire1")){
			stepcounter++;
		}
		if (stepcounter >= steps.Length) {
			Time.timeScale = 1;
			Destroy (gameObject);
		} else {
			gameObject.GetComponent<Image> ().sprite = steps [stepcounter];
		}
>>>>>>> V2
    }
}
