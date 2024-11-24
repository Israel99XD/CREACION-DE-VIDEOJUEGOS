using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Text label;

    public void Display(bool show)
    {
        this.label.enabled = show;
    }

    public void SetAmmo(int current, int max)
    {
        this.label.text = $"{current} / {max}";
    }
}
