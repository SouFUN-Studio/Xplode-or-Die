using UnityEngine;
using System.Collections;

public class LerpMover : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;
    [System.Serializable]
    public enum TypeOfMovement { Simple, Loop, Reverse}
    public TypeOfMovement typeOfMovement = TypeOfMovement.Simple;

    // Movement speed in units/sec.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    private bool activate;
    private bool resetTime;

    void Start()
    {
        // Keep a note of the time the movement started.
        Restart();
        activate = true;
        
        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // Follows the target position like with a spring
    void Update()
    {
        switch (typeOfMovement.ToString())
        {
            case "Simple":
                {
                    PositiveMove();
                    break;
                }
            case "Loop":
                {
                    PositiveMove();

                    if (Vector3.Distance(transform.position, endMarker.position) < 0.05f)
                    {
                        transform.position = startMarker.position;
                        resetTime = true;
                        Restart();
                    }
                    break;
                }
            case "Reverse":
                {
                    if(activate)
                    {
                        PositiveMove();
                    }
                    else
                    {
                        
                        NegativeMove();
                    }

                    break;
                }
        }
    }
    public void PositiveMove()
    {
        if (Vector3.Distance(transform.position, endMarker.position) > 0.005f)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
        }

        else
        {
            activate = false;
            Restart();
        }
    }

    public void NegativeMove()
    {
        Debug.Log("Negative");
        if (Vector3.Distance(transform.position, startMarker.position) > 0.005f)
        {
            Debug.Log("Move");
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(endMarker.position, startMarker.position, fracJourney);
        }
        else
        {
            activate = true;
            Restart();
        }
    }
    public void Restart()
    {
            startTime = Time.time;
            resetTime = !resetTime;
    }
}