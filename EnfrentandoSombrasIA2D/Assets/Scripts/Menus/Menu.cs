using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject btnpausa;
    public TMP_Text titulo;
    public Text labelScore;

    public GameObject btnOpciones;
    public GameObject btnSiguiente;
    public GameObject btnSalir;

    public GameObject Player;
    public GameObject MenuOpciones;

    public Toggle PantallaCompleta;
    public Slider Volumen;
    public TMP_Dropdown Calidad;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void PausarJuego()
    {
        bool estaActivo = btnOpciones.activeSelf;
        if (menu != null && btnpausa != null && titulo != null)
        {
            titulo.text = string.Empty;
            titulo.text = "PAUSA";
            Time.timeScale = 0f;
            btnpausa.SetActive(false);
            btnSiguiente.SetActive(false);
            menu.SetActive(true);
            if (!estaActivo)
            {
                btnOpciones.SetActive(true);
                btnSiguiente.SetActive(false);
            }
            if (!btnSalir.activeSelf)
            {
                btnSalir.SetActive(true);
            }
            int puntaje = ControllerUser.Instance.GetScore();
            labelScore.text = $"Puntaje : {puntaje}";
                
        }
    }

    public void ReanudarJuego()
    {
        if (menu != null && btnpausa != null && titulo != null)
        {
            btnpausa.SetActive(true);
            menu.SetActive(false);
            Time.timeScale = 1f;
            titulo.text = string.Empty;
        }
    }

    public void Home()
    {
        Time.timeScale = 1f;
        Player.GetComponent<Healt>().Reiniciar();
        ControllerUser.Instance.SetPosion(false);
        SceneManager.LoadScene("MenuInicial");
    }

    public void Opciones() {
        PantallaCompleta.isOn = ControllerUser.Instance.GetPantalla();
        Volumen.value = ControllerUser.Instance.GetVolumen();
        Calidad.value = ControllerUser.Instance.GetCalidad();
        MenuOpciones.SetActive(true);
    }

    public void FinJuego()
    {
        if (menu != null && btnpausa != null && titulo != null)
        {
            titulo.text = string.Empty;
            titulo.text = "FIN DEL JUEGO";
            Time.timeScale = 0f;
            btnpausa.SetActive(false);
            menu.SetActive(true);

                btnOpciones.SetActive(true);
                btnSiguiente.SetActive(false);

            int puntaje = ControllerUser.Instance.GetScore();
            puntaje = puntaje > 0 ? puntaje : ControllerUser.Instance.GetScoreA();
            labelScore.text = $"Puntaje : {puntaje}";
        }
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        Player.GetComponent<Healt>().Reiniciar();
        ControllerUser.Instance.SetPosion(false);
    }
    
    public void SiguienteNivel(string nivel)
    {
        Time.timeScale = 1f;
        Player.GetComponent<Healt>().Reiniciar();
        ControllerUser.Instance.SetPosion(false);
        SceneManager.LoadScene(nivel);

    }

    public void PasarNivel(int Nivel, int Personaje)
    {
        int puntaje = ControllerUser.Instance.GetScore();
        bool isPaso = ControllerUser.Instance.GetPosion();

        if (isPaso)
        {
            labelScore.text = $"Puntaje : {puntaje}";
            titulo.text = string.Empty;
            titulo.text = "GANASTE";
            Time.timeScale = 0f;
            btnpausa.SetActive(false);
            menu.SetActive(true);
            btnOpciones.SetActive(false);
            btnSiguiente.SetActive(true);
            btnSalir.SetActive(false);
            string idScore = EnvioDatos(puntaje,Nivel,Personaje);

            if (idScore != null)
            {
                btnSiguiente.SetActive(true);
            }
        }
        if (Nivel == 3)
        {
            labelScore.text = $"Puntaje : {puntaje}";
            titulo.text = string.Empty;
            titulo.text = "GANASTE";
            Time.timeScale = 0f;
            btnpausa.SetActive(false);
            menu.SetActive(true);
            btnOpciones.SetActive(false);
            btnSiguiente.SetActive(true);
            btnSalir.SetActive(false);
            string idScore = EnvioDatos(puntaje, Nivel, Personaje);

            if (idScore != null)
            {
                btnSiguiente.SetActive(true);
            }
        }

    }


    public string EnvioDatos( int iScore, int iNivel, int iPersonaje)
    {
        ConexionBD conexion = new ConexionBD();
        var coleccion = conexion.ConexionMongo2();

        string idUser = ControllerUser.Instance.GetID();
        string userSession =  ControllerUser.Instance.GetName();
        
        
        var Registro = new BsonDocument { { "idUser", idUser}, { "user", userSession }, { "score", iScore }, { "nivel",iNivel },{ "jugador",iPersonaje} };
        coleccion.InsertOne(Registro);
        var registroInsertado = Registro;

        if (registroInsertado != null)
        {
            string idRegistro = registroInsertado["_id"].ToString();
            Debug.Log($"Id registro: {idRegistro}");
            return idRegistro;
            
        }
        return null;
    }
}
