using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;

    // Movement speed in units/sec.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLengthU;
    private float journeyLengthD;
    private bool activate;
    private bool platformDown;
    private int random;
    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;
        // Calculate the journey length.
        journeyLengthU = Vector3.Distance(startMarker.position, endMarker.position);
        journeyLengthD = Vector2.Distance(endMarker.position, startMarker.position);
        platformDown = true;
        StartCoroutine(Mover());
        activate = false;
    }

    // Follows the target position like with a spring
    void Update()
    {
        if(activate)
        if (platformDown)
        {
            MoveUp();
            /*if(Vector2.Distance(transform.position, endMarker.position) < 0.005)
            {
                platformDown = false;
                Debug.Log("platform up");
            } */
        }
        if(!platformDown)
        {
            MoveDown();
            if (Vector2.Distance(transform.position, startMarker.position) < 0.005)
            {

                platformDown = true;
                activate = false;
                Debug.Log("Platform down");
            }
        }
    }
    IEnumerator Mover()
    {
        while (true)
        {
            if (platformDown)
            {
                StartTime();
                float random = Random.Range(10f, 40f);
                yield return new WaitForSeconds(random);
                activate = true;
                yield return new WaitForSeconds(10f);
                platformDown = false;
                yield return new WaitUntil(() => activate == false);

            }
        }
    }

    public void MoveUp()
    {
        if (Vector2.Distance(transform.position, endMarker.position) > 0.005)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;
            
            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLengthU;
            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(transform.position, endMarker.position, fracJourney);
        }
    }
    public void MoveDown()
    {
        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * speed;
        
        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLengthD;
        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(transform.position, startMarker.position, fracJourney);
    }

    public void StartTime()
    {
        startTime = Time.time;
    }
}