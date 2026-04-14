using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetTimer : MonoBehaviour
{
    float timeLimit = 180f;
    float timer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }

        if (timer >= timeLimit)
        {
            SceneManager.LoadScene("StartScreen");
        }
    }
}
