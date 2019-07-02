using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public GameObject[] menus;
    public GameObject mask;
    public GameObject changeNameMask;
    public AudioSource closeSource;
    public AudioClip closeClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            closeSource.PlayOneShot(closeClip);
            if (menus[0].activeSelf)
            {
                Component[] buttons = menus[0].GetComponentsInChildren<Button>();
                foreach(Button b in buttons)
                {
                    b.interactable = false;
                }
                mask.SetActive(true);
                menus[1].SetActive(true);
                menus[1].GetComponent<MenuMover>().SetActive(true);
            }


            if (menus[2].activeSelf && menus[3].activeSelf)
            {
                changeNameMask.SetActive(false);
                menus[3].GetComponent<MenuMover>().SetActive(false);
            }
            else
            {
                mask.SetActive(false);
                menus[2].GetComponent<MenuMover>().SetActive(false);
                
            }

            if (menus[4].activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                mask.SetActive(false);
                menus[4].GetComponent<MenuMover>().SetActive(false);
                
                //Extra Code to restart the help menu configuration
                GameObject.Find("GameController").GetComponent<HelpMenu>().ResetHelpMenu();
            }

            if (menus[5].activeSelf)
            {
                mask.SetActive(false);
                menus[5].GetComponent<MenuMover>().SetActive(false);
                
            }
            if (menus[6].activeSelf)
            {
                mask.SetActive(false);
                menus[6].GetComponent<MenuMover>().SetActive(false);
                
            }
        }
    }
}
