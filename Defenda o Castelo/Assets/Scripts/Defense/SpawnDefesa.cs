using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDefesa : MonoBehaviour
{
    //vetor para adicionar todos os pontos de spawn
    public Transform[] pontoDeSpawn;

    //escala para os prefabs dos bonecos
    public float novaEscala = 6f;

    //guarda todas as tropas spawnadas
    public static List<GameObject> tropasSpawnadas = new();

    //id do colisor
    public int colisorID = 0;

    public void Spawnar()
    {
        for (int i = 0; i < GerenciadorDeSpawn.instancia.paneisPosicionamento.Length; i++)
        {
            LeanTween.cancel(GerenciadorDeSpawn.instancia.paneisPosicionamento[i].gameObject);
            GerenciadorDeSpawn.instancia.paneisPosicionamento[i].transform.localScale = Vector3.one;
        }

        GameObject prefabDaVez = GerenciadorDeSpawn.instancia.prefabSelecionado;

        if (prefabDaVez != null && pontoDeSpawn != null && pontoDeSpawn.Length > 0)
        {
            foreach (Transform i in pontoDeSpawn)
            {
                Debug.Log(i);

                GameObject novoObjeto = Instantiate(
                    prefabDaVez,
                    i.position,
                    Quaternion.Euler(i.rotation.x, i.rotation.y + 90, i.rotation.z)
                );

                novoObjeto.transform.localScale = new Vector3(novaEscala, novaEscala, novaEscala);

                tropasSpawnadas.Add(novoObjeto);

                // Salva todas as tropas que nasceram em cada posição
                switch (colisorID)
                {
                    case 1: // Teto
                        DadosDaBatalha.tropasTeto.Add(novoObjeto);
                        break;

                    case 2: // Frente
                        DadosDaBatalha.tropasFrente.Add(novoObjeto);
                        break;

                    case 3: // Porta
                        DadosDaBatalha.tropasPorta.Add(novoObjeto);
                        break;
                }

                Canvas canvasDoSpawn = i.GetComponentInChildren<Canvas>();

                if (canvasDoSpawn != null)
                {
                    // desabilita o worldcanvas
                    canvasDoSpawn.gameObject.SetActive(false);
                }
            }

            Debug.Log("Tropa posicionada!");

            //verifica a posicao para somar
            int prefabID = prefabDaVez.GetComponent<PrefabID>().ID;

            Debug.Log(prefabID);
            Debug.Log("ColisorID: " + colisorID);

            switch (colisorID)
            {
                case 1: // Teto
                    DadosDaBatalha.teto = prefabID;
                    break;

                case 2: // Frente
                    DadosDaBatalha.frente = prefabID;
                    break;

                case 3: // Porta
                    DadosDaBatalha.porta = prefabID;
                    break;
            }

            Debug.Log("Salvou!");

            if (prefabID != colisorID)
            {
                string erro = GerarMensagemErro(colisorID);

                if (!GerenciadorDeSpawn.erros.Contains(erro))
                {
                    GerenciadorDeSpawn.erros.Add(erro);
                }
            }

            GerenciadorDeSpawn.instancia.Somador(prefabID == colisorID);

            //limpa o prefab selecionado
            GerenciadorDeSpawn.instancia.prefabSelecionado = null;

            //desativa o colisor pra nao colocar tropa nele denovo
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            Debug.LogWarning("Nenhuma tropa selecionada ou ponto de spawn faltando.");
        }
    }

    private void OnMouseDown()
    {
        Spawnar();
    }

    private string GerarMensagemErro(int local)
    {
        switch (local)
        {
            case 1: // Teto
                return "Os arqueiros devem ficar em cima pela distância.";

            case 2: // Frente
                return "Os escudeiros devem ficar na frente para defender.";

            case 3: // Porta
                return "Os lanceiros devem ficar na frente para atacar.";

            default:
                return "Uma tropa foi posicionada incorretamente.";
        }
    }
}