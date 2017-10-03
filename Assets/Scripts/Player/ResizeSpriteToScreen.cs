using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeSpriteToScreen : MonoBehaviour {


    private Vector3 colliderSize;
    /* Box Collider init parameters *
     ********************************
     * Offset :  x = 0 y = -3.5
     * Size : x = 5.75 y = 0.5
     * Edge Radius : 0
     */

    private void Start()
    {
        ResizeImg();
    }

    void ResizeImg()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;


        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 xWidth = transform.localScale;
        xWidth.x = worldScreenWidth / width;
        transform.localScale = xWidth;
        //transform.localScale.x = worldScreenWidth / width;
        Vector3 yHeight = transform.localScale;
        yHeight.y = worldScreenHeight / height;
        transform.localScale = yHeight;
        colliderSize = yHeight;
        //transform.localScale.y = worldScreenHeight / height;

    }
    public void reSizeCollider()
    {
        GetComponent<BoxCollider2D>().offset.Set(0.0f, 0.0f);
        GetComponent<BoxCollider2D>().size.Set(colliderSize.x, 11.0f);
    }
}
