using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonQuit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Return()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
