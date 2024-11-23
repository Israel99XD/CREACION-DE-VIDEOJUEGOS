using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerUser : MonoBehaviour
{
    public static ControllerUser Instance;

    [SerializeField] private string idUser;
    [SerializeField] private string nameUser;
    [SerializeField] private int score;
    [SerializeField] private int scoreA;
    [SerializeField] public int personaje=0;
    [SerializeField] private int calidad = 2;
    [SerializeField] private float volumen = 0;
    [SerializeField] private bool isPantallaCompleta = true;
    [SerializeField] private bool isPosion = false;

    private void Awake()
    {
        if (ControllerUser.Instance == null) {
            ControllerUser.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
        scoreA = 0;
        isPantallaCompleta = true;
        volumen = 0;
        calidad = 2;
    }

    public void InicioSession(string id, string name) 
    { 
        idUser = id;
        nameUser = name;
    }


    public void SumarPuntos(int puntaje)
    {
        if (score > 0)
        {
            scoreA = score;
        }
        score = puntaje;
        
    }

    public void SetScoreA(int s) { 
        scoreA = s;
    }

    public string GetID()
    {
        return idUser;
    }
    public string GetName()
    {
        return nameUser;
    }

    public int GetScore()
    {
        return score;
    }
    public int GetScoreA()
    {
        return scoreA;
    }
    public bool GetPantalla()
    {
        return isPantallaCompleta;
    }

    public float GetVolumen()
    {
        return volumen;
    }

    public int GetCalidad()
    {
        return calidad;
    }

    public bool GetPosion()
    {
        return isPosion;
    }

    public void SetPantalla(bool opcion)
    {
        isPantallaCompleta = opcion;
    }

    public void SetVolumen(float Nivel)
    {
        volumen = Nivel;
    }

    public void SetCalidad(int index)
    {
        calidad=index;
    }

    public void SetPosion(bool opcion)
    {
        isPosion = opcion;
    }
}
