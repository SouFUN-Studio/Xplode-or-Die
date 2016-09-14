using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    /*
    * Botones del canvas MainMenu
    */
    public Button playButton;
    public Button helpButton;
    public Button scoreButton;
    public Button audioButton;
    public Button creditsButton;
    public AudioSource music;
    public Canvas mainCanvas;

    // Use this for initialization
    void Start () {
        /* Inicialización del menu de ayuda y boton de ayuda
        */
        playButton = playButton.GetComponent<Button>();
        helpButton = helpButton.GetComponent<Button>();
        scoreButton = scoreButton.GetComponent<Button>();
        audioButton = audioButton.GetComponent<Button>();
        creditsButton = creditsButton.GetComponent<Button>();

        mainCanvas = mainCanvas.GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    /*
    * Metodo para prender el el menu HUD en la pantalla
    */
    public void deactivateMainCanvas()
    {
        mainCanvas.enabled = false; 
    }

    /* 
    * Metodo para prender o apagar el audio del juego
    */
    public void audioEnabled()
    {
        if (music.enabled)
        {
            music.enabled = false;
        }
        else
        {
            music.enabled = true;
        }
    }

    /*
    * *****METODO PARA DESACTIVAR TODOS LOS BOTONES DEL CANVAS*****
    */
    public void deactivateButtons()
    {
        playButton.enabled = false;
        helpButton.enabled = false;
        scoreButton.enabled = false;
        audioButton.enabled = false;
        creditsButton.enabled = false;
    }
    /*
    * *****METODO PARA ACTIVAR TODOS LOS BOTONES DEL CANVAS*****
    */
    public void activateButtons()
    {
        playButton.enabled = false;
        helpButton.enabled = false;
        scoreButton.enabled = false;
        audioButton.enabled = false;
        creditsButton.enabled = false;
    }

    public void startCredits()
    {
        //UnityEngine.SceneManagement.LoadSceneMode mode;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1, UnityEngine.SceneManagement.LoadSceneMode.Single);
        //Application.LoadLevel (1); obsolete

    }
}
