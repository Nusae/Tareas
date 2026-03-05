using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    public Vector2 center = Vector2.zero;
    public float radius = 2f;
    public int sides = 6;
    public Material mat;

    private Mesh mesh;

    void Update()
    {
        Generate();
    }

    void Generate()
    {
        mesh = new Mesh();
        mesh.name = "ProceduralMesh";

        GetComponent<MeshFilter>().mesh = mesh;

        // Crear vertices del polígono
        Vector3[] vertices = new Vector3[sides];

        float angleStep = 2 * Mathf.PI / sides;

        for (int i = 0; i < sides; i++)
        {
            float angle = i * angleStep;

            float x = center.x + Mathf.Cos(angle) * radius;
            float y = center.y + Mathf.Sin(angle) * radius;

            vertices[i] = new Vector3(x, y, 0);
        }

        mesh.vertices = vertices;

        // Crear triangulos
        int[] triangles = new int[(sides - 2) * 3];

        for (int i = 0; i < sides - 2; i++)
        {
            int t = i * 3;

            triangles[t] = 0;
            triangles[t + 1] = i + 1;
            triangles[t + 2] = i + 2;
        }

        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        GetComponent<MeshRenderer>().material = mat;
    }

    // dibujar las líneas de los triángulos
    void OnDrawGizmos()
    {
        if (mesh == null) return;

        Gizmos.color = Color.black;

        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 a = transform.TransformPoint(vertices[triangles[i]]);
            Vector3 b = transform.TransformPoint(vertices[triangles[i + 1]]);
            Vector3 c = transform.TransformPoint(vertices[triangles[i + 2]]);

            Gizmos.DrawLine(a, b);
            Gizmos.DrawLine(b, c);
            Gizmos.DrawLine(c, a);
        }
    }
}