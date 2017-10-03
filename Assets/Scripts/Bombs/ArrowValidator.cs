using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowValidator : MonoBehaviour {

    public bool onTrigger;
    public bool onCollision;

    //ontrigger
    public void OnTriggerEnter2D(Collider2D collision)
    {
        onTrigger = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        onTrigger = false;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        onCollision = true;
    }
}
