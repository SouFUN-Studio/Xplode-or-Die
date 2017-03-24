using UnityEngine;

public class LineHandler : MonoBehaviour {

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public Material m1;
    private GameObject lineGO;
    private LineRenderer lineRenderer;
    private int i = 0;
    void Start()
    {
        lineGO = new GameObject("Line");
        lineGO.AddComponent<LineRenderer>();
        lineRenderer = lineGO.GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        lineRenderer.startColor = c1;
        lineRenderer.endColor = c2;
        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.0f;
        lineRenderer.numPositions = 0;
        lineRenderer.materials[0] = m1;
    }

    void Update()
    {

        if (Input.touchCount > 0 )
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {   
                
                lineRenderer.numPositions = (i + 1);
                
                
                //lineRenderer.SetVertexCount(i + 1);
                Vector3 mPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
                lineRenderer.SetPosition(i, Camera.main.ScreenToWorldPoint(mPosition));
                i++;

                /* Add Collider */

                BoxCollider bc = lineGO.AddComponent<BoxCollider>();
                bc.transform.position = lineRenderer.transform.position;
                bc.size = new Vector3(0.1f, 0.1f, 0.1f);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                /* Remove Line */
                lineRenderer.numPositions = 0;
                //lineRenderer.SetVertexCount(0);
                i = 0;

                /* Remove Line Colliders */

                BoxCollider[] lineColliders = lineGO.GetComponents<BoxCollider>();

                foreach (BoxCollider b in lineColliders)
                {
                    Destroy(b);
                }
            }
        }
    }
}
