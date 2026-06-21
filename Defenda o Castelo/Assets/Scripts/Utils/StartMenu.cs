using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Defesa.wrongDefenses.Clear();
        Defesa.score = 0;
        Defesa.currentMode = 0;
        AttackModeSelection.wrongTroops.Clear();
        AttackModeSelection.score = 0;
        animator = GetComponent<Animator>();
    }

    public void PlayGame(string gameMode)
    {
        StartCoroutine(GoGame(gameMode));
    }

    public void CloseCredits(GameObject panelCredits)
    {
        StartCoroutine(EndCredits(panelCredits));
    }

    public void PlayAnimations(string triggerName)
    {
        Animator btnAnimator = EventSystem.current.currentSelectedGameObject.GetComponent<Animator>();
        animator.SetTrigger(triggerName);
    }

    IEnumerator EndCredits(GameObject panelCredits)
    {
        animator.SetTrigger("ClosePanel");
        yield return new WaitForSeconds(1f);
        panelCredits.SetActive(false);
    }

    IEnumerator GoGame(string modeName)
    {
        yield return new WaitForSeconds(0.4f);
        animator.SetTrigger("PlayGame");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(modeName);
    }
}
