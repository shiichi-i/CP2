using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolControl : MonoBehaviour
{
    public Slider volumeSlider;
    
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
