using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour
{
    private float moveSpeed = 400f;
    private float fadeSpeed = 120f;
    private float stopThreshold = 100;

    private Image alert;

    void Start()
    {
        alert = GetComponent<Image>();
        stopThreshold = 100;
    }

    void FixedUpdate()
    {

        if (stopThreshold > 0)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            stopThreshold -= fadeSpeed * Time.deltaTime;
        }
    }
}
