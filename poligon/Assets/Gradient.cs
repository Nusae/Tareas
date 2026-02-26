/*using UnityEngine;

public class Gradient : MonoBehaviour
{
    public float widthLine = 0.05f;
    public GameObject prefabsLine;
    public Transform parentLines;

    public Color color1 = Color.red;
    public Color color2 = Color.blue;

    void Start()
    {
        CreateGradient(color1, color2);
    }

    void CreateGradient(Color c1, Color c2)
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        int numberOfLines = Mathf.CeilToInt(height / widthLine);

        for (int i = 0; i < numberOfLines; i++)
        {
            GameObject objLine = Instantiate(prefabsLine, parentLines);

            LineRenderer line = objLine.GetComponent<LineRenderer>();

            float t = (float)i / (numberOfLines - 1);
            Color currentColor = Color.Lerp(c1, c2, t);


            float yPos = -height / 2f + i * widthLine;

            line.positionCount = 2;
            line.startWidth = widthLine;
            line.endWidth = widthLine;
            line.useWorldSpace = true;

            line.startColor = currentColor;
            line.endColor = currentColor;

            line.SetPosition(0, new Vector3(-width / 2f, yPos, 0));
            line.SetPosition(1, new Vector3(width / 2f, yPos, 0));
        }
    }
}*/

using System.Collections; 
using System.Collections.Generic;
using Unity.Mathematics.Geometry;
using UnityEngine;
public class Gradient : MonoBehaviour
{
    public float widthLine = 1;

    public GameObject prefabsLine;

    public Transform parentLines;

    public Color color1, color2;

    private float height;
    private float width;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera cam = Camera.main; 
        height = 2f*cam.orthographicSize; 
        width = height * cam.aspect;
        
        
        Gradient_c(color1, color2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Gradient_c(Color c1, Color c2)
    {
        int totalLines = Mathf.RoundToInt(height / widthLine);

        for (int i = 0; i < totalLines; i++)
        {
            GameObject objLine = GameObject.Instantiate(prefabsLine);
            objLine.transform.parent = parentLines;

            LineRenderer line = objLine.GetComponent<LineRenderer>();

            line.positionCount = 2;
            line.startWidth = widthLine;
            line.endWidth = widthLine;
            line.useWorldSpace = true;

            
            float yPos = -height / 2f + i * widthLine;

            
            Vector3 startPos = new Vector3(-width / 2f, yPos, 0);
            Vector3 endPos   = new Vector3( width / 2f, yPos, 0);

            line.SetPosition(0, startPos);
            line.SetPosition(1, endPos);

            
            float t = (float)i / (totalLines - 1);
            Color currentColor = Color.Lerp(c1, c2, t);

            line.startColor = currentColor;
            line.endColor = currentColor;
        }
    }
}