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
        hasTouched = TouchRelease();
        if(hasTouched == true) {
            stepcounter++;
        }
		if (stepcounter >= steps.Length) {
			Time.timeScale = 1;
			Destroy(gameObject);
		} else {
            Time.timeScale = 0;
			gameObject.GetComponent<Image> ().sprite = steps [stepcounter];
		}
    }


	public static bool TouchRelease()
	{
		bool b = false;
		for (int i = 0; i < Input.touches.Length; i++)
		{
			b = Input.touches[i].phase == TouchPhase.Ended;
			if (b)
				break;
		}
		return b;
	}
}
