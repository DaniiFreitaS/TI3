using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;
using System.Collections;

public class GerenciadorDeSpawn : MonoBehaviour
{
    public static GerenciadorDeSpawn instancia;

    [Header("Prefab Selecionado pela UI")]
    public GameObject prefabSelecionado;
    public GameObject textoAviso;

    [Header("Configurações de UI e Cenas")]
    public GameObject canvas;
    public GameObject botaoVerResultado;
    public TextMeshProUGUI confirmText;
    public static int resultadofinal = 0;
    private int confirmIndex;
    private int totalDeSpawns = 0; // Vai contar quantos já foram colocados
    public static int wrongPlaces;

    private List<Button> buttonsSaved = new List<Button>();
    private List<GameObject> troopsSaved = new List<GameObject>();

    public static List<string> erros = new List<string>();

    private void Awake()
    {
        instancia = this;
    }

    private void Start()
    {
        wrongPlaces = 1;
        Defesa.currentMode = 0;
        resultadofinal = 0;
        erros.Clear();
        StartCoroutine(StartDefense());
    }

    public void SelecionarPrefabInimigo(GameObject prefab)
    {
        textoAviso.SetActive(false);
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
        if (totalDeSpawns >= 3 && botaoVerResultado != null)
        {
            confirmIndex = 0;
            confirmText.text = "Tem certeza que terminou de montar as tropas?";
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
        confirmText.text = "Tem certeza que quer ir para o menu inicial?";
        botaoVerResultado.SetActive(true);
    }

    public void VoltarAtras()
    {
        confirmIndex = 2;
        confirmText.text = "Tem certeza que quer desfazer suas ações?";
        botaoVerResultado.SetActive(true);
    }

    IEnumerator StartDefense()
    {
        canvas.SetActive(false);
        yield return new WaitForSeconds(4.1f);
        canvas.SetActive(true);
    }
}