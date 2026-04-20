using UnityEngine;

public class Shield_Anim : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Idle()
    {
        animator.SetFloat("Speed", 0);//coloca o Speed como 0, ou seja, o personagem para de andar (Olher no Animator Controller)
    }


    public void Aiming()
    {
        animator.SetFloat("Speed", 1); //coloca o Speed como 1, ou seja, o personagem começa a andar (Olher no Animator Controller)
    }
}
