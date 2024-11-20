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
    private Coroutine currentDialogueCoroutine; // Corrutina actual del di�logo

    void Start()
    {
        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("El array de l�neas est� vac�o o no est� inicializado.");
            return;
        }
        dialogueText.text = string.Empty;
        StartDialogue();
    }

    public void StartDialogue()
    {
        //isDialogueInProgress = true;
        index = 0; // Reinicia el �ndice
        StartNextDialogue(); // Inicia el primer di�logo
    }
    private void StartNextDialogue()
    {
        if (currentDialogueCoroutine != null)
            StopCoroutine(currentDialogueCoroutine); // Detiene cualquier corrutina previa

        dialogueText.text = string.Empty; // Limpia el texto anterior
        isDialogueInProgress = true; // Marca que el di�logo est� en progreso
        currentDialogueCoroutine = StartCoroutine(ShowDialogue()); // Inicia la nueva l�nea
    }

    IEnumerator ShowDialogue()
    {
        // Muestra el texto letra por letra
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(TextSpeed);
        }

        isDialogueInProgress = false; // Marca que la l�nea actual termin�

        // Si es la �ltima l�nea, espera y carga la escena
        if (index == lines.Length - 1)
        {
            yield return new WaitForSeconds(DelayBeforeSceneLoad);
            LoadNextScene();
        }
    }

    public void DisplayFullTextAutomatically()
    {
        if (isDialogueInProgress) // Si el texto se est� escribiendo
        {
            StopCoroutine(currentDialogueCoroutine); // Detiene la animaci�n de la l�nea
            dialogueText.text = lines[index]; // Muestra toda la l�nea al instante
            isDialogueInProgress = false;
        }
        else if (index < lines.Length - 1) // Si no se est� escribiendo, avanza a la siguiente l�nea
        {
            index++;
            StartNextDialogue();
        }
        else if (index >= lines.Length - 1)// Si es la �ltima l�nea, espera y carga la escena
        {
            StartCoroutine(WaitAndLoadScene());
        }
    }

    public void OnButtonClick()
    {
        DisplayFullTextAutomatically(); // M�todo llamado al hacer clic en un bot�n
    }

    IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(DelayBeforeSceneLoad);
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        string snivel = nivel == 0 ? "MenuInicial" :
                        ControllerUser.Instance.personaje == 1 ? $"Nivel{nivel}" : $"Nivel{nivel}T";

        Debug.Log($"Cargando escena: {snivel}");
        SceneManager.LoadScene(snivel); // Carga la escena correspondiente
    }
}
