﻿/* 
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

public class HighscoreTable : MonoBehaviour {

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake() {
        entryContainer = transform.Find("Highscore Entry Container");
        entryTemplate = entryContainer.Find("Highscore Entry Template");

        entryTemplate.gameObject.SetActive(false);
//        PlayerPrefs.DeleteKey("highscoreTable");
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null) {
            // There's no stored table, initialize
            Debug.Log("Initializing table with default values...");
            AddHighscoreEntry(100);
            AddHighscoreEntry(200);
            AddHighscoreEntry(300);
            AddHighscoreEntry(400);
            AddHighscoreEntry(500);
            AddHighscoreEntry(600);
            // Reload
            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }

    }

    public void UpdateScore()
    {
        entryContainer = transform.Find("Highscore Entry Container");
        entryTemplate = entryContainer.Find("Highscore Entry Template");

        entryTemplate.gameObject.SetActive(false);
        //        PlayerPrefs.DeleteKey("highscoreTable");
        string jsonString = PlayerPrefs.GetString("highscoreTable");

        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores = SortScoreList(highscores);

        highscoreEntryTransformList = new List<Transform>();
        int count = 0;
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            if(count >= 15)
            {
                break;
            }else
                CreateHighscoreEntryTransform(highscores.highscoreEntryList[count], entryContainer, highscoreEntryTransformList);
            count++;
        }
    }

    public void DestroyTemplates()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("TemplateCopy");
        foreach (GameObject d in gameObjects)
        {
            Destroy(d);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 51f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        entryTransform.tag = "TemplateCopy";
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
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

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        //string name = highscoreEntry.name;
        //entryTransform.Find("nameText").GetComponent<Text>().text = name;

        // Set background visible odds and evens, easier to read
           //entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);
        
        // Highlight First
        if (rank == 1) {
            entryTransform.Find("posText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;
         //   entryTransform.Find("nameText").GetComponent<Text>().color = Color.green;
        }

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
    }

    public void AddHighscoreEntry(int score) {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score };
        
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

    public string GetFirstScore()
    {
        Highscores highscores = JsonUtility.FromJson<Highscores>(PlayerPrefs.GetString("highscoreTable"));
        highscores = SortScoreList(highscores);

        return highscores.highscoreEntryList[0].score.ToString();
    }

    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /*
     * Represents a single High score entry
     * */
    [System.Serializable] 
    private class HighscoreEntry {
        public int score;
    }

}
