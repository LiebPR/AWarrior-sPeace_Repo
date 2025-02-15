using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: MonoBehaviour
{
    //Declaración de Singleton 
    public static AudioManager.Instance;
         
    [Header("Audio Source References")]
    public AudioClip[] musicSorce;
    public AudioClip[] sfxSource;
        

    [Header("Audio Clip Arrays")]
    public AudioClip[] musicList;
    public AudioClip[] sfxList;
    
    private void Awake()
    {
        if (Instance == null)
        {
             //Singleton que no se destruye entre escenas
            Instance = this;
            DontDestroyOnload(gameObject);

        }
        else
        {
            Destroy(gameObject);
        
        }
    
    }

    public void PlayMusic(int musicIndex)
    {
        musicSource.clip = musicList[musicIndex];
        musicSource.Play();
    
    }

    public void PlaySFX(int sfxIndex)
    {
        sfxSource.PlayOneShot(sfxList[sfxIndex]);
    }
}
