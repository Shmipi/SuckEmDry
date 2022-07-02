using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPScript : MonoBehaviour
{
    public Slider slider;
    public Text text;

    public void SetMaxXp(float maxXp, float xp)
    {
        slider.maxValue = maxXp;
        slider.value = xp;

        text.text = slider.value + " / " + slider.maxValue;
    }

    public void SetXp(float xp)
    {
        slider.value = xp;
        text.text = slider.value + " / " + slider.maxValue;
    }

    public void MaxXpReached()
    {
        text.text = "MAX LEVEL";
    }
}
