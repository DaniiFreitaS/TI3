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
    private float[] lanePositions = {-4f, 0f, 4f};
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentIndex = 0;
        score = 0;
        choicesLeft = 3;
        wrongTroops.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if(choicesLeft == 0)
        {
            SceneManager.LoadScene("ResultScreen");
        }
    }

    public void GetTroop(int troopIndex)
    {
       Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        button.interactable = false;
        currentIndex = troopIndex;
        placement.SetActive(true);
        selection.SetActive(false);
    }

    public void PlaceTroop(int positionIndex)
    {
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        button.interactable = false;

        float xPos = lanePositions[positionIndex];
        Instantiate(troop[currentIndex], new Vector3(xPos, 0, 0), Quaternion.identity);
        
        int result = resultTable[currentIndex, positionIndex];

        score += result;

        if (result == -1)
        {
            wrongTroops.Add(troopErrors[currentIndex]);
        }

        selection.SetActive(true);
        choicesLeft -= 1;
        placement.SetActive(false);
    }
}
