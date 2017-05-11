using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    /*
    * Botones del canvas MainMenu
    */
    public Button audioButton;
    public Button creditsButton;
    public AudioSource music;
    public Sprite soundOn;
    public Sprite soundOff;


    public void AudioEnabled()
    {
        if (music.enabled) {
            audioButton.GetComponent<Image>().sprite =  soundOff;
            music.enabled = false;
        }else
        {
            audioButton.GetComponent<Image>().sprite = soundOn;
            music.enabled = true;
        }
    }
}
 