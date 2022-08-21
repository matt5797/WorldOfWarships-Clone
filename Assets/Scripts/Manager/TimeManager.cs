using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public float multiplier = 1;
    public TextMeshProUGUI textUI;
    public Slider slider;

    private void Awake()
    {
        Time.timeScale = multiplier;
        if (textUI)
            textUI.text = "Timescale: " + multiplier + "x";
        if (slider)
            slider.value = multiplier;
    }

    public void OnTimescaleSelected(float multiplier)
    {
        this.multiplier = multiplier;
        Time.timeScale = multiplier;
        if (textUI)
            textUI.text = "Timescale: " + multiplier + "x";
    }

    public void OnTimescaleChanged(float multiplier)
    {
        this.multiplier = multiplier;
        Time.timeScale = multiplier;
        if (textUI)
            textUI.text = "Timescale: " + multiplier + "x";
    }
}
