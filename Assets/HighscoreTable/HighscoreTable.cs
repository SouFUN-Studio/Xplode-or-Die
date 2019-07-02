/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;
using System.Text.RegularExpressions;

public class HighscoreTable : MonoBehaviour {

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private Transform entryPlayerTemplate;
    public ScrollRect scrollRect;
    public Sprite sp;
    public Sprite[] sprites;
    public TMP_InputField TMP;

    public void Awake()
    {
        TMP.characterLimit = 15;
        TMP.text = GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetPlayerName();

    }

    public void ShowScores()
    {
        
        //PlayerPrefs.DeleteKey("highscoreTable"); //DELETE Players DATABASE    
        GameObject.Find("DatabaseManager").GetComponent<Retrieval>().PostRequest();
        entryContainer = transform.Find("Highscore Entry Container");
        entryTemplate = entryContainer.Find("Highscore Entry Template");
        //ASDASDSADASDASDSADASDASDASDASDASDASD
        entryPlayerTemplate = GameObject.Find("Player Entry Template").transform;
        entryTemplate.gameObject.SetActive(false);
        //Debug.Log("LoadScores...");
        LoadStoredPlayersToHighscores();
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        //Debug.Log(PlayerPrefs.GetString("highscoreTable"));
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        bool conected = GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetConected();

        if (highscores == null)
        {
            // There's no stored table, initialize
            //Debug.Log("Initializing table with default values...");
            AddHighscoreEntry(1, 600, "Guest01", 0);
            AddHighscoreEntry(2, 500, "Guest02", 5);
            AddHighscoreEntry(3, 400, "Guest03", 10);
            AddHighscoreEntry(4, 300, "Guest04", 8);
            AddHighscoreEntry(5, 200, "Guest05", 2);
            AddHighscoreEntry(6, 100, "Guest06", 13);
            // Reload
            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }
        else
        {
            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }
    }

    public void UpdateScore()
    {
        
        DestroyTemplates();
        ShowScores();
        entryContainer = transform.Find("Highscore Entry Container");
        entryTemplate = entryContainer.Find("Highscore Entry Template");

        entryTemplate.gameObject.SetActive(false);
        string jsonString = PlayerPrefs.GetString("highscoreTable");

        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores = SortScoreList(highscores);

        highscoreEntryTransformList = new List<Transform>();

        HighscoreEntry playerScore = new HighscoreEntry()
        {
            id = GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetID(),
            score = GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetScore(),
            player = GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetPlayerName()
        };

        if (playerScore.player == "Guest")
            playerScore.player = playerScore.player +(9999 - GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetID());


        entryContainer.GetComponent<RectTransform>().sizeDelta = new Vector2 (100, 50 + (50*highscores.highscoreEntryList.Count));
        int count = 0;
        int startInstantiate = highscores.highscoreEntryList.Count;
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            if (count >= 99)
            {
                break;
            }
            else
            {
                CreateHighscoreEntryTransform(highscores.highscoreEntryList[count], entryContainer, highscoreEntryTransformList, playerScore.id, startInstantiate);
                count++;
            }
        }
        entryPlayerTemplate.Find("scoreText").GetComponent<TextMeshProUGUI>().SetText(playerScore.score.ToString());
        entryPlayerTemplate.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(playerScore.player);
        //        entryPlayerTemplate.Find("profilePhoto").GetComponent<Image>().sprite = GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetProfilePhoto();
        scrollRect.verticalNormalizedPosition = 1.0f;
    }

    public void DestroyTemplates()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("TemplateCopy");
        foreach (GameObject d in gameObjects)
        {
            Destroy(d);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList, int playerID, int startInstansiate) {
        float templateHeight = 50f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        entryTransform.tag = "TemplateCopy";
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0,((startInstansiate * 50)/2 -25) -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank) {
        default:
            rankString = rank + "th"; break;

        case 1: rankString = "1st"; break;
        case 2: rankString = "2nd"; break;
        case 3: rankString = "3rd"; break;
        }

        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().SetText(rankString);

        int score = highscoreEntry.score;
        string name = highscoreEntry.player;
        int randomSP = highscoreEntry.profileImg;
        //string score = GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetDownload().downloadHandler.text;

        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().SetText(score.ToString());
        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(name);
        entryTransform.Find("profilePhoto").GetComponent<Image>().sprite = sprites[randomSP];
        //string name = highscoreEntry.name;
        //entryTransform.Find("nameText").GetComponent<Text>().text = name;

        // Set background visible odds and evens, easier to read
           //entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);        

        // Set tropy
        switch (rank) {
        default:
            entryTransform.Find("trophy").gameObject.SetActive(false);
            break;
        case 1:
            entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("FFD200");
            break;
        case 2:
            entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("C6C6C6");
            break;
        case 3:
            entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("B76F56");
            break;

        }

        transformList.Add(entryTransform);

        if ( playerID == highscoreEntry.id)
        {
            entryPlayerTemplate.Find("posText").GetComponent<TextMeshProUGUI>().SetText(rankString);
            entryPlayerTemplate.Find("profilePhoto").GetComponent<Image>().sprite = sprites[randomSP];
        }

    }

    public void SetPlayName()
    {
        string newName = TMP.text;
        newName = QuitarEspaciosCadena(newName);
        if (newName != "")
        {
            if (newName == "Guest")
            {
                GameObject.Find("DatabaseManager").GetComponent<Retrieval>().UploadMyName(newName
                    + (9999 - GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetID()));
                entryPlayerTemplate.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(newName
                    + (9999 - GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetID()));
            }
            else
            {

                GameObject.Find("DatabaseManager").GetComponent<Retrieval>().UploadMyName(newName);
                entryPlayerTemplate.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(newName);
            }
        }
        TMP.text = newName;
    }

    private string QuitarEspaciosCadena(string cadena)
    {
        // -----------------------------------------------------------------------
        // \s -> Coincide con cualquier carácter que sea un espacio en blanco
        // \S -> Coincide con cualquier carácter que no sea un espacio en blanco.
        // http://msdn.microsoft.com/es-es/library/20bw873z(v=vs.90)
        // -----------------------------------------------------------------------
        Regex espacio = new Regex(@"\s+");
        cadena = espacio.Replace(cadena, "");
        return cadena;
    }


    public void AddHighscoreEntry(int id, int score, string name, int spriteImg) {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry {id = id, score = score, player=name, profileImg=spriteImg  };
        
        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null) {
            // There's no stored table, initialize
            highscores = new Highscores() {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private Highscores SortScoreList(Highscores highscores)
    {
        // Sort entry list by Score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }
        return highscores;
    }

    public int GetFirstScore()
    {
        //Debug.Log(PlayerPrefs.GetString("highscoreTable"));
        Highscores highscores = JsonUtility.FromJson<Highscores>(PlayerPrefs.GetString("highscoreTable"));
        highscores = SortScoreList(highscores);

        return highscores.highscoreEntryList[0].score;
    }
    public void LoadStoredPlayersToHighscores()
    {
        int [] i = GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetPlayersIDs();
        int[] p = GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetPlayersScores();
        string[] n = GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetPlayersName();
        int[] s = GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetPlayersImg();
        //Debug.Log(PlayerPrefs.GetString("highscoreTable"));
        if (p != null)
        {
        PlayerPrefs.DeleteKey("highscoreTable"); //DELETE SCORE DATABASE    
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
            for (int count = 0; count < p.Length; count++)
            {
                int randomSp = (int)Random.Range(0, 13);
                AddHighscoreEntry(i[count], p[count], n[count], randomSp);
            }

        }
    }
    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /*
     * Represents a single High score entry
     * */
    [System.Serializable] 
    private class HighscoreEntry {
        public int id;
        public int profileImg;
        public string player;
        public int score;
    }

}
