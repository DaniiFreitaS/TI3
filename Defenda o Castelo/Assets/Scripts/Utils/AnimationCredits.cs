using UnityEngine;

public class AnimationCredits : MonoBehaviour
{
    public RectTransform creditsContainer;
    public float pageHeight;
    public GameObject[] scrolls;
    private int currentPage = 0;
    private bool moving;

    private void Start()
    {
        currentPage = 0;
        for (int i = 0; i < scrolls.Length; i++)
        {
            scrolls[i].SetActive(false);
        }
    }

    public void OpenCredits()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.enabled = false;
        creditsContainer.localScale = new Vector3(1f, 0f, 1f);
        creditsContainer.gameObject.SetActive(true);
        LeanTween.scaleY(creditsContainer.gameObject, 1f, 0.6f)
            .setOnComplete(() => {
                for (int i = 0; i < scrolls.Length; i++)
                {
                    scrolls[i].SetActive(true);
                }
            });
    }
    public void NextPage()
    {
        if (moving) return;
        if (currentPage >= 3) return;
        currentPage++;
        MoveToPage();
    }

    public void PreviousPage()
    {
        if (moving) return;
        if (currentPage <= 0) return;
        currentPage--;
        MoveToPage();
    }

    void MoveToPage()
    {
        moving = true;
        float startY = creditsContainer.anchoredPosition.y;
        float targetY = currentPage * pageHeight;

        LeanTween.value(creditsContainer.gameObject, startY, targetY, 0.6f).setEaseInCubic()
            .setOnUpdate((float y) =>
            {
                creditsContainer.anchoredPosition = new Vector2(creditsContainer.anchoredPosition.x, y);
            })
            .setOnComplete(() =>
            {
                moving = false;
            });
    }

    public void CloseCredits()
    {
        for (int i = 0; i < scrolls.Length; i++)
        {
            scrolls[i].SetActive(false);
        }
        Animator animator = gameObject.GetComponent<Animator>();
        animator.enabled = true;
        LeanTween.scale(creditsContainer, Vector3.zero, 0.25f).setEaseInBack()
            .setOnComplete(() =>
            {
                currentPage = 0;
                creditsContainer.anchoredPosition = Vector2.zero;
                creditsContainer.gameObject.SetActive(false);
                creditsContainer.gameObject.transform.localScale = Vector3.one;
            });
    }
}
