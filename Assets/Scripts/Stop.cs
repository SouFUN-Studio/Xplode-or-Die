using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    void Update()
    {
        GetComponent<HazzardMover>().currentSpeed = 0.0f;

    }
}
