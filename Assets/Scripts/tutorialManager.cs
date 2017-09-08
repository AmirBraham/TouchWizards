using UnityEngine;
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
        if (Application.loadedLevelName == "SinglePlayer")
        {
            if (PlayerPrefs.HasKey("SoloFirstTime"))
            {
                Destroy(gameObject);
            }
            else
            {
                PlayerPrefs.SetInt("SoloFirstTime", 1);
				steps = normalsteps;
            }
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
				steps = normalsteps.Concat(multisteps).ToArray();
				Debug.Log (steps);
            }
        }
    }

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
    }
}
