using UnityEngine;

public class MusicSwap : MonoBehaviour
{
    public AudioSource victory;
    public AudioSource gameOver;
    void Start()
    {
      if (Defesa.currentMode == 0)
        {
            if (AttackModeSelection.score >= 1)
            {
               victory.loop = true;
               victory.Play();
            }
            else
            {
                gameOver.loop = true;
                gameOver.Play();
            }
        }
        else
        {
            if (GerenciadorDeSpawn.wrongPlaces == 1)
            {
                victory.loop = true;
                victory.Play();
            }
            else
            {
                gameOver.loop = true;
                gameOver.Play();
            }   
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
