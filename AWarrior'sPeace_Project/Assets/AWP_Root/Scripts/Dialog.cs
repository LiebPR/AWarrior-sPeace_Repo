using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    // En referencia al Dialogo
    private bool didDialogueStart;
    private int lineIndex;
    private bool isTyping;
    private float typingTime = 0.05f;
    private bool isPlayerInRange;
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    void Update()
    {
        if (isPlayerInRange && Input.GetButtonDown("Fire2"))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (!isTyping && dialogueText.text.Equals(dialogueLines[lineIndex]))
            {
                NextDialogueLine();
            }
            else if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
                isTyping = false;
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;

        // Inicia el diálogo
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;

            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void EndDialogue()
    {
        didDialogueStart = false;
        dialoguePanel.SetActive(false);
        dialogueMark.SetActive(true);
    }

    // Cuando el jugador entra en el área de trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;

            // Solo activa el marcador si no está en curso el diálogo
            if (!didDialogueStart)
            {
                dialogueMark.SetActive(true);
            }
        }
    }

    // Cuando el jugador sale del área de trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;

            // Desactiva el marcador solo si no hay diálogo en curso
            if (!didDialogueStart)
            {
                dialogueMark.SetActive(false);
            }

            // Si el diálogo está en curso y el jugador sale del trigger, se termina el diálogo
            if (didDialogueStart)
            {
                EndDialogue();
            }
        }
    }
}