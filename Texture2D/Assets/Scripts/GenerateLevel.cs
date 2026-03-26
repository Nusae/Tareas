using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public Texture2D image;
    public GameObject cubeWall;
    public Transform parent;

    [Range(0.1f, 10f)]
    public float scale = 1.0f; // Multiplier for size and spacing

    [Range(0f, 1f)]
    public float threshold = 0.5f;

    void Start()
    {
        Generate();
    }
    
    public void Generate()
    {
        if (image == null)
        {
            Debug.LogError("GenerateLevel: No image assigned.");
            return;
        }

        if (cubeWall == null)
        {
            Debug.LogError("GenerateLevel: No cubeWall assigned.");
            return;
        }

        int width = image.width;
        int height = image.height;

        Debug.Log($"GenerateLevel: Generating level from image '{image.name}' ({width}x{height}) with scale {scale}");
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color pixelColor = image.GetPixel(x, y);
                
                if (pixelColor.grayscale < threshold)
                {
                    // Multiply position by scale to keep spacing proportional to size
                    Vector3 position = new Vector3(x * scale, 0, y * scale);
                    GameObject wall = Instantiate(cubeWall, position, Quaternion.identity);
                    
                    // Set the size of the wall to match the scale
                    wall.transform.localScale = new Vector3(scale, scale, scale);
                    
                    if (parent != null)
                    {
                        wall.transform.SetParent(parent);
                    }
                    
                    wall.name = $"Wall_{x}_{y}";
                }
            }
        }
        
        Debug.Log("GenerateLevel: Level generation complete.");
    }
}
