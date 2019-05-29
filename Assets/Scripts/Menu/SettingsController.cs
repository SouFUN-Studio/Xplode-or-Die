using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    
    public Button musicButton;
    public Button FXButton;
    public AudioSource music;
    public AudioSource[] FXs; 
    public Sprite soundOn;
    public Sprite soundOff;

    private bool fxEnabled;

    private void Awake()
    {
        string jsonString = PlayerPrefs.GetString("playerSettings");
        //PlayerPrefs.DeleteKey("playerSettings"); //DELETE PLAYERSETTINGS DATABASE
        PlayerSettings playerSettings = JsonUtility.FromJson<PlayerSettings>(jsonString);
        if (playerSettings == null)
        {
            SaveSettings(true, true);
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
        Debug.Log(playerSettings.music + " "+ playerSettings.FX);
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
        SaveSettings(music.enabled, fxEnabled);
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
        SaveSettings(music.enabled, fxEnabled);
    }

    public void SaveSettings(bool music, bool fx)
    {
        //Create playerSettings
        PlayerSettings playerSettings = new PlayerSettings { music = music, FX = fx };

        //Load saved Settings
        string jsonString = PlayerPrefs.GetString("playerSettings");
        PlayerSettings Settings = JsonUtility.FromJson<PlayerSettings>(jsonString);
        if(Settings == null)
        {
            Settings = new PlayerSettings() { music = true, FX = true };
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
    }
    
}
 