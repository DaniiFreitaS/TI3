using UnityEngine;

// Isso força a Unity a colocar um componente de áudio no seu personagem automaticamente
[RequireComponent(typeof(AudioSource))]
public class PrefabID : MonoBehaviour
{
    public int ID = 0;

    [Header("Configurações de Áudio")]
    public AudioClip somAtaque; // Campo para colocar o arquivo de som

    private AudioSource audioSource;

    void Start()
    {
        // Encontra o componente de áudio do personagem
        audioSource = GetComponent<AudioSource>();

        // Impede que o som toque sozinho logo ao iniciar a cena
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

    //----------------------------------------------------
    // ESSA FUNÇÃO SERÁ CHAMADA PELA ANIMAÇÃO
    //----------------------------------------------------
    public void TocarSomAtaque()
    {
        if (audioSource != null && somAtaque != null)
        {
            // Toca o som sem cortar os sons anteriores
            audioSource.PlayOneShot(somAtaque);
        }
    }
}