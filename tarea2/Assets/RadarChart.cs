using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SkillHexMesh : MonoBehaviour
{
    public Material mat;

    // Stats (0-5 niveles)
    [Range(0,5)] public int attack = 3;
    [Range(0,5)] public int speed = 2;
    [Range(0,5)] public int health = 4;
    [Range(0,5)] public int defense = 1;
    [Range(0,5)] public int mana = 5;
    [Range(0,5)] public int strength = 2;

    public float maxRadius = 5f;

    private Mesh mesh;

    void Update()
    {
        Generate();
    }

    void Generate()
    {
        mesh = new Mesh();
        mesh.name = "SkillHexMesh";

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = mat;

        float[] stats = new float[6];
        stats[0] = attack;
        stats[1] = speed;
        stats[2] = health;
        stats[3] = defense;
        stats[4] = mana;
        stats[5] = strength;

        Vector3[] vertices = new Vector3[7];

        // centro
        vertices[0] = Vector3.zero;

        for (int i = 0; i < 6; i++)
        {
            float angle = Mathf.Deg2Rad * (60 * i);

            float value = stats[i] / 5f; // normalizamos 0-5
            float radius = value * maxRadius;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            vertices[i + 1] = new Vector3(x, y, 0);
        }

        mesh.vertices = vertices;

        int[] triangles = new int[]
        {
            0,1,2,
            0,2,3,
            0,3,4,
            0,4,5,
            0,5,6,
            0,6,1
        };

        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}