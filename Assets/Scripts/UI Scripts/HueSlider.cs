using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HueSlider : MonoBehaviour
{
    float m_Hue;
    float m_Saturation;
    float m_Value;
    public Slider m_SliderHue;

    Renderer m_Renderer;

    void Start()
    {
        
        m_Renderer = GetComponent<Renderer>();

        //min-max values for hue
        m_SliderHue.maxValue = 1;
        m_SliderHue.minValue = 0;
    }

    void Update()
    {
        m_Hue = m_SliderHue.value;

        m_Renderer.material.color = Color.HSVToRGB(m_Hue,m_Saturation,m_Value);
    }
}
