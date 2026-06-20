using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResultSpawner : MonoBehaviour
{
    [Header("Pontos de Spawn")]
    public Transform[] spawnTeto;
    public Transform[] spawnPorta;
    public Transform[] spawnFrente;

    [Header("Prefabs")]
    public GameObject arqueiroPrefab;
    public GameObject escudeiroPrefab;
    public GameObject lanceiroPrefab;

    [Header("Configuração")]
    public float novaEscala = 6f;

    private List<GameObject> tropasResultado = new();

    private void Start()
    {
        SpawnarGrupo(DadosDaBatalha.teto, spawnTeto);
        SpawnarGrupo(DadosDaBatalha.porta, spawnPorta);
        SpawnarGrupo(DadosDaBatalha.frente, spawnFrente);

        StartCoroutine(TocarAnimacoes());
    }

    private void SpawnarGrupo(int id, Transform[] pontos)
    {
        GameObject prefab = BuscarPrefab(id);

        if (prefab == null || pontos == null || pontos.Length == 0)
            return;

        foreach (Transform ponto in pontos)
        {
            GameObject novoObjeto = Instantiate(
                prefab,
                ponto.position,
                Quaternion.Euler(
                    ponto.eulerAngles.x,
                    ponto.eulerAngles.y + 90f,
                    ponto.eulerAngles.z
                )
            );

            novoObjeto.transform.localScale =
                new Vector3(novaEscala, novaEscala, novaEscala);

            tropasResultado.Add(novoObjeto);
        }
    }

    private GameObject BuscarPrefab(int id)
    {
        switch (id)
        {
            case 1:
                return arqueiroPrefab;

            case 2:
                return escudeiroPrefab;

            case 3:
                return lanceiroPrefab;
        }

        return null;
    }

    private IEnumerator TocarAnimacoes()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (GameObject tropa in tropasResultado)
        {
            Animator anim = tropa.GetComponentInChildren<Animator>();

            if (anim != null)
            {
                anim.speed = Random.Range(0.95f, 1.05f);

                if (DadosDaBatalha.venceu)
                {
                    anim.SetTrigger("Vitoria");
                }
                else
                {
                    anim.SetFloat("Vida", 0);
                    anim.SetTrigger("Dano");
                }
            }

            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        }
    }
}