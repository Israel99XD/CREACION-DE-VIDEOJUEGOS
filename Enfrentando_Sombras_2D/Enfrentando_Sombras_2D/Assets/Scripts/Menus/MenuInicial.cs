using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MenuInicial : MonoBehaviour
{
    public GameObject niveles;
    public GameObject home;
    public GameObject notificacion;
    public GameObject login;
    public GameObject register;
    public GameObject ranking;
    public GameObject btnranking;
    public GameObject iconoN1;
    public GameObject iconoN2;
    public GameObject iconoN3;

    public TMP_Text labelTitulo;
    public TMP_Text labelMensaje;

    public Toggle PantallaCompleta;
    public Slider Volumen;
    public TMP_Dropdown Calidad;

    public RowUi rowUi;
    public GameObject tableContainer;

    public TMP_Text nombreJugador;

    Color colorCase1 = new Color(1f, 0.823f, 0f); // Color hexadecimal: #FFD200
    Color colorCase2 = new Color(0.776f, 0.776f, 0.776f); // Color hexadecimal: #C6C6C6
    Color colorCase3 = new Color(0.715f, 0.435f, 0.337f); // Color hexadecimal: #B76F56
    void Start()
    {
        
        if (PlayerPrefs.HasKey("idUsuario") && PlayerPrefs.HasKey("usuario"))
        {
            nombreJugador.text = $"Hola {PlayerPrefs.GetString("usuario")} !";
            if (btnranking != null)
            {
                btnranking.SetActive(false);
            }
        }
        else
        {
            ControllerUser.Instance.InicioSession(string.Empty, string.Empty);
            if (btnranking != null)
            {
                btnranking.SetActive(true);
            }
        }
        ControllerUser.Instance.SetCalidad(PlayerPrefs.HasKey("calidad") ? PlayerPrefs.GetInt("calidad"):3);
        ControllerUser.Instance.SetPantalla(PlayerPrefs.HasKey("pantallaCompleta") ? (PlayerPrefs.GetInt("pantallaCompleta") ==1?true:false):true);
        ControllerUser.Instance.SetVolumen(PlayerPrefs.HasKey("volumen") ? PlayerPrefs.GetFloat("volumen"):0f);

        PantallaCompleta.isOn = ControllerUser.Instance.GetPantalla();
        Volumen.value = ControllerUser.Instance.GetVolumen();
        Calidad.value = ControllerUser.Instance.GetCalidad();
    }
    
    void Update()
    {
        if (PlayerPrefs.HasKey("idUsuario"))
        {
            if (btnranking != null)
            {
                btnranking.SetActive(true);
            }
        }
        else
        {
            if (btnranking != null)
            {
                btnranking.SetActive(false);
            }
        }
        PantallaCompleta.isOn = ControllerUser.Instance.GetPantalla();
        Volumen.value = ControllerUser.Instance.GetVolumen();
        Calidad.value = ControllerUser.Instance.GetCalidad();
        ControllerUser.Instance.SetPosion(false);
    }
    public void Jugar(int index)
    {
        ControllerUser.Instance.SetPosion(false);
        ControllerUser.Instance.personaje = index;
        SceneManager.LoadScene("Cinematica3");
        //SceneManager.LoadScene("CinematicaFinal");
    }

    public void Niveles()
    {
        if (PlayerPrefs.HasKey("idUsuario"))
        {
            // El valor existe, puedes recuperarlo
            string idUsuario = PlayerPrefs.GetString("idUsuario");
            string nameUsuario = PlayerPrefs.GetString("usuario");
            ControllerUser.Instance.InicioSession(idUsuario,nameUsuario);
            if (home != null && niveles != null) {
                niveles.SetActive(true);
                home.SetActive(false);
            }
        }
        else
        {
            // El valor no existe o no se ha guardado todavía
            if (notificacion != null)
            {
                labelTitulo.text = "Accede para jugar";
                labelMensaje.text = "Por favor, inicia sesión para acceder a todos nuestros niveles y disfrutar al máximo de la experiencia de juego.";
                notificacion.SetActive(true);

            }
        }
    }

    public void Login()
    {
        if (PlayerPrefs.HasKey("idUsuario"))
        {
            if (notificacion != null)
            {
                labelTitulo.text = "Sesion Activa";
                labelMensaje.text = "Ya estás autenticado. Si deseas iniciar sesión con otra cuenta, por favor, cierra la sesión actual primero.";
                notificacion.SetActive(true);

            }

        }
        else
        {
            if (home != null && login != null)
            {
                login.SetActive(true);
                home.SetActive(false);
            }

        }
    }

    public void Register()
    {
        if (PlayerPrefs.HasKey("idUsuario"))
        {
            if (notificacion != null)
            {
                labelTitulo.text = "Sesion Activa";
                labelMensaje.text = "Ya estás autenticado. Si deseas crear una nueva cuenta, por favor, cierra la sesión actual primero.";
                notificacion.SetActive(true);
            }

        }
        else
        {
            if (home != null && register != null)
            {
                register.SetActive(true);
                home.SetActive(false);
            }

        }
    }

    public void Salir()
    {
        nombreJugador.text = string.Empty;
        Debug.Log("Salir...");
        PlayerPrefs.DeleteKey("idUsuario");
        PlayerPrefs.DeleteKey("usuario");
        Application.Quit();
    }
    public void Ranking(int nivel)
    {
        if (nivel == 4)
        {
            ranking.SetActive(true);
            home.SetActive(false);
            nivel = 1;
        }

        if (nivel == 1)
        {
            iconoN1.SetActive(true);
            iconoN2.SetActive(false);
            iconoN3.SetActive(false);
        }
        if (nivel == 2)
        {
            iconoN1.SetActive(false);
            iconoN2.SetActive(true);
            iconoN3.SetActive(false);
        }
        if (nivel == 3)
        {
            iconoN1.SetActive(false);
            iconoN2.SetActive(false);
            iconoN3.SetActive(true);
        }

        ConexionBD conexion = new ConexionBD();
        var coleccion = conexion.ConexionMongo2();
        string idUserA = PlayerPrefs.GetString("idUsuario");
        var filtro = Builders<BsonDocument>.Filter.Eq("nivel", nivel) ;
        var usuariosTopScore = coleccion.Find(filtro)
            .Sort(Builders<BsonDocument>.Sort.Descending("score"))
            .Limit(10)
            .ToList();

        if (usuariosTopScore != null && usuariosTopScore.Any())
        {
            DeleteAllChildren();
            int position = 0;
            foreach (var usuario in usuariosTopScore)
            {
                var row = Instantiate(rowUi, tableContainer.transform).GetComponent<RowUi>();
                position += 1;
                var score = usuario["score"].ToString();
                var name = usuario["user"].ToString();

                string rankString;
                switch (position)
                {
                    default:
                        rankString = position.ToString() + "TH"; break;

                    case 1: rankString = "1ST"; break;
                    case 2: rankString = "2ND"; break;
                    case 3: rankString = "3RD"; break;
                }

                Debug.Log($"{position} - {name} - {score},");
                if (position == 1)
                {
                    row.rank.color = Color.green;
                    row.name.color = Color.green;
                    row.score.color = Color.green;
                }

                row.rank.text = rankString;
                row.name.text = score;
                row.score.text = name;

                switch (position)
                {
                    default:
                        row.trophy.enabled = false;
                        break;
                    case 1:
                        row.trophy.color = colorCase1;
                        break;
                    case 2:
                        row.trophy.color = colorCase2;
                        break;
                    case 3:
                        row.trophy.color = colorCase3;
                        break;

                }

            }
        }
        else {
            DeleteAllChildren();
            var row = Instantiate(rowUi, tableContainer.transform).GetComponent<RowUi>();
            row.rank.text = "N/A";
            row.name.text = "N/A";
            row.score.text = "N/A";
            row.trophy.enabled = false;
        }
    }

    void DeleteAllChildren()
    {
        foreach (Transform child in tableContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }


}
