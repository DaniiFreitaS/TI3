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
                GameObject novoObjeto = Instantiate(prefabDaVez, i.position, Quaternion.Euler(i.rotation.x, i.rotation.y + 90, i.rotation.z));
                novoObjeto.transform.localScale = new Vector3(novaEscala, novaEscala, novaEscala);
                tropasSpawnadas.Add(novoObjeto);
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


            if (prefabID != colisorID)
            {
                GerenciadorDeSpawn.erros.Add(
                    GerarMensagemErro(prefabID, colisorID)
                );
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

    private string GerarMensagemErro(int tropa, int local)
    {
        switch (local)
        {
            case 1: // Teto
                return $"{NomeDaTropa(tropa)} não faz nada no teto. ";

            case 2: // Porta
                
                return $"{NomeDaTropa(tropa)} tem que ficar atrás dos escudeiros. ";

            case 3: // Frente
                return $"{NomeDaTropa(tropa)} tem que ficar na frente para defender. ";

            default:
                return "Uma tropa foi posicionada incorretamente.";
        }
    }

    private string NomeDaTropa(int id)
    {
        switch (id)
        {
            case 1: return "Arqueiro";
            case 2: return "Escudeiro";
            case 3: return "Lanceiro";
            default: return "Tropa";
        }
    }
}