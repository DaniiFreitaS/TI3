using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PrefabID : MonoBehaviour
{
    public int ID = 0;

    [Header("Configurações de Áudio")]
    public AudioClip somAtaque;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }

    public void InstantiateAlert(GameObject alertTextPrefab)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        GameObject alert = Instantiate(alertTextPrefab, gameObject.transform);
    }

    public void TocarSomAtaque()
    {
        if (audioSource != null && somAtaque != null)
        {
            audioSource.PlayOneShot(somAtaque);
        }
    }
}