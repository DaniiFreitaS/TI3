using UnityEngine;

public class SpawnDefesa : MonoBehaviour
{
    //vetor para adicionar todos os pontos de spawn
    public Transform[] pontoDeSpawn;
    //escala para os prefabs dos bonecos
    public float novaEscala = 6f;

    //id do colisor
    public int colisorID = 0;

    public void Spawnar()
    {
        GameObject prefabDaVez = GerenciadorDeSpawn.instancia.prefabSelecionado;

        if (prefabDaVez != null && pontoDeSpawn != null && pontoDeSpawn.Length > 0)
        {
            foreach (Transform i in pontoDeSpawn)
            {
                GameObject novoObjeto = Instantiate(prefabDaVez, i.position, i.rotation);
                novoObjeto.transform.localScale = new Vector3(novaEscala, novaEscala, novaEscala);
            }
            Debug.Log("Tropa posicionada!");

            //verifica a posicao para somar
            int prefabID = prefabDaVez.GetComponent<PrefabID>().ID;
            Debug.Log(prefabID);
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
}