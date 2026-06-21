using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class ShowResults : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public RectTransform scroll;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(OpenResults());
        scroll.localScale = new Vector3(1f, 0f, 1f);
        if (Defesa.currentMode == 1)
        {
            if (AttackModeSelection.score >= 1)
            {
                resultText.text = "Vitória! Você conquistou o castelo!";
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
                resultText.text = "Vitória! A defesa foi um sucesso!";
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

    IEnumerator OpenResults()
    {
        yield return new WaitForSeconds(1f);
        LeanTween.scaleY(scroll.gameObject, 1f, 1f);
    }

}
