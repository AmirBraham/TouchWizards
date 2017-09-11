using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class tutorialManager : MonoBehaviour
{
	public Sprite[] normalsteps;
	public Sprite[] multisteps;
	Sprite[] steps;
	int stepcounter;
	bool hasTouched;


    void Start()
    {
		hasTouched = false;
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
		if (Input.touchCount > 0 ) {
			if (!hasTouched) {
				stepcounter++;
				hasTouched = true;
			}
		} else {
			hasTouched = false;
		}
		if(Input.GetButton("Fire1")){
			stepcounter++;
		}
		if (stepcounter >= steps.Length) {
			Time.timeScale = 1;
		} else {
			gameObject.GetComponent<Image> ().sprite = steps [stepcounter];
		}
    }
}
