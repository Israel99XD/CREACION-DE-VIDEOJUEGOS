using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MongoDB.Driver;
using System;
using MongoDB.Bson;

public class GameOpntions : MonoBehaviour
{
    public TMP_InputField campoCorreo;
    public TMP_InputField campoUsuario;
    public TMP_InputField campoContrasena;

    public TMP_Text label;
    public TMP_Text labelTitulo;
    public TMP_Text labelMensaje;

    public GameObject registro;
    public GameObject home;
    public GameObject notificacion;
    public GameObject ranking;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MenuInicio()
    {
        SceneManager.LoadScene(0);
    }

    public void ImprimirValores()
    {
        string valorCampo1 = campoCorreo.text;
        string valorCampo2 = campoUsuario.text;
        string valorCampo3 = campoContrasena.text;
        EnvioDatos(valorCampo1, valorCampo2, valorCampo3);

        Debug.Log("Valor del campo 1: " + valorCampo1);
        Debug.Log("Valor del campo 2: " + valorCampo2);
        Debug.Log("Valor del campo 3: " + valorCampo3);

    }

    public void EnvioDatos(string sCorreo, string sUsuario, string sContrasena)
    {
        ConexionBD conexion = new ConexionBD();
        var coleccion = conexion.ConexionMongo();

        var Registro = new BsonDocument { { "Correo", sCorreo }, { "Usuario", sUsuario }, { "Contrasena", sContrasena } };
        coleccion.InsertOne(Registro);
        var registroInsertado = Registro;

        if (registroInsertado != null)
        {
            string idUsuario = registroInsertado["_id"].ToString();
            string usuario = registroInsertado["Usuario"].ToString();
            PlayerPrefs.SetString("idUsuario", idUsuario);
            PlayerPrefs.SetString("usuario", usuario);
            ControllerUser.Instance.InicioSession(idUsuario, usuario);
            //Debug.Log("ID del documento insertado: " + idUsuario);

            if (home != null && registro != null && ranking != null)
            {
                home.SetActive(true);
                ranking.SetActive(true);
                registro.SetActive(false);
            }
            
            if (notificacion != null)
            {
                labelTitulo.text = $"¡Bienvenido {usuario}!";
                labelMensaje.text = "¡Felicidades! Tu registro ha sido completado con éxito. Estamos encantados de tenerte con nosotros. ¡Bienvenido y disfruta del juego!";
                notificacion.SetActive(true);
            }
            label.text = $"Hola {usuario} !";


        }
        else
        {
            if (notificacion != null)
            {
                labelTitulo.text = "Registro Fallido";
                labelMensaje.text = "Hubo un problema al procesar tu registro. Asegúrate de que todos tus datos sean correctos y vuelve a intentarlo. ";
                notificacion.SetActive(true);

            }
            ControllerUser.Instance.InicioSession(string.Empty, string.Empty);
        }
    }
}
