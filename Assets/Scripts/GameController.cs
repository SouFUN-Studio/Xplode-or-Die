using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour {
    // Object that contain the destroy boundary script
    public DestroyByContact DBC;

    // Object thsst contain the resize collider
    public ResizeSpriteToScreen RSTS;
    
    public Canvas GameOver;

	public GameObject hazzard0;
	public GameObject hazzard1;
    public GameObject hazzard2;
	public Vector3 spawnValues;
	public int hazzardCount;
    public int life;
	//public int hazzardCount2;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public Quaternion spawnRotation;
    //public Quaternion spawnRotationDeimos;
    private GameObject[] hazzardArray = new GameObject[3];

	void Start (){
        hazzardArray[0] = hazzard0;
        hazzardArray[1] = hazzard1;
        hazzardArray[2] = hazzard2;
	}

    private void Update()
    {
        if (DBC.getLifes() == 0)
        {
            GameOver.enabled = true;
            StopHazzardSpawn();
            RSTS.reSizeCollider();
        }
    }

    /* time to launch waves */

    IEnumerator spawnWaves () {
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazzardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                
				Instantiate (hazzardArray[(int)Random.Range(0.0f,2.3f)], spawnPosition, spawnRotation);
				//Instantiate (hazzard2, spawnPosition, spawnRotationDeimos);
				yield return new WaitForSeconds (spawnWait);
			}

			yield return new WaitForSeconds (waveWait);
		}
	}

    /* Start the hazzard coroutine 
     * */
    public void StartHazzardSpawn()
    {
        StartCoroutine(spawnWaves());
    }

    /* Stop the hazzard coroutine
     * */

    public void StopHazzardSpawn()
    {
        StopAllCoroutines();
    }
}
