using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Header("NextBomb")]
    public GameObject nextBomb;
    [Header("Stop Value")]
    public Transform target;
    [Header("Info")]
    public GameObject info;
    [Header("Hand")]
    public Transform hand;

    // Start is called before the first frame update
    void Start()
    {   
            GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < target.position.y)
        {
            GetComponent<HazzardMover>().currentSpeed = 0.0f;
            GetComponent<BoxCollider2D>().enabled = true;
            info.SetActive(true);
            hand.gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Line")
        {
            //target.transform.position = new Vector3(transform.position.x, transform.position.y-1, 0);
            StartCoroutine(WaitforNext());
        }
    }

    IEnumerator WaitforNext()
    {
        yield return new WaitForSeconds(1f);
        nextBomb.SetActive(true);
    }
}
