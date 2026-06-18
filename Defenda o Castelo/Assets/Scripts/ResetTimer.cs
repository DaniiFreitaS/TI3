using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetTimer : MonoBehaviour
{
    float timeLimit = 60f;//TEMPO PARA VOLTAR AO MENU EM CASO AFK
    float timer = 0f;
    public static ResetTimer instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(timer);
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
            timer = 0f;
            SceneManager.LoadScene("StartScreen");
        }
    }
}
