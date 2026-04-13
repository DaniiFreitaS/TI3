using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AttackModeSelection : MonoBehaviour
{
    public GameObject placement;
    public GameObject selection;
    public GameObject[] troop;
    private int currentIndex;
    public static int score;
    private int choicesLeft;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentIndex = 0;
        score = 0;
        choicesLeft = 3;
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
        Instantiate(troop[currentIndex], new Vector3(positionIndex, 0, 0), Quaternion.identity);
        
        if(currentIndex+positionIndex==-4|| currentIndex + positionIndex == 2 || currentIndex + positionIndex == 5)
        {
            score += 1;
        }else if(currentIndex + positionIndex == 4 || currentIndex + positionIndex == -2 || currentIndex + positionIndex == -3)
        {
            score -= 1;
        }

        selection.SetActive(true);
        choicesLeft -= 1;
        placement.SetActive(false);
    }
}
