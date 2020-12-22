using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource musicPlayer;
    public AudioClip[] audioclip;
   float vol = 100;
    private int ind;
    public static AudioManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;

                if (instance == null)
                    Debug.Log("No Singleton obj");
            }

            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
        ind = GetSceneIndex();
        music_manager();
    }

    void Update()
    {
        if (ind != GetSceneIndex())
        {
            ind = GetSceneIndex();
            if(ind == 2)
            {

            }
            else { music_manager(); }
           
           

        }

    }


    public int GetSceneIndex()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    }

    public void music_manager()
    {
        musicPlayer = GetComponent<AudioSource>();
        if (ind != 0) { musicPlayer.loop = true; }
        musicPlayer.volume = vol;
        musicPlayer.clip = audioclip[ind];
        musicPlayer.Play();

    }
    public void vo(bool k)
    {
        if (k)
        {
            musicPlayer.volume = 100;
            vol = 100;
        }
        else
        {
            musicPlayer.volume = 0;
            vol = 0;
        }
    }
    public void volumecontrol(float k)
    {
        musicPlayer.volume =k;
        vol = k;

    }
    public float volumevalue()
    {
        return musicPlayer.volume;
    }
}




