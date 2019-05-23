using UnityEngine.Monetization;
using System.Collections;
using UnityEngine;

public class UnityAdsScript : MonoBehaviour
{

    string gameId = "1353589";
    bool testMode = true;

    void Start()
    {
        Monetization.Initialize(gameId, testMode);
    }
}