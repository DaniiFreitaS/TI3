using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AttackModeSelection : MonoBehaviour
{

    public PlayableDirector timeline;

    //posicionamento
    public GameObject placement;
    public GameObject selection;
    public GameObject alertTextPrefab;
    public RectTransform textIntro;
    public GameObject confirmMenu;
    public TextMeshProUGUI confirmInfo;
    public Animator[] animator;
    private float[] lanePositions = {16f, 20f, 24f};
    private int[,] resultTable = new int[3, 3]
    {
    { 1,  0, -1},
    {-1,  0,  1},
    { -1, 1,  0}
    };


    //tropas
    public GameObject[] troop;
    private int currentIndex;
    public static List<string> wrongTroops = new List<string>();
    private string[] troopErrors = { "Arqueiros são vulneráveis na frente! ", "Escudeiros não conseguem defender ninguém estando atrás! ", "Lanceiros não conseguem acertar de trás! "};

    [Header("Escala")]
    public float novaEscala = 3f;

    //outros
    public static int score;
    private int choicesLeft;
    private bool zeroChoice;
    private int confirmIndex;
    public GameObject resetButton;
    private List<Button> buttonsSaved = new List<Button>();
    private List<GameObject> troopsSaved = new List<GameObject>();
    private List<Button> panelsSaved = new List<Button>();
    private List<GameObject> alertImage = new List<GameObject>();

    public GameObject canvas;
    void Start()
    {
        DadosAtaque.Limpar();
        Defesa.currentMode = 1;
        currentIndex = 0;
        score = 0;
        choicesLeft = 3;
        zeroChoice = false;
        wrongTroops.Clear();
        StartCoroutine(StartGame());
    }

    void FixedUpdate()
    {
        if(zeroChoice)
        {
            confirmIndex = 0;
            confirmInfo.text = "Tem certeza que terminou de montar as tropas?";
            confirmMenu.SetActive(true);
            zeroChoice = false;
            resetButton.SetActive(false);
        }
    }

    public void GetTroop(int troopIndex)
    {
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        buttonsSaved.Add(button);
        button.gameObject.SetActive(false);
        currentIndex = troopIndex;
        placement.SetActive(true);
        selection.SetActive(false);
    }

    public void PlaceTroop(int positionIndex)
    {
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        button.interactable = false;
        panelsSaved.Add(button);

        float xPos = lanePositions[positionIndex];
        GameObject instance = Instantiate(troop[currentIndex], new Vector3(0, -2.5f, xPos), Quaternion.Euler(0, -90, 0));
        troopsSaved.Add(instance);
        animator[positionIndex] = instance.GetComponent<Animator>();

        instance.transform.localScale = Vector3.one * novaEscala;

        int prefabID = instance.GetComponent<PrefabID>().ID;

        switch (positionIndex)
        {
            case 0: // Trás
                DadosAtaque.tras = prefabID;
                DadosAtaque.tropasTras.Add(instance);
                break;

            case 1: // Meio
                DadosAtaque.meio = prefabID;
                DadosAtaque.tropasMeio.Add(instance);
                break;

            case 2: // Frente
                DadosAtaque.frente = prefabID;
                DadosAtaque.tropasFrente.Add(instance);
                break;
        }

        int result = resultTable[currentIndex, positionIndex];

        score += result;

        if (result == -1)
        {
            wrongTroops.Add(troopErrors[currentIndex]);
            instance.GetComponent<PrefabID>().InstantiateAlert(alertTextPrefab);
        }

        selection.SetActive(true);
        choicesLeft -= 1;
        if (choicesLeft == 0)
        {
            zeroChoice = true;
        }
        placement.SetActive(false);
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("AttackSelection");
        /*if (troopsSaved.Count > 0)
        {
            choicesLeft += 1;
            int lastIndex = troopsSaved.Count - 1;
            buttonsSaved[lastIndex].gameObject.SetActive(true);
            panelsSaved[lastIndex].interactable = true;
            Destroy(troopsSaved[lastIndex]);
            troopsSaved.RemoveAt(lastIndex);
            buttonsSaved.RemoveAt(lastIndex);
            panelsSaved.RemoveAt(lastIndex);
        }*/
    }

    public void Cancel()
    {
        confirmMenu.SetActive(false);
        resetButton.SetActive(true);
        Restart();
    }

    public void Return()
    {
        confirmIndex = 1;
        confirmInfo.text = "Tem certeza que quer ir para o menu inicial?";
        confirmMenu.SetActive(true);
    }
    public void CallCoroutine(string coroutine)
    {
        StartCoroutine(coroutine);
    }
    IEnumerator Confirm()
    {
        if (confirmIndex == 0)
        {
            /*for (int i = 0; i < 3; i++)
            {
                animator[i].SetTrigger("SwitchScene");
            }*/
            Defesa.currentMode = 1;
            yield return new WaitForSeconds(0.3f);
            canvas.SetActive(false);
            timeline.Play();
            //SceneManager.LoadScene("ResultScreen");
        }
        else if (confirmIndex==1)
        {
            yield return new WaitForSeconds(0.3f);
            SceneManager.LoadScene("StartScreen");
        }
    }
    IEnumerator StartGame()
    {
        canvas.SetActive(false);
        yield return new WaitForSeconds(4.2f);
        canvas.SetActive(true);
        textIntro.localScale = Vector3.one * 2f;
        LeanTween.scale(textIntro.gameObject, Vector3.one, 0.8f).setEaseOutBack().
            setOnComplete(() =>
            {
                LeanTween.rotateZ(textIntro.gameObject, 720f, 0.8f);
                LeanTween.moveY(textIntro, 600f, 0.8f).setEaseOutCubic();
            });
    }
}

