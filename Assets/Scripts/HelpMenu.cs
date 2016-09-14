using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour {

    public Canvas helpMenu;

    public Image bombaNorm;
    public Image bombaInv;
    public Image bombaMult;
    int count = 1;

	// Use this for initialization
	void Start () {
        bombaNorm = bombaNorm.GetComponent<Image>();
        bombaInv = bombaInv.GetComponent<Image>();
        bombaMult = bombaMult.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /*
    * Metodo que verifica que imagen mostrar del menu de ayuda
    */
    public void test()
    {
        bombaNorm.enabled = true;
    }
    public void showImage()
    {
        switch (count)
        {
            case 1:
                {
                    bombaNorm.enabled = true;
                    bombaMult.enabled = false;
                    bombaInv.enabled = false;
                    break;
                }
            case 2:
                {
                    bombaInv.enabled = true;
                    bombaNorm.enabled = false;
                    bombaMult.enabled = false;
                    break;
                }
            case 3:
                {
                    bombaMult.enabled = true;
                    bombaInv.enabled = false;
                    bombaNorm.enabled = false;
                    break;
                }
        }
    }
    /*
    * Metodo que aumenta el contador 
    */
    public void addCount()
    {
        if(count == 3)
        {
            count = 1;
        }
        else count++;
    }
    /*
    * Metodo que disminuye el contador 
    */
    public void restCount()
    {
        if (count == 1)
        {
            count = 3;
        }
        else count--;
    }
    /*
    * Metodo que regresa los atributos al estado inicial
    */
    public void resetHelpAc()
    {
        bombaNorm.enabled = true;
        bombaInv.enabled = false;
        bombaMult.enabled = false;
        count = 1;
    }
}
