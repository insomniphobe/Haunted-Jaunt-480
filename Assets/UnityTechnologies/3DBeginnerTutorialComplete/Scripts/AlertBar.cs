using UnityEngine;
using UI = UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class AlertBar : MonoBehaviour
{
    [Header("References")]
    public Observer observer;
    public UI.Image fillImage;
    public Canvas canvas;

    [Header("Settings")]
    public Vector3 offset = new Vector3(0f, 2.5f, 0f); // Height above enemy
    public float hideThreshold = 0.05f;                 // Hide bar when nearly empty

    [Header("Colors")]
    public Color safeColor = Color.green;
    public Color warningColor = Color.yellow;
    public Color dangerColor = Color.red;

    Camera m_MainCamera;

    void Start()
    {
        m_MainCamera = Camera.main;
    }

    void Update()
    {
        float alertLevel = observer.GetAlertLevel();

        // Show/hide bar
        canvas.enabled = alertLevel > hideThreshold;

        // Update fill amount
        fillImage.fillAmount = alertLevel;

        // Color shifts green -> yellow -> red as alert rises
        if (alertLevel < 0.5f)
        {
            fillImage.color = Color.Lerp(safeColor, warningColor, alertLevel * 2f);
        }
        else
        {
            fillImage.color = Color.Lerp(warningColor, dangerColor, (alertLevel - 0.5f) * 2f);
        }

        // Always face the camera (billboard effect)
        transform.LookAt(transform.position + m_MainCamera.transform.forward);
    }
}