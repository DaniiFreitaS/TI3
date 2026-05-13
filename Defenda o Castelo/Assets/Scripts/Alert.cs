using TMPro;
using UnityEngine;

public class Alert : MonoBehaviour
{
    private float moveSpeed = 150f;
    private float fadeSpeed = 0.5f;

    private TextMeshProUGUI text;
    private Color color;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        color = text.color;
    }

    void FixedUpdate()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        color.a -= fadeSpeed * Time.deltaTime;

        text.color = color;

        if (color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
