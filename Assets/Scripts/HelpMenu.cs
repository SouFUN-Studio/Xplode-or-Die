using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour {

    public Button nextButton;
    public Button backButton;

    public Image imageNorm;
    public Image imageInv;
    public Image imageMult;

    public void NextImage()
    {
        if (imageNorm.enabled == true)
        {
            imageInv.enabled = true;
            imageNorm.enabled = false;
            backButton.GetComponent<Image>().enabled = true;
        }
        else
        {
            if (imageInv.enabled)
            {
                imageMult.enabled = true;
                imageInv.enabled = false;
                nextButton.GetComponent<Image>().enabled = false;
            }
        }
    }

    public void BackImage()
    {
        if (imageInv.enabled)
        {
            imageNorm.enabled = true;
            imageInv.enabled = false;
            backButton.GetComponent<Image>().enabled = false;
        }else
        {
            if (imageMult.enabled)
            {
                imageInv.enabled = true;
                imageMult.enabled = false;
                nextButton.GetComponent<Image>().enabled = true;
            }
        }
    }

}
