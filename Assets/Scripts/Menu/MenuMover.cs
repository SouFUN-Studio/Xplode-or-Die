using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuMover : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    public RectTransform startMarker;
    public RectTransform endMarker;
    public GameObject mask;
    public GameObject mainMenu;
    public bool activate;
    // Movement speed in units/sec.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;
        // Calculate the journey length.
        journeyLength = Vector2.Distance(startMarker.position, endMarker.position);
    }

    // Follows the target position like with a spring
    void Update()
    {
        if (activate)
            EnterMenu();
        else
            ExitMenu();
        //transform.position = endMarker.position;
    }

    public void StartTime()
    {
        startTime = Time.time;
    }

    public void EnterMenu()
    {
        if (Vector2.Distance(transform.position, endMarker.position) > 0.05f)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector2.Lerp(startMarker.position, endMarker.position, fracJourney);
            GetComponent<CanvasGroup>().alpha = (Vector2.Distance(transform.position, startMarker.position)) / (Vector2.Distance(startMarker.position, endMarker.position));
        }
        else
            mask.SetActive(true);
    }

    public void ExitMenu()
    {
        if (Vector2.Distance(transform.position, startMarker.position) > 0.05f)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector2.Lerp(endMarker.position, startMarker.position, fracJourney);
            GetComponent<CanvasGroup>().alpha = (Vector2.Distance(transform.position, startMarker.position)) / (Vector2.Distance(startMarker.position, endMarker.position));
        }
        else
        {
            this.gameObject.SetActive(false);
            mainMenu.SetActive(true);
            Component[] buttons = mainMenu.GetComponentsInChildren<Button>();
            foreach (Button b in buttons)
            {
                b.interactable = true;
            }
        }
    }

    public void SetActive(bool activate)
    {
        this.activate = activate;
        StartTime();
    }
}
