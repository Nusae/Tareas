using UnityEngine;

public class FadeController : MonoBehaviour
{
    [Header("Settings")]
    public float fadeDuration = 3f;
    public string propertyName = "_FadeAmount";

    private Material targetMaterial;
    private float currentFade = 0f;
    private bool isFading = false;

    void Start()
    {
        // Obtener el material del objeto
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            targetMaterial = renderer.material;
        }
        
        // Empezar el desvanecimiento automáticamente
        StartFade();
    }

    public void StartFade()
    {
        isFading = true;
        currentFade = 0f;
    }

    void Update()
    {
        if (isFading && targetMaterial != null)
        {
            // Incrementar el valor según el tiempo
            currentFade += Time.deltaTime / fadeDuration;
            
            // Aplicar al shader
            targetMaterial.SetFloat(propertyName, Mathf.Clamp01(currentFade));

            // Detener cuando llegue a 1
            if (currentFade >= 1f)
            {
                isFading = false;
                Debug.Log("Desvanecimiento completado.");
            }
        }
    }
}
