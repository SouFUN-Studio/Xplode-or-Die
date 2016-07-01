using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour {
    MainMenu mainMenu = new MainMenu();

    Button closeButton;
    Button fogButton;
    Button backButton;
    Button nextButton;

    public Canvas helpMenu;

    Image bombaNorm;
    Image bombaInv;
    Image bombaMult;
    
	// Use this for initialization
	void Start () {
        helpMenu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /*
    * Metodo para cerrar el menu de ayuda y reactivacion del menu principal
  
    public void closeButtonPress()
    {
        helpMenu.enabled = false;
        helpButton.enabled = true;
        audioButton.enabled = true;
        playButton.enabled = true;
        creditsButton.enabled = true;
        fogImage.enabled = false;
        fogButton.enabled = false;
    }
      */
}
