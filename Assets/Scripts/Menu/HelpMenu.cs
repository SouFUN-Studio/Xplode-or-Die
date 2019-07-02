using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour {

    public GameObject[] images;
    public GameObject mask;
    public GameObject helpMenu;
    public void NextImage()
    {
        if (images[0].activeSelf)
        {
            images[1].SetActive(true);
            images[0].SetActive(false);
            //backButton.GetComponent<Image>().enabled = true;
        }
        else
        {
            if (images[1].activeSelf)
            {
                images[2].SetActive(true);
                images[1].SetActive(false);
              //nextButton.GetComponent<Image>().enabled = false;
            }
            else
            {
                if (images[2].activeSelf)
                {
                    images[3].SetActive(true);
                    images[2].SetActive(false);
                    //nextButton.GetComponent<Image>().enabled = false;
                }
                else
                {
                    if (images[3].activeSelf)
                    {
                        images[4].SetActive(true);
                        images[3].SetActive(false);
                        //nextButton.GetComponent<Image>().enabled = false;
                    }
                    else
                    {
                        mask.SetActive(false);
                        helpMenu.GetComponent<MenuMover>().SetActive(false);
                    }
                }
                
            }
        }
    }


    public void ResetHelpMenu()
    {
        foreach(GameObject im in images)
        {
            im.SetActive(false);
        }
        images[0].SetActive(true);
        
    }

}
