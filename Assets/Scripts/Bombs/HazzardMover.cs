﻿using UnityEngine;
using System.Collections;

public class HazzardMover : MonoBehaviour {
    //Use for select the use of the bomb beetwen normal, invert and multiple
    public bool normal;
    public bool invert;
    public bool multiple;
    public int life;

	// Use this for initialization
	public float currentSpeed;
    public float newSpeed;
    public GameObject tarjetPlatform;

    private Animator myAnimator;
    private float startDistance;
    private float distance;
    private int  cornerNumber;
    private Rigidbody2D rb;
    private bool scored;

    //Use this to initialize
    void Start () {
       // Debug.Log("Start bomb" + gameObject.name);
        rb = GetComponent<Rigidbody2D> ();
		rb.velocity = transform.up * - currentSpeed;
        myAnimator = GetComponent<Animator>();
        myAnimator.SetFloat("distance", 100);
        startDistance = Vector2.Distance(new Vector2(GetComponent<Transform>().position.x, 
                                                GetComponent<Transform>().position.y), 
                                    new Vector2(GetComponent<Transform>().position.x, 
                                                tarjetPlatform.GetComponent<BoxCollider2D>().offset.y));
        distance = startDistance;
        scored = false;
        if (normal)
        {
            currentSpeed = Mathf.Log(1f + newSpeed, 3f) + 1f;
            life = 1;
        }
        else
        {
            if (invert)
            {
                currentSpeed = Mathf.Log(2f + newSpeed, 2f) + 1f;
                life = 1;
            }
            else
            {
                currentSpeed = Mathf.Log(1f + newSpeed) + 0.8f;
                life = 10;
            }
        }
    }

    void LateUpdate()
    {
        rb.velocity = transform.up * -currentSpeed;
        DistanceCalculator();
        if (!GameObject.Find("GameController").GetComponent<LineHandler>().getOnTouch())
        {
        }

        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("End Animation"))
        {
            Destroy(this.gameObject);
        }
        myAnimator.SetFloat("distance", (distance *100)/startDistance);

    }

    /* ********************************************************
     * Distance Calculator to change the value of the animator*
     * ********************************************************
     */
    private void DistanceCalculator()
    {
        distance = Vector2.Distance(new Vector2(GetComponent<Transform>().position.x,
                                        GetComponent<Transform>().position.y),
                            new Vector2(GetComponent<Transform>().position.x,
                                        tarjetPlatform.GetComponent<BoxCollider2D>().offset.y));
        if ( distance > (startDistance / 3) * 2)
        {
            Debug.DrawLine(GetComponent<Transform>().position, new Vector3(GetComponent<Transform>().position.x,
                                                                        tarjetPlatform.GetComponent<BoxCollider2D>().offset.y, 0.0f), new Color(0, 255, 0, 255));
        }
        else
        {
            if (distance > (startDistance / 3))
            {
                Debug.DrawLine(GetComponent<Transform>().position, new Vector3(GetComponent<Transform>().position.x,
                                                                        tarjetPlatform.GetComponent<BoxCollider2D>().offset.y, 0.0f), new Color(255, 255, 0, 255));
            }
            else
            {
                Debug.DrawLine(GetComponent<Transform>().position, new Vector3(GetComponent<Transform>().position.x,
                                                                        tarjetPlatform.GetComponent<BoxCollider2D>().offset.y, 0.0f), new Color(255, 0, 0, 255));
            }
        }
    }


    /******************************
     * method for destroy the bomb*
     * ****************************
     */
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Line")
        {
            life--;
            if (life == 0)
            {
                scored = true;
                if (scored)
                {
                    GameObject.Find("GameController").GetComponent<GameController>().SetScoreCount(100);
                    GameObject.Find("GameController").GetComponent<GameController>().SetComboCount(1);
                    GameObject.FindGameObjectWithTag("Combo").transform.position = this.gameObject.transform.position + new Vector3(0.5f,0.5f,0);
                    GameObject.FindGameObjectWithTag("Combo").GetComponent<Animator>().SetTrigger("Restart");
                    
                    GameObject.FindGameObjectWithTag("Combo").GetComponent<Animator>().enabled = true;
                }
                SetDestroy(true);
            }
        }
    }

    /**********************************************************************
     * GET and SET methods for the private variables used in other scripts*
     **********************************************************************
     */
    //@return String Corner number
    public string GetCornerNumber()
    {
        return cornerNumber.ToString();
    }
    //@SET bool destroy
    public void SetDestroy(bool destroy)
    {
        myAnimator.SetBool("destroy", destroy);
        GetComponent<BoxCollider2D>().enabled = false;
        currentSpeed = 0.0f;
    }
    public void SetNewSpeed(float newSpeed)
    {
        this.newSpeed = newSpeed;
    }
    public float GetNewSpeed()
    {
        return newSpeed;
    }
}