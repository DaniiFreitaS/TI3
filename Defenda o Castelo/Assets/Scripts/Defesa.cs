using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Defesa : MonoBehaviour
{
    //defesa
    public int contador = 2;
    public GameObject[] defenses;

    //atacante
    public TextMeshProUGUI alertText;
    private int randomEnemy = 0;
    private int enemyIndex = 0;

    //verificador
    private int[,] resultTable = new int[4, 3]
    {
        { 1,  -1, -1},//0
        {-1,  0,  1},//1
        { 1,  1, -1},//2
        {0,  -1,  1}//3
    };
    public static int score = 0;
    public static int currentMode = 0;
    public static List<string> wrongDefenses = new List<string>();
    private string[] defenseErrors = { "Sem muros, os bárbaros conseguiram invadir! ",
        "Muros não param espiões! ",
        "Eles te forçaram a se render pela fome! "};
    private string[] troopErrors = { "Arqueiros não são o suficiente para impedir a invasão!",
        "Arqueiros não identificam espiões! ",
        "Guardas não conseguem atingir o exército de cerco! "};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contador = 2;
        randomEnemy = Random.Range(0, 4);
        if (randomEnemy == 0)
        {
            alertText.text = "Bárbaros vão te atacar";//historicamente errado, mas depois a gente vê um nome mais adequado para invasores violentos
            enemyIndex = 0;
        }else if(randomEnemy == 1)
        {
            alertText.text = "Espiões vão te atacar";
            enemyIndex = 1;
        }
        else
        {
            alertText.text = "Vão fazer um cerco ao redor do castelo";
            enemyIndex = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (contador == 0)
        {
            Resultado();
        }
    }

    public void EscolherDefesa(int index)
    {
        Instantiate(defenses[index], new Vector3(index-1, 0, 0), Quaternion.identity);
        int result = resultTable[index, enemyIndex];
        score += result;
        contador -= 1;
        if (result == -1&&index<=1)
        {
            wrongDefenses.Add(defenseErrors[enemyIndex]);
        }else if(result == -1)
        {
            wrongDefenses.Add(troopErrors[enemyIndex]);
        }
    }


    public void Resultado()
    {
        score = score * 1000;
        currentMode = 1;
        SceneManager.LoadScene("ResultScreen");
    }

    public void Restart()
    {
        SceneManager.LoadScene("DefensePosition");
    }

    public void ChangeText(TextMeshProUGUI text)
    {
        text.text = "Escolha seus soldados";
    }
}
