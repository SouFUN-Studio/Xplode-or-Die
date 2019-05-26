using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

    /*************************************
     *                                   *
     *         PUBLIC VARIABLES          *
     *                                   *
     ************************************/
    [Header("Destroy by Contact Script")]
    // Object that contain the destroy boundary script
    public DestroyByContact DBC;

    [Header("Resize Background Script")]
    // Object that contain the resize collider
    public ResizeSpriteToScreen RSTS;

    [Header("Canvas")]
    //GameOver canvas 
    public Canvas GameOver;

    public HighscoreTable HighscoreTable;
    //combo NOT CREATED YET
    public GameObject combo;
    [Header("Gelatux")]
    public GameObject[] allies;
    [Header("Bombs")]
    public GameObject hazzard0;
	public GameObject hazzard1;
    public GameObject hazzard2;
    [Header("Spawn Settings")]
    public Vector3 spawnValues;
    public int hazzardCount;
    public float spawnWait;
	public float startWait;
	public float waveWait;
	public Quaternion spawnRotation;

    [Header("Current score in playmode")]
    public Text textHUD;

    [Header("Gameover text's")]
    public Text gameoverScore;
    public Text highscore;

    public TextMesh textCombo;

    [Header("Animation State")]
    public int count;

    /*************************************
     *                                   *
     *         PRIVATE VARIABLES         *
     *                                   *
     ************************************/

    readonly string highScore;

    private GameObject[] hazzardArray = new GameObject[3];
    private GameObject[] enemyArray;

    private  int currentScore;
    private int score;
    private int currentCombo;

    private float speedUp = 0f;

    /*************************************
     *                                   *
     *              START                *
     *                                   *
     ************************************/

    void Start (){
        hazzardArray[0] = hazzard0;
        hazzardArray[1] = hazzard1;
        hazzardArray[2] = hazzard2;
        count = (allies.Length * 30) / allies.Length;
        currentCombo = 0; 
    }

    /*************************************
     *                                   *
     *              UPDATE               *
     *                                   *
     ************************************/
    private void Update()
    {
        textHUD.text = "Score: " + score.ToString();
        textCombo.text = currentCombo.ToString();
        currentScore = score;

        //GAMEOVER 
        if (DBC.GetLifes() == 0)
        {
            HighscoreTable.AddHighscoreEntry(currentScore);

            GameObject.Find("AdsController").GetComponent<UnityAdsPlacement>().ShowAd();
            GameOver.enabled = true;
            StopHazzardSpawn();
            RSTS.ReSizeCollider();
            highscore.text = HighscoreTable.GetFirstScore();
            gameoverScore.text = currentScore.ToString();
            ResetGame();
            DBC.SetLifes(3);
        }
        count = (allies.Length*30)/allies.Length;


        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (GameObject.Find("Main Menu Canvas").GetComponent<Canvas>().isActiveAndEnabled)
                GameObject.Find("Exit Canvas").GetComponent<Canvas>().enabled = true;

            if (GameObject.Find("Highscore Canvas").GetComponent<Canvas>().isActiveAndEnabled)
            {
                GameObject.Find("Highscore Canvas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Main Menu Canvas").GetComponent<Canvas>().enabled = true;
            }

            if (GameObject.Find("Help Menu Canvas").GetComponent<Canvas>().isActiveAndEnabled && Input.GetKeyDown(KeyCode.Escape))
            {
                GameObject.Find("Help Menu Canvas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Main Menu Canvas").GetComponent<Canvas>().enabled = true;
                //Extra Code to restart the help menu configuration
                GetComponent<HelpMenu>().ResetHelpMenu();
            }

            if (GameObject.Find("Credits Canvas").GetComponent<Canvas>().isActiveAndEnabled && Input.GetKeyDown(KeyCode.Escape))
            {
                GameObject.Find("Credits Canvas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Main Menu Canvas").GetComponent<Canvas>().enabled = true;
            }
        }
            
        
    }

    /*************************************
     *                                   *
     *         BOMBS CONTROLLER          *
     *                                   *
     ************************************/

    /* Coroutine*/
    IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazzardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                
				Instantiate (hazzardArray[(int)Random.Range(0.0f,2.3f)], spawnPosition, spawnRotation);
				//Instantiate (hazzard2, spawnPosition, spawnRotationDeimos);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
            for(int a = 0; a < hazzardArray.Length; a++)
            {
                hazzardArray[a].GetComponent<HazzardMover>().SetNewSpeed(speedUp);
            }
            speedUp += 0.3f;
            spawnWait -= 0.05f;
		}
	}

    /* Destroy bombs */

    public void DestroyBombs()
    {
        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject bomb in enemyArray)
        {
            bomb.GetComponent<HazzardMover>().SetDestroy(true);
        }
    }
     
     /* Start the hazzard coroutine*/
     
    public void StartHazzardSpawn()
    {
        StartCoroutine(SpawnWaves());
    }

    /* Stop the hazzard coroutine*/

    public void StopHazzardSpawn()
    {
        StopAllCoroutines();
    }

    public void SetSpeedUp(float speedUp)
    {
        this.speedUp = speedUp;
    }
    

    /*************************************
     *                                   *
     *         ALLIES CONTROLLER         *
     *                                   *
     ************************************/
    public void SpawnAllies()
    {
        int currentCount = GameObject.FindGameObjectsWithTag("Ally").Length;

        if (currentCount == 0)
        {
            foreach (GameObject ally in allies)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Instantiate(ally, spawnPosition, spawnRotation);
            }
        }
        else
        {
            for(int rest = currentCount; rest < allies.Length; rest++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Instantiate(allies[rest], spawnPosition, spawnRotation);
            }
        }
    }

    public int getCount()
    {
        return count;
    }
    public void DestroyAllies(int lifes)
    {
        //Debug.Log("Lifes: " + lifes);
        int aux =  (allies.Length/3)*lifes;
       // Debug.Log("Aux: " + aux);
        GameObject[] deadAllies = GameObject.FindGameObjectsWithTag("Ally");
        for (int i = aux; i < deadAllies.Length; i++)
        {

            deadAllies[i].GetComponent<Movement>().SetDestroy(true);

        }
    }

    /*************************************
     *                                   *
     *         SCORE CONTROLLER          *
     *                                   *
     ************************************/
    public void ResetScores()
    {
        textHUD.text = "Score: 0";
        score = 0;
    }

    public void SetScoreCount(int score)
    {
        this.score += score;
        currentScore = score;
    }



    /*************************************
     *                                   *
     *         COMBO CONTROLLER          *
     *                                   *
     ************************************/

    public void AddCombo(int currentCombo)
    {
        this.currentCombo = this.currentCombo + currentCombo;
    }

    public void SetComboPosition(Vector3 newPosition)
    {
        combo.GetComponent<Transform>().position = newPosition;
    }
    public void ResetCombo()
    {
        currentCombo = 0;
    }

    /*************************************
     *                                   *
     *       RESTART PLAYING GAME        *
     *                                   *
     ************************************/
    public void ResetGame()
    {
        spawnWait = 1f;
        speedUp = 0f;
        DestroyBombs();
        ResetScores();
    }
    /*************************************
     *                                   *
     *         APLICATION CONTROLLER     *
     *                                   *
     ************************************/
    public void CloseApplication()
    {
        Application.Quit();
    }
    
}
