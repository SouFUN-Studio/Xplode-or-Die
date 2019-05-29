using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Retrieval : MonoBehaviour
{
    string highscore_url = "http://www.soufunstudio.com/data.php";
    string playName = "Player 1";
    int score = -1;
    private readonly string game = "XorD";
    UnityWebRequest download;

    void Start()
    {
        // A correct website page.
        StartCoroutine(GetRequest("https://www.soufunstudio.com/datajson.php"));

        // A non-existing page.
        StartCoroutine(GetRequest("https://error.html"));
    }
    // Use this for initialization
    public IEnumerator NewRecord(int score, string playName)
    {
        this.score = score;
        this.playName= playName;

        // Create a form object for sending high score data to the server
        WWWForm form = new WWWForm();

        // Assuming the php script manages high scores for different games
        form.AddField("game", game);

        // The name of the player submitting the scores
        form.AddField("playerName", playName);

        // The score
        form.AddField("score", score);

        // Create a download object
        download = UnityWebRequest.Post(highscore_url, form);

        // Wait until the download is done
        yield return download.SendWebRequest();

        if (download.isNetworkError || download.isHttpError)
        {
            print("Error downloading: " + download.error);
        }
        else
        {
            // show the highscores
            Debug.Log(download.downloadHandler.text);
        }
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
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
    public UnityWebRequest GetDownload()
    {
        return download;
    }
}
