using UnityEngine;


public class ManagePixelsImage : MonoBehaviour
{
    public Texture2D image;
    public Color targetColor = Color.red;
    public Color replacementColor = Color.blue;
    [Range(0f, 1f)]
    public float tolerance = 0.1f;

    private Color[] _originalColors;
    private int _width;
    private int _height;

    private void Awake()
    {
        if (image != null)
        {
            _width = image.width;
            _height = image.height;
            _originalColors = image.GetPixels();
            
            Debug.Log($"ManagedPixelsImage: Cached original state for texture '{image.name}' ({_width}x{_height})");
        }
        else
        {
            Debug.LogWarning("ManagedPixelsImage: No image assigned to the script.");
        }
    }
    
    [ContextMenu("Change Color")]
    public void ChangeColor()
    {
        if (image == null) return;
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                Color currentPixel = image.GetPixel(x, y);

                if (Vector4.Distance(currentPixel, targetColor) <= tolerance)
                {
                    image.SetPixel(x, y, replacementColor);
                }
            }
        }
        
        image.Apply();
        Debug.Log("ManagedPixelsImage: Color swap applied successfully.");
    }

    private void OnDisable()
    {

        if (image != null && _originalColors != null)
        {
            image.SetPixels(_originalColors);
            image.Apply();
            Debug.Log($"ManagedPixelsImage: Texture '{image.name}' restored to original state.");
        }
    }
}
