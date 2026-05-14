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

    [Header("Configurações de UI e Cenas")]
    public GameObject botaoVerResultado;
    //public GameObject twinPanel;

    public static int resultadofinal = 0;
    private int totalDeSpawns = 0; // Vai contar quantos já foram colocados
    public static int wrongPlaces;

    private List<Button> buttonsSaved = new List<Button>();
    private List<GameObject> troopsSaved = new List<GameObject>();
    private List<Button> panelsSaved = new List<Button>();

    bool isTwin = false;

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
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        buttonsSaved.Add(button);
        button.gameObject.SetActive(false);
        prefabSelecionado = prefab;
        instancia.prefabSelecionado = prefab;
        troopsSaved.Add(prefab);
    }

    // Sua função modificada, agora controlando também o botão!
    public void Somador(bool acertou)
    {
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
            botaoVerResultado.SetActive(true);
            totalDeSpawns = -1;
        }
    }

    public void Resultado()
    {
        SceneManager.LoadScene("ResultScreen");
    }

    public void VoltarAtras()
    {
        SceneManager.LoadScene("DefensePosition");
        /*
        if (totalDeSpawns == -1)
        {
            totalDeSpawns = 2;
        }
        totalDeSpawns -= 1;
        wrongPlaces *= -1;
        int lastIndex = troopsSaved.Count - 1;
        buttonsSaved[lastIndex].gameObject.SetActive(true);
        troopsSaved[lastIndex].SetActive(false);
        troopsSaved.RemoveAt(lastIndex);
        buttonsSaved.RemoveAt(lastIndex);
    */
    }
}