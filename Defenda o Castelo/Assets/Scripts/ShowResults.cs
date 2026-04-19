using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShowResults : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Defesa.currentMode == 0)
        {
            if (AttackModeSelection.score >= 1)
            {
                resultText.text = "Vitória! Sua pontuação foi " + AttackModeSelection.score * 1000 + " pontos";
            }
            else
            {
                string mistakes = string.Join(" ", AttackModeSelection.wrongTroops);
                resultText.text = "Você falhou! " + mistakes;
            }
        }
        else
        {
            if (Defesa.score >= 0)
            {
                resultText.text = "Vitória! Sua pontuação foi " + Defesa.score + "  pontos";
            }
            else
            {
                string mistakes = string.Join(" ", Defesa.wrongDefenses);
                resultText.text = "Você falhou! " + mistakes;
            }
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
