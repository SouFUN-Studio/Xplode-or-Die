using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    
    //Variables for ally
    public LayerMask allyMask;
    public float speed;
    public float jumpForce;
    public GameObject face;
    Rigidbody2D myBody;
    Transform myTransform;
    Animator myAnimator;
    float myWidth;
    bool rbIsGrounded = false;

    //Variables for wait time
    private float currentTime = 0.0f;
    private float step = 1f;


    private int count;
	void Start () {
        //Initialization of ally components
        myTransform = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        myAnimator = this.GetComponent<Animator>();
        count = FindObjectOfType<GameController>().getCount();
    //Initialization of wait time
    StartCoroutine(TimerRoutine());
	}
    private void Update()
    {
        Physics2D.IgnoreLayerCollision(8,8,true);
    }

    void LateUpdate () {
        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Destroy"))
        {
            Destroy(this.gameObject);
        }
        //Check to see if there's ground in front of us before moving forward
        Vector2 lineCastPosition = myTransform.position - myTransform.right * myWidth;
//DEBUG        Debug.DrawLine(lineCastPosition,lineCastPosition + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPosition,
                          lineCastPosition + Vector2.down, allyMask);
        myAnimator.SetFloat("speed", speed);
        myAnimator.SetInteger("count", count);
        //if theres not ground, turn around
        if (!isGrounded)
        {
            Vector3 currentRot = myTransform.eulerAngles;
            Vector3 currentFacePosition = face.transform.localPosition;
            face.transform.localPosition = currentFacePosition*-1;
      //      Debug.Log(face.transform.localPosition);
            currentRot.y += 180.0f;
            myTransform.eulerAngles = currentRot;
            // cambia a idle innecesariamente idle();
        }
        //Random animation selecction
        if (currentTime > 3)
        {
            int random = (int)Random.Range(1.0f, 2.5f);
            switch (random)
            {
                case 1:
                    speedUp(rbIsGrounded);
                    currentTime = 0.0f;
                    break;
                case 2:
                    speedDown();
                    currentTime = 0.0f;
                    break;
            }
        }
        //Always move forward
        Vector2 myVel = myBody.velocity;
        myVel.x = -myTransform.right.x * speed;
        myBody.velocity = myVel;
        if (rbIsGrounded)
        {
            myBody.velocity = Vector2.up * jumpForce;
        }
    }
    
    public void speedUp(bool grounded)
    {
        //If rigidbody is grounded, can walk
        if (grounded)
        {
            speed = 1.0f;
            jumpForce = 1.0f;
        }
        
    }
    
    public void speedDown()
    {
        speed = 0.0f;
        jumpForce = 0.0f;
    }

    //Wait time method

    IEnumerator TimerRoutine()
    {
        while (currentTime < 10)
        {
            yield return new WaitForSeconds(step);
            currentTime += step;
        }
    }

    /**********************************************************************
     * GET and SET methods for the private variables used in other scripts*
     **********************************************************************
     */

    public void OnCollisionStay2D(Collision2D collision)
    {
        rbIsGrounded = true;
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        rbIsGrounded = false;
    }
    public void SetDestroy(bool destroy)
    {
        speed = 0.0f;
        myAnimator.SetBool("isDead", destroy);
     //   Debug.Log("Is dead: " + this.gameObject.name);
    }
}
