using UnityEngine;
using UnityEngine.UI;
public class OpenScroll : MonoBehaviour
{
    [SerializeField] public AudioClip audioPointerEnter;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public Button button;

    public void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        if (button == null)
        {
            Button btn = button.GetComponent<Button>();
        }
    }

    public void AudioPlay()
    {
        if (audioSource != null && button != null)
        {
            audioSource.PlayOneShot(audioPointerEnter);
        }
    }
}
