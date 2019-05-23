using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameController : MonoBehaviour {
    [Header("Destroy by Contact Script")]
    // Object that contain the destroy boundary script
    public DestroyByContact DBC;

    [Header("Resize Background Script")]
    // Object that contain the resize collider
    public ResizeSpriteToScreen RSTS;

    [Header("Canvas")]
    //GameOver canvas 
    public Canvas GameOver;
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

    public TextMesh textCombo;

    [Header("Animation State")]
    public int count;

    readonly string highScore;

    private GameObject[] hazzardArray = new GameObject[3];
    private GameObject[] enemyArray;

    private  int currentScore;
    private int score;
    private int currentCombo;

    private float speedUp = 0f;

	void Start (){
        hazzardArray[0] = hazzard0;
        hazzardArray[1] = hazzard1;
        hazzardArray[2] = hazzard2;
        count = (allies.Length * 30) / allies.Length;
        currentCombo = 0; 
    }

    private void Update()
    {
        textHUD.text = "Score: " + score.ToString();
        textCombo.text = currentCombo.ToString();
        currentScore = score;
        if (DBC.GetLifes() == 0)
        {
            GameObject.Find("AdsController").GetComponent<UnityAdsPlacement>().ShowAd();
            GameOver.enabled = true;
            StopHazzardSpawn();
            RSTS.reSizeCollider();
            gameoverScore.text = currentScore.ToString();
            ResetGame();
            DBC.SetLifes(3);
        }
        count = (allies.Length*30)/allies.Length;
    }

    /* time to launch waves */

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
            speedUp += 0.5f;
		}
	}

    /* Start the hazzard coroutine 
     * */
    public void StartHazzardSpawn()
    {
        StartCoroutine(SpawnWaves());
    }

    /* Stop the hazzard coroutine
     * */

    public void StopHazzardSpawn()
    {
        StopAllCoroutines();
    }

    public void SetScoreCount(int score)
    {
        this.score += score;
        currentScore = score;
    }

    /* Destroy all bombs
     * */
     public void DestroyBombs()
    {
        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject bomb in enemyArray)
        {
            bomb.GetComponent<HazzardMover>().SetDestroy(true);
        }
    }

    /*
     * Destroy blocks of allies
     */
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

    /* Reset scores
     */
     public void ResetScores()
    {
        textHUD.text = "Score: 0";
        score = 0;
    }

    /*Reset Game
     */
     public void ResetGame()
    {
        ResetScores();
        DestroyBombs();
    }

    public void ResetCombo()
    {
        currentCombo = 0;
    }

    public void AddCombo(int currentCombo)
    {
        this.currentCombo = this.currentCombo + currentCombo;
    }

    public void SetComboPosition(Vector3 newPosition)
    {
        combo.GetComponent<Transform>().position = newPosition;
    }

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
    /***************************************************************************
     *                                                                         *
     *                           SAVE GAME RESOURCES                           *
     *                                                                         *
     **************************************************************************/

    
}
