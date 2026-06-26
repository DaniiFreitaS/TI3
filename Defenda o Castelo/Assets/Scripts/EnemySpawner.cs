using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instancia;

    [Header("Prefabs Arqueiro")]
    public GameObject[] inimigosArqueiro;

    [Header("Prefabs Escudeiro")]
    public GameObject[] inimigosEscudeiro;

    [Header("Prefabs Lanceiro")]
    public GameObject[] inimigosLanceiro;

    [Header("Escala")]
    public float escala = 3f;

    [Header("Spawn Arqueiro")]
    public Transform[] spawnArqueiro;

    [Header("Spawn Escudeiro")]
    public Transform[] spawnEscudeiro;

    [Header("Spawn Lanceiro")]
    public Transform[] spawnLanceiro;

    public List<GameObject> inimigos = new();

    private void Awake()
    {
        instancia = this;
    }

    public void SpawnArqueiros()
    {
        Limpar();
        SpawnGrupo(inimigosArqueiro, spawnArqueiro);
    }

    public void SpawnEscudeiros()
    {
        Limpar();
        SpawnGrupo(inimigosEscudeiro, spawnEscudeiro);
    }

    public void SpawnLanceiros()
    {
        Limpar();
        SpawnGrupo(inimigosLanceiro, spawnLanceiro);
    }

    private void SpawnGrupo(GameObject[] prefabs, Transform[] pontos)
    {
        int quantidade = Mathf.Min(prefabs.Length, pontos.Length);

        for (int i = 0; i < quantidade; i++)
        {
            GameObject inimigo = Instantiate(
                prefabs[i],
                pontos[i].position,
                Quaternion.Euler(pontos[i].rotation.x, pontos[i].rotation.y - 90, pontos[i].rotation.z)
            );

            inimigo.transform.localScale = new Vector3(escala, escala, escala);

            inimigos.Add(inimigo);
        }
    }

    public void Limpar()
    {
        inimigos.Clear();
    }
}