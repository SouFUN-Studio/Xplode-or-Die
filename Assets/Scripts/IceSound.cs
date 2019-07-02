using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSound : MonoBehaviour
{
    public AudioClip ice;

    private AudioSource audioSource;
    private float volLowRange;
    private float volHighRange;
    // Start is called before the first frame update
    void Start()
    {
        volLowRange = 0.2f;
        volHighRange = 0.4f;
        audioSource = GetComponent<AudioSource>();
    }

    public void Activate()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        audioSource.PlayOneShot(ice, vol);
    }
}
