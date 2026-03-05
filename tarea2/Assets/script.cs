using UnityEngine;
using System.Collections; 
using System.Collections.Generic; 
public class script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3[] positions = new Vector3[5];
    private LineRenderer line; 
    void Start()
    {
        
        line = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frameç
    void Update()
    {
        
        CreateShape();
    }
    void CreateShape()
    {
        line.positionCount = positions.Length; 
        
        line.SetPosition(0,  positions[0]);
        line.SetPosition(1,  positions[1]);
        line.SetPosition(2,  positions[2]);
        line.SetPosition(3,  positions[3]);

        line.loop = true;

        line.numCornerVertices = 90;
        line.numCapVertices = 90;

        line.startWidth = 0.5f; 
        line.endWidth =  0.5f;
    }
}
