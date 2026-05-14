using UnityEngine;
using UnityEngine.UI; // Obrigatório para mexer com Botões (UI)

public class BotaoTropa : MonoBehaviour
{
    public GameObject prefabDestaTropa;
    private Button meuBotao;

    void Start()
    {
        // Pega o botão automaticamente quando o jogo começa
        meuBotao = GetComponent<Button>();
        Debug.Log(meuBotao);
    }

    // Esta é a função que o botão vai chamar
    public void EscolherEDesativar()
    {
        // 1. Manda o prefab pro nosso Gerenciador
        GerenciadorDeSpawn.instancia.SelecionarPrefabInimigo(prefabDestaTropa);

        // 2. Desativa o botão (ele fica cinza e não pode mais ser clicado)
        meuBotao.interactable = false;
    }
}