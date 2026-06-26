using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTimelineController : MonoBehaviour
{
    [Header("Inimigos")]
    public Animator[] inimigosArqueiro;
    public Animator[] inimigosEscudeiro;
    public Animator[] inimigosLanceiro;

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
            foreach (Animator inimigo in inimigosArqueiro)
            {
                if (inimigo != null)
                    inimigo.SetFloat("Vida", 0);
                    inimigo.SetTrigger("Dano");
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
                anim.SetInteger("Atk", 1); // ou o parâmetro da animação de defesa
            }
        }
    }

    public void ResolverEscudeiro()
    {
        if (DadosDaBatalha.frente == 2)
        {
            foreach (Animator inimigo in inimigosEscudeiro)
            {
                if (inimigo != null)
                    inimigo.SetInteger("Atk", 1);
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
            foreach (Animator inimigo in inimigosLanceiro)
            {
                if (inimigo != null)
                    inimigo.SetFloat("Vida", 0);
                    inimigo.SetTrigger("Dano");
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