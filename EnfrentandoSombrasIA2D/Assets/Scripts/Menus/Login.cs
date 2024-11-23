using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MongoDB.Driver;
using System;
using MongoDB.Bson;

public class Login : MonoBehaviour
{
    
    public TMP_InputField campoContrasena;
    public TMP_InputField campoUsuario;

    public TMP_Text label;
    public TMP_Text labelTitulo;
    public TMP_Text labelMensaje;

    public GameObject login;
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
        string valorCampo2 = campoUsuario.text.Replace(" ", "");
        string valorCampo3 = campoContrasena.text.Replace(" ", "");
        EnvioDatos(valorCampo2, valorCampo3);

        Debug.Log("Valor del campo 2: " + valorCampo2);
        Debug.Log("Valor del campo 3: " + valorCampo3);

    }

    public void EnvioDatos(string sUsuario, string sContrasena)
    {
        try 
        {
            ConexionBD conexion = new ConexionBD();
            var coleccion = conexion.ConexionMongo();

            //var filtro = Builders<BsonDocument>.Filter.Eq("Usuario", sUsuario) & Builders<BsonDocument>.Filter.Eq("Contrasena", sContrasena);
            var filtro = Builders<BsonDocument>.Filter.Regex("Usuario", new BsonRegularExpression(sUsuario, "i")) &
             Builders<BsonDocument>.Filter.Regex("Contrasena", new BsonRegularExpression(sContrasena, "i"));

            var usuarioE = coleccion.Find(filtro).FirstOrDefault();

            if (usuarioE != null)
            {
                string idUsuario = usuarioE["_id"].ToString();
                string usuario = usuarioE["Usuario"].ToString();
                PlayerPrefs.SetString("idUsuario", idUsuario);
                PlayerPrefs.SetString("usuario", usuario);
                //Debug.Log("ID del documento insertado: " + idUsuario);
                ControllerUser.Instance.InicioSession(idUsuario, usuario);
                if (home != null && login != null && ranking != null)
                {
                    home.SetActive(true);
                    ranking.SetActive(true);
                    login.SetActive(false);
                }

                if (notificacion != null)
                {
                    labelTitulo.text = $"Hola {usuario}!";
                    labelMensaje.text = "Bienvenido de nuevo. Estamos encantados de verte. ¡Disfruta de tu experiencia en el juego!";
                    notificacion.SetActive(true);
                }
                label.text = $"Hola {usuario} !";


            }
            else
            {
                if (notificacion != null)
                {
                    labelTitulo.text = "Inicio de Sesion Fallido";
                    labelMensaje.text = "Lo sentimos, no hemos podido iniciar sesión con las credenciales proporcionadas. Verifica tu usuario y contraseña e intenta nuevamente. ";
                    notificacion.SetActive(true);

                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Excepción: " + e.ToString());

            if (notificacion != null)
            {
                labelTitulo.text = "Inicio de Sesion Fallido";
                labelMensaje.text = "Lo sentimos, ha ocurrido un error al iniciar sesión. Por favor, contacta al soporte técnico.";
                notificacion.SetActive(true);
            }
        }
        
    }
}
