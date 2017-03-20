using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class Ads : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }
    // Update is called once per frame
    void Update () {
	
	}
}
