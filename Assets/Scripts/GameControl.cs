using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void onClick()
    {
        Debug.Log("GameObject:" + this.gameObject);
        this.gameObject.AddComponent<TriangleExplosion>();
        StartCoroutine(this.gameObject.GetComponent < TriangleExplosion>().SplitMesh(true));
    }

    public void explosion()
    {
        this.gameObject.AddComponent<TriangleExplosion>();
        StartCoroutine(this.gameObject.GetComponent<TriangleExplosion>().SplitMesh(true));
    }
    public void restartLevel()
    {
        UnityEngine.SceneManagement.LoadSceneMode mode;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0, mode = UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
