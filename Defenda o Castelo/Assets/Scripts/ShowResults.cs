using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShowResults : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (AttackModeSelection.score >= 1)
        {
            resultText.text = "Vitória! Sua pontuação foi " + AttackModeSelection.score*100;
        }
        else
        {
            resultText.text = "Você falhou, tente rever o posicionamento das tropas";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Return()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
