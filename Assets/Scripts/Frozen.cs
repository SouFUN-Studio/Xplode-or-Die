using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frozen : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Line")
        {
            GameObject bomb = GameObject.Find("Normal Bomb");
            
                bomb.GetComponent<HazzardMover>().currentSpeed = 0.0f;
                bomb.transform.Find("Ice").gameObject.SetActive(true);
                bomb.GetComponent<BoxCollider2D>().enabled = true;
            
        }
    }
}
