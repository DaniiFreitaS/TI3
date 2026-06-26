using System.Collections;
using UnityEngine;

public class AttackTimelineController : MonoBehaviour
{
    public GameObject personagem;
    public Transform destino;

    public float velocidade = 1200f;

    public void Correr()
    {
        StartCoroutine(CorrerAteDestino());
    }

    IEnumerator CorrerAteDestino()
    {
        Animator anim = personagem.GetComponentInChildren<Animator>();

        if (anim != null)
            anim.SetFloat("Velocidade", 1);

        while (Mathf.Abs(personagem.transform.position.z - destino.position.z) > 0.05f)
        {
            Vector3 novaPosicao = personagem.transform.position;

            novaPosicao.z = Mathf.MoveTowards(
                personagem.transform.position.z,
                destino.position.z,
                velocidade * Time.deltaTime);

            personagem.transform.position = novaPosicao;

            yield return null;
        }

        if (anim != null)
            anim.SetFloat("Velocidade", 0);
    }
}