using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShowResults : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (Defesa.currentMode == 1)
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
            if (GerenciadorDeSpawn.resultadofinal >= 1)
            {
                resultText.text = "Vitória! Sua pontuação foi " + 2000 + "  pontos";
            }
            else
            {
                string mistakes = string.Join("\n", GerenciadorDeSpawn.erros);

                resultText.text =
                    "Você falhou!\n\n" +
                    mistakes;
            }
        }
    }
}
