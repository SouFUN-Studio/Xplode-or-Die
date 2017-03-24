using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour {

	public GameObject hazzard0;
	public GameObject hazzard1;
    public GameObject hazzard2;
	public Vector3 spawnValues;
	public int hazzardCount;
	//public int hazzardCount2;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public Quaternion spawnRotation;
    //public Quaternion spawnRotationDeimos;
    private GameObject[] hazzardArray = new GameObject[3];
	void Start (){
		StartCoroutine (spawnWaves ());
		Debug.Log ("Termino la rutina");
        hazzardArray[0] = hazzard0;
        hazzardArray[1] = hazzard1;
        hazzardArray[2] = hazzard2;
	}

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
}
