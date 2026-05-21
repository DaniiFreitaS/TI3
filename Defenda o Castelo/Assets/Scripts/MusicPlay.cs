using UnityEngine;
using UnityEngine.Audio;

public class MusicPlay : MonoBehaviour
{
    public AudioSource gameMusic;
    
    void Start()
    {
        gameMusic.loop = true;
        gameMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
} 

