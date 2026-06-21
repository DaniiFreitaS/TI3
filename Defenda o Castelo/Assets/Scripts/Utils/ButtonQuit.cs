using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonQuit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Return()
    {
        StartCoroutine(Back());
    }

    IEnumerator Back()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("StartScreen");
    }
}
