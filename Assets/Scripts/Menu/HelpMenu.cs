using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour {

    public Button nextButton;
    public Button backButton;

    public GameObject imageNorm;
    public GameObject imageInv;
    public GameObject imageMult;


    public void NextImage()
    {
        if (imageNorm.activeSelf)
        {
            imageInv.SetActive(true);
            imageNorm.SetActive(false);
            //backButton.GetComponent<Image>().enabled = true;
        }
        else
        {
            if (imageInv.activeSelf)
            {
                imageMult.SetActive(true);
                imageInv.SetActive(false);
              //nextButton.GetComponent<Image>().enabled = false;
            }
            else
            {
                ResetHelpMenu();
                GameObject.Find("Help Menu Canvas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Main Menu Canvas").GetComponent<Canvas>().enabled = true;
            }
        }
    }


    public void ResetHelpMenu()
    {
        imageNorm.SetActive(true);
        imageInv.SetActive(false);
        imageMult.SetActive(false);
    }
    /*CODE DESPRECIATED
    public void BackImage()
    {
        if (imageInv.activeSelf)
        {
            imageNorm.SetActive(true);
            imageInv.SetActive(false);
            backButton.GetComponent<Image>().enabled = false;
        }else
        {
            if (imageMult.activeSelf)
            {
                imageInv.SetActive(true);
                imageMult.SetActive(false);
                nextButton.GetComponent<Image>().enabled = true;
            }
        }
    }*/

}
