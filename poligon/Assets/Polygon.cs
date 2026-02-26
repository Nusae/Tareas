using UnityEngine;

public class Polygon : MonoBehaviour
{
    [SerializeField] private int centerX = 0;
    [SerializeField] private int centerY = 0; 
    [SerializeField] private int radius = 5; 
    [SerializeField] private int sides = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    
    void Update()
    {
         DrawPolygon();
    }
    void DrawPolygon()
    {
        if (sides < 3)
        {
            Debug.LogError("Un polígono necesita al menos 3 lados.");
            return;
        }

        lineRenderer.positionCount = sides + 1; // +1 para cerrar el polígono
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        float angleStep = 2 * Mathf.PI / sides;

        for (int i = 0; i <= sides; i++)
        {
            float angle = i * angleStep;

            float x = centerX + Mathf.Cos(angle) * radius;
            float y = centerY + Mathf.Sin(angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}
