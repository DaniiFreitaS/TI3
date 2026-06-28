using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackTimelineController : MonoBehaviour
{
    [Header("Destinos")]
    public Transform[] destinoMeio;
    public Transform[] destinoFrente;

    [Header("Movimentação")]
    public float velocidade = 10f;
    public float alturaExtra = 0.5f;

    [Header("Inimigos Arqueiro")]
    public Animator[] inimigosArqueiro;

    [Header("Inimigos Corpo a Corpo")]
    public Animator[] inimigosTerrestres;

    //----------------------------------------------------
    // CORRIDA
    //----------------------------------------------------

    public void CorpoACorpoCorre()
    {
        StartCoroutine(CorrerGrupo(DadosAtaque.tropasMeio, destinoMeio));
        StartCoroutine(CorrerGrupo(DadosAtaque.tropasFrente, destinoFrente));
    }

    IEnumerator CorrerGrupo(List<GameObject> tropas, Transform[] destinos)
    {
        if (tropas.Count == 0)
            yield break;

        bool terminou = false;

        while (!terminou)
        {
            terminou = true;

            for (int i = 0; i < tropas.Count; i++)
            {
                if (tropas[i] == null)
                    continue;

                Animator anim = tropas[i].GetComponentInChildren<Animator>();

                if (anim != null)
                    anim.SetFloat("Velocidade", 1);

                Transform destino;

                if (destinos.Length == 1)
                    destino = destinos[0];
                else
                    destino = destinos[Mathf.Clamp(i, 0, destinos.Length - 1)];

                Vector3 destinoFinal = new Vector3(
                                            destino.position.x,
                                            destino.position.y + alturaExtra,
                                            destino.position.z);

                tropas[i].transform.position = Vector3.MoveTowards(
                    tropas[i].transform.position,
                    destinoFinal,
                    velocidade * Time.deltaTime);

                if (Vector3.Distance(tropas[i].transform.position, destinoFinal) > 0.05f)
                    terminou = false;
            }

            yield return null;
        }

        foreach (GameObject tropa in tropas)
        {
            if (tropa == null)
                continue;

            Animator anim = tropa.GetComponentInChildren<Animator>();

            if (anim != null)
                anim.SetFloat("Velocidade", 0);
        }
    }

    //----------------------------------------------------
    // ARQUEIRO
    //----------------------------------------------------

    public void ArqueiroAtaca()
    {
        foreach (GameObject tropa in DadosAtaque.tropasTras)
        {
            if (tropa == null)
                continue;

            Animator anim = tropa.GetComponentInChildren<Animator>();

            if (anim != null)
                anim.SetInteger("Atk", 1);
        }
    }

    public void ResolverArqueiro()
    {
        if (DadosAtaque.tras == 1)
        {
            foreach (Animator inimigo in inimigosArqueiro)
            {
                if (inimigo == null)
                    continue;

                inimigo.SetFloat("Vida", 0);
                inimigo.SetTrigger("Dano");
            }
        }
    }

    //----------------------------------------------------
    // CORPO A CORPO
    //----------------------------------------------------

    public void CorpoACorpoAtaca()
    {
        foreach (GameObject tropa in DadosAtaque.tropasFrente)
        {
            if (tropa == null)
                continue;

            Animator anim = tropa.GetComponentInChildren<Animator>();

            if (anim != null)
                anim.SetInteger("Atk", 1);
        }

        foreach (GameObject tropa in DadosAtaque.tropasMeio)
        {
            if (tropa == null)
                continue;

            Animator anim = tropa.GetComponentInChildren<Animator>();

            if (anim != null)
                anim.SetInteger("Atk", 1);
        }
    }

    public void ResolverCorpoACorpo()
    {
        bool escudeiroCorreto = DadosAtaque.frente == 2;
        bool lanceiroCorreto = DadosAtaque.meio == 3;

        if (escudeiroCorreto && lanceiroCorreto)
        {
            foreach (Animator inimigo in inimigosTerrestres)
            {
                if (inimigo == null)
                    continue;

                inimigo.SetFloat("Vida", 0);
                inimigo.SetTrigger("Dano");
            }
        }
        else
        {
            foreach (Animator inimigo in inimigosTerrestres)
            {
                if (inimigo == null)
                    continue;

                inimigo.SetInteger("Atk", 1);
            }

            foreach (GameObject tropa in DadosAtaque.tropasFrente)
            {
                if (tropa == null)
                    continue;

                Animator anim = tropa.GetComponentInChildren<Animator>();

                if (anim != null)
                {
                    anim.SetFloat("Vida", 0);
                    anim.SetTrigger("Dano");
                }
            }

            foreach (GameObject tropa in DadosAtaque.tropasMeio)
            {
                if (tropa == null)
                    continue;

                Animator anim = tropa.GetComponentInChildren<Animator>();

                if (anim != null)
                {
                    anim.SetFloat("Vida", 0);
                    anim.SetTrigger("Dano");
                }
            }
        }
    }

    //----------------------------------------------------
    // RESULTADO
    //----------------------------------------------------

    public void IrParaResultado()
    {
        SceneManager.LoadScene("ResultScreen");
    }
}