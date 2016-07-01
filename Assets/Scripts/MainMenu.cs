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
        helpButton = helpButton.GetComponent<Button>();
        audioButton = audioButton.GetComponent<Button>();
        playButton = playButton.GetComponent<Button>();
        creditsButton = creditsButton.GetComponent<Button>();

        mainCanvas = mainCanvas.GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    /*
    * Metodo para prender el el menu de ayuda en la pantalla de inicio
    */
    public void helpButtonPress()
    {

    }
    /*
    * Metodo para prender el el menu HUD en la pantalla
    */
    public void deactivateMainCanvas()
    {
        mainCanvas.enabled = false;
    }
    /* 
    * Metodo para abrir el score
    */
    public void scorePress()
    {

    }
    /* 
    * Metodo para prender o apagar el audio del juego
    */
    public void deactivateMusic()
    {
        music.enabled = false;
    }
    /* 
    * Metodo para prender musica
    */
    public void activateMusic()
    {
        music.enabled = true;
    }
    /*
    * Metodo para mostrar los creditos del juego 
    */
    public void creditsPress()
    {

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
    /* 
    * *****FUNCIONES GET DE CADA ATRIBUTO*****
    */
    public Button getPlayButton()
    {
        return playButton;
    }

    public Button getHelpButton()
    {
        return helpButton;
    }

    public Button getScoreButton()
    {
        return scoreButton;
    }

    public Button getAudioButton()
    {
        return audioButton;
    }

    public Button getCreditsButton()
    {
        return creditsButton;
    }
}
