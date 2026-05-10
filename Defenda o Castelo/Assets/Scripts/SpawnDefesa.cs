using UnityEngine;

public class SpawnDefesa : MonoBehaviour
{
    public GameObject prefabToSpawn;

    // Esta variável vai guardar o seu objeto vazio (o "null")
    public Transform pontoDeSpawn;
    public float novaEscala = 6f;

    private void OnMouseDown()
    {
        // Verifica se tanto o prefab quanto o ponto de spawn foram configurados no Inspector
        if (prefabToSpawn != null && pontoDeSpawn != null)
        {
            // Agora instanciamos na posição e rotação do 'pontoDeSpawn' em vez do objeto clicado
            GameObject novoObjeto = Instantiate(prefabToSpawn, pontoDeSpawn.position, pontoDeSpawn.rotation);

            novoObjeto.transform.localScale = new Vector3(novaEscala, novaEscala, novaEscala);

            Debug.Log("Novo prefab criado na área fixa!");
        }
        else
        {
            // Aviso caso você esqueça de preencher os campos na Unity
            Debug.LogWarning("Atenção: Falta atribuir o Prefab ou o Ponto de Spawn no Inspector do objeto " + gameObject.name);
        }
    }
}
