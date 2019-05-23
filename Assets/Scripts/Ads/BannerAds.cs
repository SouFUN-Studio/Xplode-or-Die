using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{

    public string bannerPlacement = "EditorBanner";
    public bool testMode = true;

#if UNITY_IOS
    public const string gameID = "1353590";
#elif UNITY_ANDROID
    public const string gameID = "1353589";
#elif UNITY_EDITOR
    public const string gameID = "1353589";
#endif

    void Start()
    {
        Advertisement.Initialize(gameID, testMode);
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady("EditorBanner"))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(bannerPlacement);
    }
}