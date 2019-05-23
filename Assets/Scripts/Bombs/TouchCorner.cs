using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCorner : MonoBehaviour {

    private bool touched;

	// Use this for initialization
	void Start () {
        
        touched = false;
	}
	
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Line")
        {
            touched = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Line")
        {
            touched = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    //@GET bool touched;
    public bool getTouched()
    {
        return touched;
    }
   
}
