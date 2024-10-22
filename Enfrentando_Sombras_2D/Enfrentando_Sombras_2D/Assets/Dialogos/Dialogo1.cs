using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogo1 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float TextSpeed = 0.01f;
    public float DelayBeforeSceneLoad = 2f;
    [SerializeField] public int nivel;
    private int index;
    private bool isDialogueInProgress;


    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
    }

    public void StartDialogue()
    {
        isDialogueInProgress = true;
        index = 0;
        StartCoroutine(ShowDialogue());
    }

    IEnumerator ShowDialogue()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(TextSpeed);
        }

        isDialogueInProgress = false;

        if (index == lines.Length - 1)
        {
            yield return new WaitForSeconds(DelayBeforeSceneLoad);
            string snivel = string.Empty;
            if (nivel == 0)
            {
                snivel = "MenuInicial";
            }
            else
            {
                snivel = ControllerUser.Instance.personaje == 1 ? $"Nivel{nivel}" : $"Nivel{nivel}T";
            }
            Debug.Log(snivel);
            SceneManager.LoadScene(snivel);
        }
    }

    public void DisplayFullTextAutomatically()
    {
        if (isDialogueInProgress && index < lines.Length)
        {
            StopAllCoroutines(); // Detenemos la escritura autom�tica del di�logo actual
            dialogueText.text = lines[index]; // Mostramos la l�nea completa

            // Avanzamos al siguiente di�logo si no es la �ltima l�nea
            if (index < lines.Length - 1)
            {
                StartCoroutine(ShowDialogueWithDelay()); // Mostramos el siguiente di�logo con un retraso
            }
            else // Si es la �ltima l�nea, esperamos antes de cargar la siguiente escena
            {
                isDialogueInProgress = false;
                StartCoroutine(WaitAndLoadScene());
            }
        }
        else if (!isDialogueInProgress && index < lines.Length - 1) // Si el di�logo no est� en progreso, avanzamos a la siguiente l�nea
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(ShowDialogue());
        }
    }

    IEnumerator ShowDialogueWithDelay()
    {
        yield return new WaitForSeconds(2f); // Cambia el valor a la cantidad de segundos que desees esperar
        index++;
        dialogueText.text = string.Empty;
        StartCoroutine(ShowDialogue());
    }

    IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(DelayBeforeSceneLoad);

        string snivel = string.Empty;
        if (nivel == 0)
        {
            snivel = "MenuInicial";
        }
        else
        {
            snivel = ControllerUser.Instance.personaje == 1 ? $"Nivel{nivel}" : $"Nivel{nivel}T";
        }
        Debug.Log(snivel);
        SceneManager.LoadScene(snivel);
    }
    // Puedes llamar a este m�todo desde un bot�n u otro evento para avanzar manualmente en el di�logo
    public void OnButtonClick()
    {
        DisplayFullTextAutomatically();
    }
}
