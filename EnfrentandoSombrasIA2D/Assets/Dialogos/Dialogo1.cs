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
    private Coroutine currentDialogueCoroutine; // Corrutina actual del diálogo

    void Start()
    {
        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("El array de líneas está vacío o no está inicializado.");
            return;
        }
        dialogueText.text = string.Empty;
        StartDialogue();
    }

    public void StartDialogue()
    {
        //isDialogueInProgress = true;
        index = 0; // Reinicia el índice
        StartNextDialogue(); // Inicia el primer diálogo
    }
    private void StartNextDialogue()
    {
        if (currentDialogueCoroutine != null)
            StopCoroutine(currentDialogueCoroutine); // Detiene cualquier corrutina previa

        dialogueText.text = string.Empty; // Limpia el texto anterior
        isDialogueInProgress = true; // Marca que el diálogo está en progreso
        currentDialogueCoroutine = StartCoroutine(ShowDialogue()); // Inicia la nueva línea
    }

    IEnumerator ShowDialogue()
    {
        // Muestra el texto letra por letra
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(TextSpeed);
        }

        isDialogueInProgress = false; // Marca que la línea actual terminó

        // Si es la última línea, espera y carga la escena
        if (index == lines.Length - 1)
        {
            yield return new WaitForSeconds(DelayBeforeSceneLoad);
            LoadNextScene();
        }
    }

    public void DisplayFullTextAutomatically()
    {
        if (isDialogueInProgress) // Si el texto se está escribiendo
        {
            StopCoroutine(currentDialogueCoroutine); // Detiene la animación de la línea
            dialogueText.text = lines[index]; // Muestra toda la línea al instante
            isDialogueInProgress = false;
        }
        else if (index < lines.Length - 1) // Si no se está escribiendo, avanza a la siguiente línea
        {
            index++;
            StartNextDialogue();
        }
        else if (index >= lines.Length - 1)// Si es la última línea, espera y carga la escena
        {
            StartCoroutine(WaitAndLoadScene());
        }
    }

    public void OnButtonClick()
    {
        DisplayFullTextAutomatically(); // Método llamado al hacer clic en un botón
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
