using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;
using System.Collections;
using DentedPixel;
using UnityEngine.Playables;

public class GerenciadorDeSpawn : MonoBehaviour
{
    public static GerenciadorDeSpawn instancia;

    [Header("Prefab Selecionado pela UI")]
    public GameObject prefabSelecionado;
    public GameObject[] paneisPosicionamento;
    public GameObject textoAviso;

    [Header("Configurações de UI e Cenas")]
    public GameObject canvas;
    public GameObject botaoVerResultado;
    public TextMeshProUGUI confirmText;
    public GameObject confirmPanel;
    public Animator cameraAnimator;
    public static int resultadofinal = 0;
    private int confirmIndex;
    private int totalDeSpawns = 0; // Vai contar quantos já foram colocados
    public static int wrongPlaces;

    public PlayableDirector battleTimeline;

    private List<Button> buttonsSaved = new List<Button>();
    private List<GameObject> troopsSaved = new List<GameObject>();

    public static List<string> erros = new List<string>();

    public GameObject[] ExercitoASerDestruido;

    private void Awake()
    {
        instancia = this;
    }

    private void Start()
    {
        SpawnDefesa.tropasSpawnadas.Clear();
        DadosDaBatalha.teto = 0;
        DadosDaBatalha.porta = 0;
        DadosDaBatalha.frente = 0;
        DadosDaBatalha.venceu = false;
        DadosDaBatalha.tropasTeto.Clear();
        DadosDaBatalha.tropasFrente.Clear();
        DadosDaBatalha.tropasPorta.Clear();
        wrongPlaces = 1;
        Defesa.currentMode = 0;
        resultadofinal = 0;
        erros.Clear();
        StartCoroutine(StartDefense());
    }

    public void SelecionarPrefabInimigo(GameObject prefab)
    {
        textoAviso.SetActive(false);
        for (int i = 0; i < paneisPosicionamento.Length; i++)
        {
            LeanTween.cancel(paneisPosicionamento[i].gameObject);
            LeanTween.scale(paneisPosicionamento[i].gameObject, Vector3.one * 1.2f, 0.5f)
                .setEaseInOutSine().setLoopPingPong();
        }
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
            //SceneManager.LoadScene("ResultScreen");
            canvas.SetActive(false);
            //StartCoroutine(MostrarAtaqueEIrParaResultado());
            DadosDaBatalha.venceu = resultadofinal >= 1;
            foreach (GameObject i in ExercitoASerDestruido)
            {
                if (i != null)
                {
                    Destroy(i);
                }
            }

            battleTimeline.Play();
        }
        else if(confirmIndex == 1)
        {
            SceneManager.LoadScene("StartScreen");
        }else if (confirmIndex == 2)
        {
            SceneManager.LoadScene("DefensePosition");
        }
    }

    private IEnumerator MostrarAtaqueEIrParaResultado()
    {
        canvas.SetActive(false);
        cameraAnimator.Play("DefenseCloseIn");
        foreach (GameObject tropa in SpawnDefesa.tropasSpawnadas)
        {
            Animator anim = tropa.GetComponentInChildren<Animator>();

            if (anim != null)
            {
                anim.speed = Random.Range(0.95f, 1.05f);
                anim.SetInteger("Atk", 1);
            }

            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        }
        Debug.Log("Teto: " + DadosDaBatalha.teto);
        Debug.Log("Porta: " + DadosDaBatalha.porta);
        Debug.Log("Frente: " + DadosDaBatalha.frente);
        DadosDaBatalha.venceu = resultadofinal >= 1;
        yield return new WaitForSeconds(3f);//MUDAR TEMPO DEPOIS DE FINALIZAR OS TESTES

        SceneManager.LoadScene("ResultScreen");
    }

    public void Cancel()
    {
        if (confirmIndex >= 1)
        {
            confirmPanel.SetActive(false);
        }
        else
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