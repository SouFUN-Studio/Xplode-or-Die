using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject bombN;
    public int bombNCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Vector2 spawnValues;

    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /*
    public void ResizeSpriteToScreen()
    {
        SpriteRenderer rs = GetComponent<SpriteRenderer>();
    if(rs == null) return;

        transform.localScale = new Vector3(1, 1, 1);
        double width = rs.sprite.bounds.size.x;
        double height = rs.sprite.bounds.size.y;

        double worldScreenHeight = Camera.main.orthographicSize * 2.0;
        double worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3((float)(worldScreenWidth / width),
                                (float)(worldScreenHeight / height),
                                1);
    }*/
}
