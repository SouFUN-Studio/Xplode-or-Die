using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDone : MonoBehaviour
{
    public GameObject tutorial;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("GameController").GetComponent<SettingsController>().SetTutorialStatus(true);
        GameObject.Find("GameController").GetComponent<GameController>().StartGame();
        GameObject.Find("GameController").GetComponent<GameController>().HUD.transform.Find("Pause Button").GetComponent<Button>().enabled = true;
        tutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
