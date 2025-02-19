using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Solo si el jugador toca el trigger
        {
            // Obtener el índice de la escena actual
            int escenaActual = SceneManager.GetActiveScene().buildIndex;

            // Verificar si hay una siguiente escena en la lista
            if (escenaActual + 1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(escenaActual + 1); // Cargar la siguiente escena
            }
            else
            {
                Debug.LogError("❌ No hay más escenas en la lista de Build Settings.");
            }
        }
    }
}

