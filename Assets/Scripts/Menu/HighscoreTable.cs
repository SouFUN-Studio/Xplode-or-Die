using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscoretable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    private void Awake()
    {
        entryContainer = transform.Find("Highscore Entry Container");
        entryTemplate = entryContainer.Find("Highscore Entry Template");

        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 20f;
        for (int i = 0; i < 10; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * 1);
            entryTransform.gameObject.SetActive(true);
        }
    }
}
