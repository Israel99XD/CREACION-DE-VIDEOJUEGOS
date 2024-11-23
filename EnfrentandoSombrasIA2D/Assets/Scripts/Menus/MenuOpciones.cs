using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] public bool isMain;
    public void PantallaCompleta(bool pantallaCompleta)
    {
        if (isMain)
        {
            PlayerPrefs.SetInt("pantallaCompleta",pantallaCompleta==true?1:0);
        }
        Screen.fullScreen = pantallaCompleta;
        ControllerUser.Instance.SetPantalla(pantallaCompleta);
    }

    public void CambiarVolumen (float volumen)
    {
        if (isMain)
        {
            PlayerPrefs.SetFloat("volumen", volumen);
        }
        audioMixer.SetFloat("Volumen", volumen);
        ControllerUser.Instance.SetVolumen(volumen);
    }

    public void CambiarCalidad(int index)
    {
        if (isMain)
        {
            PlayerPrefs.SetInt("calidad", index);
        }
        QualitySettings.SetQualityLevel(index);
        ControllerUser.Instance.SetCalidad(index);
    }
}
