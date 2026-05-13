using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AttackModeSelection : MonoBehaviour
{
    //posicionamento
    public GameObject placement;
    public GameObject selection;
    public GameObject alertTextPrefab;
    public Animator[] animator;
    private float[] lanePositions = {-2f, 0f, 2f};
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

    //outros
    public static int score;
    private int choicesLeft;
    public GameObject resetButton;
    private List<Button> buttonsSaved = new List<Button>();
    private List<GameObject> troopsSaved = new List<GameObject>();
    private List<Button> panelsSaved = new List<Button>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentIndex = 0;
        score = 0;
        choicesLeft = 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(choicesLeft == 0)
        {
            animator[3].SetTrigger("Expand");
            choicesLeft = 1;
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
        GameObject instance = Instantiate(troop[currentIndex], new Vector3(xPos, 0, 0), Quaternion.identity);
        troopsSaved.Add(instance);
        animator[positionIndex] = instance.GetComponent<Animator>();
        
        int result = resultTable[currentIndex, positionIndex];

        score += result;

        if (result == -1)
        {
            wrongTroops.Add(troopErrors[currentIndex]);

            Vector3 screenPos = Camera.main.WorldToScreenPoint(instance.transform.position);
            Transform canvas = gameObject.transform;
            GameObject dmg = Instantiate(
                alertTextPrefab,
                screenPos,
                Quaternion.identity,
                canvas
            );
        }

        selection.SetActive(true);
        choicesLeft -= 1;
        placement.SetActive(false);
    }

    public void Restart()
    {
        if (troopsSaved.Count > 0)
        {
            choicesLeft += 1;
            int lastIndex = troopsSaved.Count - 1;
            buttonsSaved[lastIndex].gameObject.SetActive(true);
            panelsSaved[lastIndex].interactable = true;
            Destroy(troopsSaved[lastIndex]);
            troopsSaved.RemoveAt(lastIndex);
            buttonsSaved.RemoveAt(lastIndex);
            panelsSaved.RemoveAt(lastIndex);
        }
        else
        {
            return;
        }
    }

    public void Cancel(string animationName)
    {
        animator[3].SetTrigger(animationName);
        resetButton.SetActive(true);
        choicesLeft = 0;
        Restart();
    }
    public void CallCoroutine(string coroutine)
    {
        StartCoroutine(coroutine);
    }
    IEnumerator GoToResults()
    {
        for (int i = 0; i < 3; i++)
        {
            animator[i].SetTrigger("SwitchScene");
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("ResultScreen");
    }
}
