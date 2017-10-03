using UnityEngine;
using System.Collections;

public class HazzardMover : MonoBehaviour {

	// Use this for initialization
	public float speed;

    /* Game controller for the arrows initialization and interaction
    ****************************************************************
    */
    //Validation onTrigger enter and onTrigger exit
    public bool firstArrow;
    public bool secondArrow;

    public GameObject corner0;
    public GameObject corner1;
    public GameObject corner2;
    public GameObject corner3;

    private GameObject[] corners = new GameObject[4];
    
	void Start () {
        corners[0] = corner0;
        corners[1] = corner1;
        corners[2] = corner2;
        corners[3] = corner3;
        Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		rb.velocity = transform.up * speed;
        //rb.velocity = transform.forward * speed;
        randomArrow(corners);
	}

    public void randomArrow (GameObject[] corners)
    {
        int cornerNumber = (int)Random.Range(0.0f, 3.0f);
        Debug.Log(cornerNumber);
        corners[cornerNumber].GetComponent<SpriteRenderer>().enabled = true;
        switch (cornerNumber)
        {
            case 0:
                {
                    corners[1].SetActive(false);
                    corners[2].SetActive(false);
                    break;
                }
            case 1:
                {
                    corners[0].SetActive(false);
                    corners[3].SetActive(false);
                    break;
                }
            case 2:
                {
                    corners[0].SetActive(false);
                    corners[3].SetActive(false);
                    break;
                }
            case 3:
                {
                    corners[1].SetActive(false);
                    corners[2].SetActive(false);
                    break;
                }
        }
        
    }

    public void OnTriggerEnter2D (Collider2D other)
    {

    }

    void FixedUpdate()
    {

    }


}
