using UnityEngine;

public class LineHandler : MonoBehaviour {

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public Material m1;
    public bool onTouch;
    private GameObject lineGO;
    private LineRenderer lineRenderer;
    private int i = 0;
    void Start()
    {
        lineGO = new GameObject("Line");
        lineGO.AddComponent<LineRenderer>();
        lineGO.tag = "Line";
        lineRenderer = lineGO.GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        lineRenderer.startColor = c1;
        lineRenderer.endColor = c2;
        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.0f;
        lineRenderer.positionCount = 0;
        lineRenderer.materials[0] = m1;
    }

    void Update()
    {
        if (Input.touchCount > 0 )
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)//true
            {
                //Create only if count is less than X
                if (i + 1 > 11)//21
                {
                    Vector3[] positions = new Vector3[lineRenderer.positionCount];
                    lineRenderer.GetPositions(positions);
                    Vector3[] newPositions = new Vector3[20];
                    for(int j = 0; j< 10; j++)//20
                    {
                        newPositions[j] = positions[j + 1];
                    }
                    lineRenderer.SetPositions(newPositions);
                    lineRenderer.positionCount = 10;//20
                    i = 10;//20
                    BoxCollider2D[] lineColliders = lineGO.GetComponents<BoxCollider2D>();

                    Destroy(lineColliders[0]);
                }
                onTouch = true;
                    lineRenderer.positionCount = (i + 1);


                    Vector3 mPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
                    lineRenderer.SetPosition(i, Camera.main.ScreenToWorldPoint(mPosition));
                    i++;

                    /* Add Collider */

                    BoxCollider2D bc = lineGO.AddComponent<BoxCollider2D>();
                    bc.isTrigger = true;
                    bc.transform.position = lineRenderer.transform.position;
                    bc.size = new Vector3(0.15f, 0.15f, 0.15f); //(0.1f,0.1f,0.1f)
                
            }

            if (touch.phase == TouchPhase.Ended)//else 
            {
                onTouch = false;
                /* Remove Line */
                lineRenderer.positionCount = 0;
                //lineRenderer.SetVertexCount(0);
                i = 0;

                /* Remove Line Colliders */

                BoxCollider2D[] lineColliders = lineGO.GetComponents<BoxCollider2D>();

                foreach (BoxCollider2D b in lineColliders)
                {
                    Destroy(b);
                }
            }
        }
    }
//#endif



    public bool getOnTouch()
    {
        return onTouch;
    }
}
