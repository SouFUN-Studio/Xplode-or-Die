using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Retrieval : MonoBehaviour
{
    /****************************************
     *                                      *
     *            VARIABLES                 *
     *                                      *
     ***************************************/ 
    readonly string highscore_url = "http://www.soufunstudio.com/data.php"; //POST REQUEST PAGE
    readonly string hignscore_json = "http://www.soufunstudio.com/datajson.php"; //GET REQUEST PAGE
    private int id = -1;
    private readonly string game = "XorD";
    private string playName = "Player 1";
    private int score = -1;
    private PlayerEntry myInfo;
    private Players players;
    private bool conected=false;
    UnityWebRequest download;

    /****************************************
     *                                      *
     *             RUN ON START             *
     *                                      *
     ***************************************/
    void Awake()
    {
        LoadMyInfo();
        PostRequest();
        //GetRequest();
    }

    /****************************************
     *                                      *
     *                 POST                 *
     *                                      *
     ***************************************/

    public void PostRequest()
    {
        IEnumerator POST = PostRequest(highscore_url);
        StartCoroutine(POST);
        //StopCoroutine(POST);
    }

    public IEnumerator PostRequest(string url)
    {
        Debug.Log("New Score to be stored: " + score);
        // Create a form object for sending high score data to the server
        WWWForm form = new WWWForm();

        // Assuming the php script manages id for diferent players
        form.AddField("id", id);

        // Assuming the php script manages high scores for different games
        form.AddField("game", game);

        // The name of the player submitting the scores
        form.AddField("playerName", playName);

        // The score
        form.AddField("score", score);

        // Create a download object
        download = UnityWebRequest.Post(url, form);
        
        // Wait until the download is done
        yield return download.SendWebRequest();
        //Debug.Log("Response for POST method " + download.downloadHandler.text);
        if (download.isNetworkError || download.isHttpError)
        {
            conected = false;
    
        }
        else
        {
            Debug.Log("conected");
            conected = true;
            if (id == -1)
            {
                // show the id
                Debug.Log("id recived: " + download.downloadHandler.text);
                //Debug.Log(download.downloadHandler.text);
                SetID(System.Convert.ToInt32(download.downloadHandler.text));
                Debug.Log("Id converted!");
                UploadMyID(id);
            }
            GetRequest();
        }
    }

    /****************************************
     *                                      *
     *                  GET                 *
     *                                      *
     ***************************************/

    public void GetRequest()
    {
        // A correct website page.
        StartCoroutine(GetRequest(hignscore_json));
        // A non-existing page.
        StartCoroutine(GetRequest("http://error.html"));
    }


    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                conected = false;
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                conected = true;
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                //TextAsset asset = webRequest.downloadHandler.text as TextAsset;
                string playersInfo = PlayerPrefs.GetString("playersInfoTable");
                PlayerPrefs.DeleteKey("playersInfoTable"); //DELETE Players DATABASE    
                players = JsonUtility.FromJson<Players>(webRequest.downloadHandler.text.ToString());
                string json = JsonUtility.ToJson(players);
                PlayerPrefs.SetString("playersInfoTable", json);
                PlayerPrefs.Save();
                Debug.Log("Players from database stored... :D");
            }
        }
    }

    /****************************************
     *                                      *
     *           LOAD PLAYER INFO           *
     *                                      *
     ***************************************/

    public void LoadMyInfo()
    {
        //Create my new info
        Debug.Log("Loading Player info...");
        //Load my info
        PlayerPrefs.DeleteKey("playerInfoTable"); //DELETE SCORE DATABASE    
        string playerInfo = PlayerPrefs.GetString("playerInfoTable");
        myInfo = JsonUtility.FromJson<PlayerEntry>(playerInfo);
        if (myInfo == null)
        {
            PlayerEntry myNewInfo = new PlayerEntry { id = this.id, game = this.game, player = this.playName, score = this.score };
            Debug.Log("No data founded...");
            Debug.Log("Creating new Player info...");
            // There's no stored table, initialize
            myInfo = myNewInfo;//new PlayerEntry { id = this.id, game = "XorD", player = "NewPlayer", score = this.score };
            Debug.Log("Info created...");
            Debug.Log("[ My Id: " + myInfo.id + "\n My score: " + myInfo.score + " ]" );
            Debug.Log("My Info loaded...");
            string json = JsonUtility.ToJson(myInfo);
            PlayerPrefs.SetString("playerInfoTable", json);
            PlayerPrefs.Save();

        }
        else
        {
            this.id = myInfo.id;
            this.playName = myInfo.player;
            this.score = myInfo.score;
            Debug.Log("[ My Id: " + myInfo.id + "\n My score: " + myInfo.score + " ]");
            Debug.Log("My Info loaded...");
        }
    }

    public void UploadMyScore(int score)
    {
        this.score = score;
        string playerInfo = PlayerPrefs.GetString("playerInfoTable");
        myInfo = JsonUtility.FromJson<PlayerEntry>(playerInfo);
        this.myInfo.score = score;
        // Save updated Player Info
        string json = JsonUtility.ToJson(myInfo);
        PlayerPrefs.SetString("playerInfoTable", json);
        PlayerPrefs.Save();
    }

    public void UploadMyID(int id)
    {
        this.id = id;
        string playerInfo = PlayerPrefs.GetString("playerInfoTable");
        if(playerInfo == null)
        this.myInfo = JsonUtility.FromJson<PlayerEntry>(playerInfo);
        this.myInfo.id = id;
        // Save updated Player Info
        string json = JsonUtility.ToJson(myInfo);
        PlayerPrefs.SetString("playerInfoTable", json);
        PlayerPrefs.Save();
    }

    /****************************************
     *                                      *
     *             GET AND SET              *
     *                                      *
     ***************************************/

    public void SetID(int id)
    {
        Debug.Log("ID converted to int: [value]: " + id);
        this.id = id;
    }
    public int GetScore()
    {
        return score;
    }
    public bool GetConected()
    {
        return conected;
    }

    public int[] GetPlayersScores()
    {
        string playersInfo = PlayerPrefs.GetString("playersInfoTable");
        players = JsonUtility.FromJson<Players>(playersInfo);

        if (players != null)
        {
            int size = players.PlayerList.Count;
            int[] scores = new int[size];
            for (int i = 0; i < size; i++)
            {
                scores[i] = players.PlayerList[i].score;
                //Debug.Log("Scores: "+players.PlayerList[i].score);
            }
        return scores;
        }
        return null;
    }

    public int GetPlayerScore()
    {
        return score;
    }

    /****************************************
     *                                      *
     *               PLAYER                 *
     *                                      *
     ***************************************/

    public class Players
    {
        public List<PlayerEntry> PlayerList;
    }
    [System.Serializable]
    public class PlayerEntry
    {
        public int id = -1;
        public string game;
        public string player;
        public int score;
    }
}
