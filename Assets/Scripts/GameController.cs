using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
//por que unity no puede ejecutar


public class GameController : MonoBehaviour {

    /*************************************
     *                                   *
     *         PUBLIC VARIABLES          *
     *                                   *
     ************************************/
    [Header("Destroy by Contact Script")]
    // Object that contain the destroy boundary script
    public GameObject DBC;

    [Header("Resize Background Script")]
    // Object that contain the resize collider
    public ResizeSpriteToScreen RSTS;

    [Header("Canvas")]
    //GameOver canvas 
    public GameObject GameOver;
    public GameObject HUD;
    public Image newRecordImage;

    public HighscoreTable highscoreTable;
    //combo NOT CREATED YET
    public GameObject comboObject;
    [Header("Gelatux")]
    public GameObject[] allies;
    [Header("Bombs")]
    public GameObject[] hazzardArray;

    [Header("Spawn Settings")]
    public Vector3 spawnValues;
    public int hazzardCount;
    public float spawnWait;
	public float startWait;
	public float waveWait;
	public Quaternion spawnRotation;

    [Header("Current score in playmode")]
    public TextMeshProUGUI textHUD;

    [Header("Gameover")]
    public TextMeshProUGUI gameoverScore;
    public TextMeshProUGUI highscore;
    public TextMeshProUGUI gameoverCombo;
    public AudioSource musicSource;
    public AudioClip gameOverSong;
    public AudioSource bombFx;
    public AudioClip bombExplosion;
    
    [Header("Animation State")]
    public int count;

    [Header("Tutorial")]
    public bool tutorialStatus;
    public GameObject tutorial;
    /*************************************
     *                                   *
     *         PRIVATE VARIABLES         *
     *                                   *
     ************************************/

    readonly string highScore;

    private GameObject[] enemyArray;

    private  int currentScore;
    private int score;
    private int currentCombo;
    private int combo;
    private int maxCombo;
    private float speedUp;
    private Vector2 starMark;
    private Vector2 endMark;
    private float musicSpeed;

    //   private Coroutine bombsCoroutine;

    /*************************************
     *                                   *
     *              START                *
     *                                   *
     ************************************/

    void Start (){
        count = (allies.Length * 30) / allies.Length;
        currentCombo = 0;
        maxCombo = 0;
        speedUp = 0f;
        endMark = GameObject.Find("Destination").GetComponent<Transform>().position;
        starMark= GameObject.Find("Platform").GetComponent<Transform>().position;
        musicSpeed = 1f;
        tutorialStatus = GetComponent<SettingsController>().GetTutorialStatus();
        SetPrefabsFxEnabled();
    }

    /*************************************
     *                                   *
     *              UPDATE               *
     *                                   *
     ************************************/
    private void Update()
    {
        textHUD.SetText("Score: " + (score)); //+ speedUp.ToString();
        comboObject.GetComponentInChildren<TextMesh>().text = combo.ToString();
        currentScore = score;
        currentCombo = combo;
        
        //GAMEOVER 
        if (DBC.GetComponent<DestroyByContact>().GetLifes() == 0)
        {
            GameObject.Find("Platform").GetComponent<PlatformMover>().enabled = false;
            GameObject.Find("Platform").transform.position = GameObject.Find("Start").transform.position;
            musicSource.clip = gameOverSong;
            musicSource.loop = false;
            musicSource.Play();
            musicSpeed = 1f;
            musicSource.outputAudioMixerGroup.audioMixer.SetFloat("Speed", musicSpeed);
            SetSpeedUp(0.0f);
            NewRecord();
            if(GetComponent<SettingsController>().IsFXEnabled())
                bombFx.PlayOneShot(bombExplosion, 0.5f);
            //HighscoreTable.AddHighscoreEntry(currentScore);
            GameObject.Find("AdsController").GetComponent<UnityAdsPlacement>().ShowAd();
            GameOver.SetActive(true);
            GameOver.GetComponent<MenuMover>().SetActive(true);
            StopHazzardSpawn();
            StopCoroutine(StopBombsC());
            RSTS.ReSizeCollider();
            highscore.SetText(highscoreTable.GetFirstScore().ToString());
            gameoverScore.SetText("" + (currentScore));
            gameoverCombo.SetText(maxCombo.ToString());
            ResetGame();
            DBC.GetComponent<DestroyByContact>().SetLifes(3);
        }
        count = (allies.Length*30)/allies.Length;
    }

    /*************************************
     *                                   *
     *         BOMBS CONTROLLER          *
     *                                   *
     ************************************/

    /* Coroutine*/
    IEnumerator SpawnWaves ()
    {
		yield return new WaitForSeconds (startWait);
        
		while (true) {
			for (int i = 0; i < hazzardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                float random = Random.Range(0.2f, 2.58f);
                if (random > 2.45f && random <= 2.5f)
                    random = 3f;
                if (random > 2.5 && random < 3f)
                    random = 4f;
                
                Instantiate (hazzardArray[(int)random], spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
            for(int a = 0; a < hazzardArray.Length; a++)
            {
                hazzardArray[a].GetComponent<HazzardMover>().SetNewSpeed(GetSpeedUP());
            }
            speedUp += 0.3f;
            if(musicSpeed < 1.40f)
                musicSpeed += 0.01f;
            if(spawnWait > 0.5)
                spawnWait -= 0.05f;
		}

	}

    /* Destroy bombs */

    public void DestroyBombs()
    {
        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject bomb in enemyArray)
        {
            bomb.GetComponent<AudioSource>().enabled = false;
            bomb.GetComponent<HazzardMover>().SetDestroy(true);
        }
    }
     
     /* Start the hazzard coroutine*/
     
    public void StartHazzardSpawn()
    {
        
        StartCoroutine("SpawnWaves");
        
    }

    /* Stop the hazzard coroutine*/

    public void StopHazzardSpawn()
    {
        StopCoroutine("SpawnWaves");
    }

    public void SetSpeedUp(float speedUp)
    {
        this.speedUp = speedUp;
    }
    public float GetSpeedUP()
    {
        return this.speedUp;
    }
    public void StopBombs()
    {
        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject bomb in enemyArray)
        {
            bomb.GetComponent<HazzardMover>().currentSpeed = 0.0f;
            bomb.transform.Find("Ice").gameObject.SetActive(true);
        }
        StartCoroutine(StopBombsC());
        
    }
    IEnumerator StopBombsC()
    {
        StopHazzardSpawn();
        yield return new WaitForSeconds(spawnWait);
        startWait = 0.5f;
        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject bomb in enemyArray)
        {
            bomb.GetComponent<HazzardMover>().Start();
            bomb.transform.Find("Ice").gameObject.SetActive(false);
        }
        StartHazzardSpawn();
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

    public int GetCount()
    {
        return count;
    }
    public void DestroyAllies(int lifes)
    {
        int aux =  (allies.Length/3)*lifes;
        GameObject[] deadAllies = GameObject.FindGameObjectsWithTag("Ally");
        for (int i = aux; i < deadAllies.Length; i++)
        {

            deadAllies[i].GetComponent<Movement>().SetDestroy(true);

        }
    }

    public void PauseAudioSource()
    {
        GameObject[] currentAllies = GameObject.FindGameObjectsWithTag("Ally");
        foreach (GameObject ally in currentAllies)
        {
            ally.GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer.SetFloat("Speed", 0.0f);
        }
    }
    public void UnpauseAudioSource()
    {
        GameObject[] currentAllies = GameObject.FindGameObjectsWithTag("Ally");
        foreach (GameObject ally in currentAllies)
        {
            ally.GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer.SetFloat("Speed", 1.38f);
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

    public void NewRecord()
    {
        //Debug.Log("Current score: " + currentScore);
        int storedScore = GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetScore();
        //Debug.Log("Stores score: " + storedScore);
        if (currentScore > storedScore)
        {
            //Debug.Log("New Record ...");
            newRecordImage.enabled = true;
            GameObject.Find("DatabaseManager").GetComponent<Retrieval>().UploadMyScore(currentScore);
            highscoreTable.AddHighscoreEntry(
                GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetID(),
                currentScore,
                GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetPlayerName(),
                GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetProfilePhoto()
                );
            //Debug.Log("New Score: " + GameObject.Find("DatabaseManager").GetComponent<Retrieval>().GetScore());
        }
    }



    /*************************************
     *                                   *
     *         COMBO CONTROLLER          *
     *                                   *
     ************************************/

    public void SetComboCount(int combo)
    {
        this.combo += combo;
        currentCombo = combo;
    }
    public void ResetCombo()
    {
        maxCombo = GetMaxCombo();
        combo = 0;
    }

    public int GetMaxCombo()
    {
        if (maxCombo < currentCombo)
            return maxCombo = currentCombo;
        else
            return maxCombo;
    }

    /*************************************
     *                                   *
     *       RESTART PLAYING GAME        *
     *                                   *
     ************************************/
    public void ResetGame()
    {
        startWait = 3f;
        spawnWait = 1f;
        DestroyBombs();
        ResetScores();
        ResetCombo();
        StopHazzardSpawn();
        StopCoroutine(StopBombsC());
        foreach(GameObject b in hazzardArray)
        {
            b.GetComponent<HazzardMover>().SetNewSpeed(0.0f);
        }
    }

    public void StartGame()
    {
        tutorialStatus = GetComponent<SettingsController>().GetTutorialStatus();
        SetPrefabsFxEnabled();
        if (!tutorialStatus)
        {
            tutorial.SetActive(true);
            HUD.transform.Find("Pause Button").GetComponent<Button>().enabled = false;
            SpawnAllies();
        }
        else
        {
            //InstantiateCombo();
            SetSpeedUp(0.0f);
            SpawnAllies();
            StartHazzardSpawn();
        }
    }
    public void SetAlliesFxEnabled()
    {
        bool enabled = GetComponent<SettingsController>().IsFXEnabled();
        GameObject[] currentAllies = GameObject.FindGameObjectsWithTag("Ally");
        foreach (GameObject ally in currentAllies)
        {
            ally.GetComponent<AudioSource>().enabled = enabled;
        }
    }

    public void SetPrefabsFxEnabled()
    {
        bool enabled = GetComponent<SettingsController>().IsFXEnabled();
        AudioSource[] tutorialBombs = tutorial.GetComponentsInChildren<AudioSource>();
        foreach (AudioSource bombFx in tutorialBombs)
        {
            bombFx.enabled = enabled;
        }
        foreach (GameObject enemy in hazzardArray)
        {
            enemy.GetComponent<AudioSource>().enabled = enabled;
        }
        foreach (GameObject ally in allies)
        {
            ally.GetComponent<AudioSource>().enabled = enabled;
        }
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
