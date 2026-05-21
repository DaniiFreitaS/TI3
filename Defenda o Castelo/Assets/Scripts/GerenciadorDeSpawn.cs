using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class GerenciadorDeSpawn : MonoBehaviour
{
    public static GerenciadorDeSpawn instancia;

    [Header("Prefab Selecionado pela UI")]
    public GameObject prefabSelecionado;
    public GameObject textoAviso;

    [Header("Configurações de UI e Cenas")]
    public GameObject botaoVerResultado;
    public static int resultadofinal = 0;
    private int confirmIndex;
    private int totalDeSpawns = 0; // Vai contar quantos já foram colocados
    public static int wrongPlaces;

    private List<Button> buttonsSaved = new List<Button>();
    private List<GameObject> troopsSaved = new List<GameObject>();

    private void Awake()
    {
        instancia = this;
    }

    private void Start()
    {
        wrongPlaces = 1;
        Defesa.currentMode = 1;
    }

    public void SelecionarPrefabInimigo(GameObject prefab)
    {
        textoAviso.SetActive(true);
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        buttonsSaved.Add(button);
        //button.gameObject.SetActive(false);
        button.interactable = false;
        prefabSelecionado = prefab;
        instancia.prefabSelecionado = prefab;
        troopsSaved.Add(prefab);
    }

    public void Somador(bool acertou)
    {
        textoAviso.SetActive(false);
        if (acertou)
        {
            resultadofinal++;
            wrongPlaces = 1;
        }
        else
        {
            resultadofinal--;
            wrongPlaces = -1;
        }

            totalDeSpawns++;
    }

    private void FixedUpdate()
    {
        if (totalDeSpawns >= 2 && botaoVerResultado != null)
        {
            confirmIndex = 0;
            botaoVerResultado.SetActive(true);
            totalDeSpawns = -1;
        }
    }

    public void Confirm()
    {
        if (confirmIndex == 0)
        {
            SceneManager.LoadScene("ResultScreen");
        }else if(confirmIndex == 1)
        {
            SceneManager.LoadScene("StartScreen");
        }else if (confirmIndex == 2)
        {
            SceneManager.LoadScene("DefensePosition");
        }
    }

    public void MenuInicial()
    {
        confirmIndex = 1;
        botaoVerResultado.SetActive(true);
    }

    public void VoltarAtras()
    {
        confirmIndex = 2;
        botaoVerResultado.SetActive(true);
    }
}