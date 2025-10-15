/*using UnityEngine;
using UnityEngine.UI;

public class DangerIndicator : MonoBehaviour
{
    public static DangerIndicator Instance { get; private set; }

    public Image redOverlay;
    public Color flashColor = new Color(1f, 0f, 0f, 0.08f);
    public Color dangerColor = new Color(1f, 0f, 0f, 0.15f);
    public float flashSpeed = 2f;

    private bool anyFacesActive = false;
    private bool anyFacesChecking = false;
    private float flashTimer = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (redOverlay != null)
        {
            redOverlay.color = new Color(1f, 0f, 0f, 0f);
        }
    }

    private void Update()
    {
        if (redOverlay == null) return;

        if (anyFacesChecking)
        {
            redOverlay.color = dangerColor;
        }
        else if (anyFacesActive)
        {
            flashTimer += Time.deltaTime * flashSpeed;
            float alpha = Mathf.Lerp(0f, flashColor.a, (Mathf.Sin(flashTimer) + 1f) / 2f);
            redOverlay.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
        }
        else
        {
            redOverlay.color = new Color(1f, 0f, 0f, 0f);
            flashTimer = 0f;
        }
    }

    public void SetFacesActive(bool active)
    {
        anyFacesActive = active;
    }

    public void SetFacesChecking(bool checking)
    {
        anyFacesChecking = checking;
    }
}
*/

using UnityEngine;
using UnityEngine.UI;

public class DangerIndicator : MonoBehaviour
{
    public static DangerIndicator Instance { get; private set; }

    public Image redOverlay;
    public Color flashColor = new Color(0.3f, 0f, 0f, 0.15f);
    public Color dangerColor = new Color(0.5f, 0f, 0f, 0.25f);
    public float flashSpeed = 2f;

    private bool anyFacesActive = false;
    private bool anyFacesChecking = false;
    private float flashTimer = 0f;
    private Material additiveMaterial;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (redOverlay != null)
        {
            redOverlay.color = new Color(1f, 0f, 0f, 0f);
            
            Shader additiveShader = Shader.Find("UI/Default");
            if (additiveShader != null)
            {
                additiveMaterial = new Material(additiveShader);
                redOverlay.material = additiveMaterial;
            }
        }
    }

    private void Update()
    {
        if (redOverlay == null) return;

        if (anyFacesChecking)
        {
            redOverlay.color = dangerColor;
        }
        else if (anyFacesActive)
        {
            flashTimer += Time.deltaTime * flashSpeed;
            float alpha = Mathf.Lerp(0f, flashColor.a, (Mathf.Sin(flashTimer) + 1f) / 2f);
            redOverlay.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
        }
        else
        {
            redOverlay.color = new Color(1f, 0f, 0f, 0f);
            flashTimer = 0f;
        }
    }

    public void SetFacesActive(bool active)
    {
        anyFacesActive = active;
    }

    public void SetFacesChecking(bool checking)
    {
        anyFacesChecking = checking;
    }
}

