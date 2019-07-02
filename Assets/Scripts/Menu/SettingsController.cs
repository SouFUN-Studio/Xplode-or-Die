using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [Header("Audio")]
    public Button musicButton;
    public Button FXButton;
    public AudioSource music;
    public AudioSource[] FXs; 
    public Sprite soundOn;
    public Sprite soundOff;

    [Header("Language")]
    public TextMeshProUGUI[] textListD;
    public Text[] textListH;
    public TextMeshProUGUI[] textListB;
    public TextMeshPro[] textTutorials;

    private string[,] tmpText;
    private string[,] uguiText;
    private string[,] uguiWords;
    private string[,] uguitutorial;
    private bool fxEnabled;
    private int lang = 1;
    private bool tutorial = false;

    private void Start()
    {
        tmpText = new string[2, 7]
        {//SPANISH
            {"Desliza el dedo sobre la bomba para destruirla","Desliza el dedo sobre la bomba varias veces. Esta bomba es lenta peor muy dura",
                "Cuidado esta bomba es muy rapida", "Congela a todas las demas bombas en pantalla", "Destruye a las bombas cercanas a ella",
                "¿Estas seguro de que quieres salir?", "\nProgramador Roberto Torres\n \n Arte y Diseño Elizabeth Librado\n \n Desarrollador iOS German D. Perez\n \n Musica y Efectos Bit Girls Music",               
            },
         //ENGLISH
            {"Slide your finger on the bomb to destroy it "," Slide your finger on the bomb several times. This pump is slow worse, very hard",
                "Beware this bomb is very fast", "Freeze all the other bombs on screen", "Destroy the bombs near it", "Are you sure you want to exit?",
            "\nProgrammer Roberto Torres\n \n Art and Design Elizabeth Librado\n \n iOS Developer German D. Perez\n \n Music and FX Bit Girls Music",
            
            }
        };
        uguiText = new string[2, 11]
        {
            {"AYUDA", "AJUSTES", "CREDITOS","MARCADOR","PAUSA","Cancelar","Salir","Guardar","Si", "No", "Cancelar" },
            {"HELP", "SETTINGS", "CREDITS", "HIGHSCORES", "PAUSE","Cancel","Exit","Save","Yes","No","Cancel" }
        };
        uguiWords = new string[2, 8]
        {
            {"Cambiar mombre","Musica","Efectos","Record Mundial","Score","Combo", "Record Personal", "Lenguaje"},
            {"Change name","Music","Fx","World Record","Score","Combo", "Player Highscore", "Language"}
        };
        uguitutorial = new string[2, 5]
        {//SPANISH
            {"Desliza el dedo sobre la bomba", "Algunas bombas pueden destruir las bombas al rededor de ella", "Algunas bombas pueden congelar a las demas bombas por 3 segundos",
                "Esta bomba es muy rapida, cudado", "Desliza el dedo sobre la bomba varias veces" },
            //ENGLISH
            {
                "Slide your finger over the bomb", "Some bombs can destroy the bombs arround it", "Some bombs can frezze the other bombs for 3 seconds",
            "This bomb is very fast, Be careful", "Slide your finger on the bomb several times"
            }
        };

        SelectLanguage(lang);
    }

    public void SelectLanguage(int lang)
    {
        this.lang = lang;
        for (int count = 0; count < textListD.Length; count++)
        {
            textListD[count].text = tmpText[lang, count];
        }
        for (int count = 1; count < textListH.Length; count++)
        {
            textListH[count-1].text = uguiText[lang, count/2];
            textListH[count].text = uguiText[lang, count/2];
            count++;
        }
        for (int count = 0; count < textListB.Length; count++)
        {
            textListB[count].text = uguiWords[lang, count];
        }
        for (int count = 0; count < textTutorials.Length; count++)
        {
            textTutorials[count].text = uguitutorial[lang, count];
        }
        SaveSettings(music.enabled, fxEnabled, lang, tutorial);
    }


    private void Awake()
    {
        string jsonString = PlayerPrefs.GetString("playerSettings");
        //PlayerPrefs.DeleteKey("playerSettings"); //DELETE PLAYERSETTINGS DATABASE
        PlayerSettings playerSettings = JsonUtility.FromJson<PlayerSettings>(jsonString);
        if (playerSettings == null)
        {
            SaveSettings(true, true, 1, false);
            //reload
            jsonString = PlayerPrefs.GetString("playerSettings");
            playerSettings = JsonUtility.FromJson<PlayerSettings>(jsonString);
        }
        UpdateSettings();
    }
    public void UpdateSettings()
    {
        string jsonString = PlayerPrefs.GetString("playerSettings");

        PlayerSettings playerSettings = JsonUtility.FromJson<PlayerSettings>(jsonString);
        //Debug.Log(playerSettings.music + " "+ playerSettings.FX);
        if (playerSettings.music)
        {
            musicButton.GetComponent<Image>().sprite = soundOn;
            music.enabled = true;
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = soundOff;
            music.enabled = false;
        }
        if (playerSettings.FX)
        {
            fxEnabled = true;
            FXButton.GetComponent<Image>().sprite = soundOn;
            foreach (AudioSource fx in FXs)
            {
                fx.enabled = true;
            }
        }
        else{
            fxEnabled = false;
            FXButton.GetComponent<Image>().sprite = soundOff;
            foreach (AudioSource fx in FXs)
            {
                fx.enabled = false;
            }
        }
        
        lang = playerSettings.lang;
        tutorial = playerSettings.tutorial;
        Debug.Log("Tutorial eneabled: " + playerSettings.tutorial);
    }
    public void MusicEnabled()
    {
        if (music.enabled) {
            musicButton.GetComponent<Image>().sprite =  soundOff;
            music.enabled = false;
        }else
        {
            musicButton.GetComponent<Image>().sprite = soundOn;
            music.enabled = true;
        }
        SaveSettings(music.enabled, fxEnabled, lang, tutorial);
    }
    public void FXEnabled()
    {
        if (fxEnabled)
        {
            fxEnabled = false;
            FXButton.GetComponent<Image>().sprite = soundOff;
            foreach (AudioSource fx in FXs)
            {
                fx.enabled = false;
            }
        }
        else
        {
            fxEnabled = true;
            FXButton.GetComponent<Image>().sprite = soundOn;
            foreach (AudioSource fx in FXs)
            {
                fx.enabled = true;
            }
        }
        SaveSettings(music.enabled, fxEnabled, lang, tutorial);
    }

    public bool IsFXEnabled()
    {
        return fxEnabled;
    }

    public void SaveSettings(bool music, bool fx, int lang, bool tutorial)
    {
        //Create playerSettings
        PlayerSettings playerSettings = new PlayerSettings { music = music, FX = fx, lang = lang, tutorial = tutorial};

        //Load saved Settings
        string jsonString = PlayerPrefs.GetString("playerSettings");
        PlayerSettings Settings = JsonUtility.FromJson<PlayerSettings>(jsonString);
        if(Settings == null)
        {
            Settings = new PlayerSettings() { music = true, FX = true, lang = 1, tutorial = tutorial};
        }
        //Update audio settings
        Settings = playerSettings;

        //Save updated settings
        string json = JsonUtility.ToJson(Settings);
        PlayerPrefs.SetString("playerSettings", json);
        PlayerPrefs.Save();
    }
    
    [System.Serializable]
    private class PlayerSettings
    {
        public bool music;
        public bool FX;
        public int lang;
        public bool tutorial;
    }
    
    public bool GetTutorialStatus()
    {
        return tutorial;
    }

    public void SetTutorialStatus(bool tutorial)
    {
        this.tutorial = tutorial;
        SaveSettings(music.enabled, fxEnabled, lang, tutorial);
    }


}