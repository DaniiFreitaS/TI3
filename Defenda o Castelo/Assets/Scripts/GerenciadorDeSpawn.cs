using UnityEngine;
using UnityEngine.UI; // Necessário para gerenciar o Button
using UnityEngine.SceneManagement; // Necessário para as cenas

public class GerenciadorDeSpawn : MonoBehaviour
{
    public static GerenciadorDeSpawn instancia;

    [Header("Prefab Selecionado pela UI")]
    public GameObject prefabSelecionado;

    [Header("Configurações de UI e Cenas")]
    public Button botaoVerResultado; // Arraste o botão de resultado aqui no Inspector
    public string cenaVitoria = "Vitoria";
    public string cenaDerrota = "Derrota";

    private int resultadofinal = 0;
    private int totalDeSpawns = 0; // Vai contar quantos já foram colocados

    private void Awake()
    {
        // Define que a "instancia" é este script atual
        instancia = this;
    }

    private void Start()
    {
        // Desativa o botão de resultado assim que o jogo começa
        if (botaoVerResultado != null)
        {
            botaoVerResultado.interactable = false;
        }
    }

    // Função para os botoes no canvas
    public void SelecionarPrefabInimigo(GameObject prefab)
    {
        prefabSelecionado = prefab;
        Debug.Log("Você selecionou a tropa: " + prefab.name);
    }

    // Sua função modificada, agora controlando também o botão!
    public void Somador(bool acertou)
    {
        if (acertou)
        {
            resultadofinal++;
        }

        totalDeSpawns++;

        // Se já colocou as 2 tropas, liga o botão de resultado
        if (totalDeSpawns >= 2 && botaoVerResultado != null)
        {
            botaoVerResultado.interactable = true;
        }
    }

    public void Resultado()
    {
        if (resultadofinal > 1)
        {
            // vitoria
            SceneManager.LoadScene(cenaVitoria);
        }
        else
        {
            // derrota
            SceneManager.LoadScene(cenaDerrota);
        }
    }

    // Função para o seu botão de Restart
    public void ReiniciarJogo()
    {
        string cenaAtual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(cenaAtual);
    }
}