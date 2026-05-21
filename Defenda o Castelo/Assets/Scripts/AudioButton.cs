using UnityEngine;
using UnityEngine.UI;
public class AudioButton : MonoBehaviour
{
    [SerializeField] private AudioClip audioPointerEnter;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Button button;

    private void Awake()
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
        if (audioSource  != null && button != null)
        {
            audioSource.PlayOneShot(audioPointerEnter);
        }
    }
}
