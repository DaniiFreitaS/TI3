using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTimelineController : MonoBehaviour
{
    //-------------------------
    // INIMIGOS
    //-------------------------

    public void SpawnInimigosArqueiro()
    {
        EnemySpawner.instancia.SpawnArqueiros();
    }

    public void SpawnInimigosEscudeiro()
    {
        EnemySpawner.instancia.SpawnEscudeiros();
    }

    public void SpawnInimigosLanceiro()
    {
        EnemySpawner.instancia.SpawnLanceiros();
    }

    //-------------------------
    // ARQUEIRO
    //-------------------------

    public void ArqueiroAtaca()
    {
        foreach (GameObject tropa in DadosDaBatalha.tropasTeto)
        {
            if (tropa == null) continue;

            Animator anim = tropa.GetComponentInChildren<Animator>();

            if (anim != null)
            {
                anim.SetInteger("Atk", 1);
            }
        }
    }

    public void ResolverArqueiro()
    {
        if (DadosDaBatalha.teto == 1)
        {
            foreach (GameObject inimigo in EnemySpawner.instancia.inimigos)
            {
                if (inimigo == null) continue;

                Animator anim = inimigo.GetComponentInChildren<Animator>();

                if (anim != null)
                {
                    anim.SetFloat("Vida", 0);
                    anim.SetTrigger("Dano");
                }
            }
        }
        else
        {
            foreach (GameObject tropa in DadosDaBatalha.tropasTeto)
            {
                if (tropa == null) continue;

                Animator anim = tropa.GetComponentInChildren<Animator>();

                if (anim != null)
                {
                    anim.SetFloat("Vida", 0);
                    anim.SetTrigger("Dano");
                }
            }
        }
    }

    //-------------------------
    // ESCUDEIRO
    //-------------------------

    public void EscudeiroDefende()
    {
        foreach (GameObject tropa in DadosDaBatalha.tropasFrente)
        {
            if (tropa == null) continue;

            Animator anim = tropa.GetComponentInChildren<Animator>();

            if (anim != null)
            {
                anim.SetInteger("Atk", 1);
            }
        }
    }

    public void ResolverEscudeiro()
    {
        if (DadosDaBatalha.frente == 2)
        {
            foreach (GameObject inimigo in EnemySpawner.instancia.inimigos)
            {
                if (inimigo == null) continue;

                Animator anim = inimigo.GetComponentInChildren<Animator>();

                if (anim != null)
                {
                    anim.SetInteger("Atk", 1);
                }
            }
        }
        else
        {
            foreach (GameObject tropa in DadosDaBatalha.tropasFrente)
            {
                if (tropa == null) continue;

                Animator anim = tropa.GetComponentInChildren<Animator>();

                if (anim != null)
                {
                    anim.SetFloat("Vida", 0);
                    anim.SetTrigger("Dano");
                }
            }
        }
    }

    //-------------------------
    // LANCEIRO
    //-------------------------

    public void LanceiroAtaca()
    {
        foreach (GameObject tropa in DadosDaBatalha.tropasPorta)
        {
            if (tropa == null) continue;

            Animator anim = tropa.GetComponentInChildren<Animator>();

            if (anim != null)
            {
                anim.SetInteger("Atk", 1);
            }
        }
    }

    public void ResolverLanceiro()
    {
        if (DadosDaBatalha.porta == 3)
        {
            foreach (GameObject inimigo in EnemySpawner.instancia.inimigos)
            {
                if (inimigo == null) continue;

                Animator anim = inimigo.GetComponentInChildren<Animator>();

                if (anim != null)
                {
                    anim.SetFloat("Vida", 0);
                    anim.SetTrigger("Dano");
                }
            }
        }
        else
        {
            foreach (GameObject tropa in DadosDaBatalha.tropasPorta)
            {
                if (tropa == null) continue;

                Animator anim = tropa.GetComponentInChildren<Animator>();

                if (anim != null)
                {
                    anim.SetFloat("Vida", 0);
                    anim.SetTrigger("Dano");
                }
            }
        }
    }

    //-------------------------
    // FINAL
    //-------------------------

    public void IrParaResultado()
    {
        SceneManager.LoadScene("ResultScreen");
    }
}