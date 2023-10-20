using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    Slider volSlide;
    
    void Start()
    {
        volSlide = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        volSlide.value = 0.5f;
    }

    public void changeVol()
    {
        if(volSlide)
        {
            AudioListener.volume = volSlide.value * 2;
        }
    }
}
