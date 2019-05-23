using UnityEngine.Monetization;
using System.Collections;
using UnityEngine;

public class UnityAdsPlacement : MonoBehaviour
{

    public string placementId = "video";

    public void ShowAd()
    {
        StartCoroutine(ShowAdWhenReady());
    }

    private IEnumerator ShowAdWhenReady()
    {
        while (!Monetization.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.25f);
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show();
        }
    }
}