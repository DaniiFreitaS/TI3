using UnityEngine;

public class Defesa : MonoBehaviour
{
    public int contador = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contador = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void somador(int valor)
    {
        contador += valor;
    }


    public void resultado(int contador)
    {
        if(contador < 60)
        {

        }else if(contador > 60)
        {

        }else
        {

        }
    }
}
