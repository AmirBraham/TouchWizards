using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GoogleMobileAds.Api;

public class SceneManager : MonoBehaviour
{
    public GameObject sorryPanel;
    public GameObject MainButtons;
    public GameObject MultiChooseButtons;
    public GameObject OptionsMenu;
    public Slider MusicSlider;
    private BannerView bannerView;

    void Start()
    {
        sorryPanel.SetActive(false);
        Time.timeScale = 1;
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVol");
        string appId = "ca-app-pub-4105711425411317~2575620706";
        MobileAds.Initialize(appId);
        RequestBanner();

    }

    public void ShowSorry()
    {
        sorryPanel.SetActive(true);
    }

    public void HideSorry()
    {
        sorryPanel.SetActive(false);
    }

    public void bringMultioptions()
    {
        MultiChooseButtons.transform.DOMoveX(0, 0.5f);
        MainButtons.transform.DOMoveX(-10, 0.5f);
    }

    public void TakeMultioptions()
    {
        MultiChooseButtons.transform.DOMoveX(10, 0.5f);
        MainButtons.transform.DOMoveX(0, 0.5f);
    }

    public void changeMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVol", MusicSlider.value);
    }

    public void ShowOptions()
    {
        OptionsMenu.SetActive(true);
    }

    public void HideOptions()
    {
        OptionsMenu.SetActive(false);
    }

    public void SoloScene()
    {
        Application.LoadLevel("SinglePlayer");
    }

    public void MultiScene()
    {
        Application.LoadLevel("LocalMultiplayer");
    }
    public void OnlineMultiScene()
    {
        Application.LoadLevel("OnlineMultiplayer");
    }
    void RequestBanner()
    {
        string adUnitId = "ca-app-pub-4105711425411317/9400458479";
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);

    }
}
