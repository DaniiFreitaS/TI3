using UnityEngine;

public class SpawnDefesa : MonoBehaviour
{
    public GameObject prefabToSpawn;

    //variavel para o transform do Spawn
    public Transform[] pontoDeSpawn;
    //escalando os bonecos em 6 pra ficar grande na tela
    public float novaEscala = 6f;

    private void OnMouseDown()
    {
        // Verifica se tanto o prefab quanto o ponto de spawn foram configurados no Inspector
        if (prefabToSpawn != null && pontoDeSpawn != null)
        {
            // Agora instanciamos na posição e rotação do 'pontoDeSpawn' em vez do objeto clicado
            foreach (Transform i in pontoDeSpawn)
            {
                GameObject novoObjeto = Instantiate(prefabToSpawn, i.position, i.rotation);
                novoObjeto.transform.localScale = new Vector3(novaEscala, novaEscala, novaEscala);
                Debug.Log("Novo prefab criado");
            }   
        }
        else
        {
            // Aviso caso você esqueça de preencher os campos na Unity
            Debug.LogWarning("Atenção: Falta atribuir o Prefab ou o Ponto de Spawn no Inspector do objeto " + gameObject.name);
        }
    }
}
