using UnityEngine;
using System.Collections.Generic;

public class PixelVoxel : MonoBehaviour
{
    public Texture2D image;
    public GameObject cubePixel;
    public Transform parent;
    public float scale = 1.0f;

    private List<Rigidbody> _rigidbodies = new List<Rigidbody>();

    void Start()
    {
        Generate();
    }

    [ContextMenu("Generate")]
    public void Generate()
    {
        if (image == null)
        {
            Debug.LogError("PixelVoxel: No image assigned.");
            return;
        }

        if (cubePixel == null)
        {
            Debug.LogError("PixelVoxel: No cubePixel prefab assigned.");
            return;
        }

        // Clear existing voxels if any (useful for re-generating in Editor)
        if (parent != null)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }
        _rigidbodies.Clear();

        int width = image.width;
        int height = image.height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color pixelColor = image.GetPixel(x, y);

                // If pixel is not nearly transparent
                if (pixelColor.a > 0.1f)
                {
                    Vector3 position = new Vector3(x * scale, y * scale, 0);
                    GameObject voxel = Instantiate(cubePixel, position, Quaternion.identity);
                    
                    voxel.transform.localScale = new Vector3(scale, scale, scale);
                    if (parent != null)
                    {
                        voxel.transform.SetParent(parent);
                    }

                    voxel.name = $"Voxel_{x}_{y}";

                    // Apply color
                    Renderer renderer = voxel.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        // Using material.color (instantiates a new material instance)
                        renderer.material.color = pixelColor;
                    }

                    // Setup Rigidbody
                    Rigidbody rb = voxel.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.useGravity = false;
                        rb.isKinematic = true;
                        _rigidbodies.Add(rb);
                    }
                }
            }
        }

        Debug.Log($"PixelVoxel: Generation complete. {_rigidbodies.Count} voxels created.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateGravity();
        }
    }

    public void ActivateGravity()
    {
        foreach (Rigidbody rb in _rigidbodies)
        {
            if (rb != null)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }
        Debug.Log("PixelVoxel: Gravity activated for all voxels!");
    }
}
