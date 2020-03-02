using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour {

    Image barImage;

    private void Awake()
    {
        barImage = GetComponent<Image>();
    }

    public void SetFill(float value)
    {
        if (barImage != null)
        {
            barImage.fillAmount = value;
        }
    }
}
