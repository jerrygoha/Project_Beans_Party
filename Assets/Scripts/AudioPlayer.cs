using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AudioPlayer : MonoBehaviour


{
    public Slider backVolume;
    bool k = true;
    int i = 1;
    public Sprite[] img;
    public GameObject Bar;



    private float backVol = 1f;

    private void Start()
        
    {
        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        backVolume.value = backVol;
      

    }
    void Update()
    {
        if (k)
        {
           
            SoundSlider();
        }
        else { backVol = backVolume.value;
            PlayerPrefs.SetFloat("backvol", backVol);
        }
        

    }
    public void SoundSlider()
    {
        var singleton = AudioManager.instance;
        backVol = backVolume.value;
        PlayerPrefs.SetFloat("backvol", backVol);



        singleton.volumecontrol(backVolume.value);
    }
    public void sd()
    {
        var singleton = AudioManager.instance;
        singleton.vo(!k);
        k = !k;
        if (i == 1)
        {
            Bar.GetComponent<Image>().sprite = img[1];
            i++;
        }
        else
        {
            Bar.GetComponent<Image>().sprite = img[0];
            i = 1;
        }

    }
}
