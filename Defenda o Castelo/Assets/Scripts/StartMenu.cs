using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Defesa.wrongDefenses.Clear();
        Defesa.score = 0;
        Defesa.currentMode = 0;
        AttackModeSelection.wrongTroops.Clear();
        AttackModeSelection.score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoAttack()
    {
        SceneManager.LoadScene("AttackSelection");
    }

    public void GoDefense() 
    {
        SceneManager.LoadScene("DefensePosition");
    }
}
