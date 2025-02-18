using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Configuración de Parallax")]
    public Transform[] backgrounds;    // Array de capas de fondo
    public float[] parallaxScales;     // Velocidad de movimiento para cada fondo

    private Transform cam;             // La cámara principal
    private Vector3 previousCamPos;   // Posición anterior de la cámara

    // Límite de los fondos, para evitar que se alejen demasiado
    public float leftLimit = -10f;
    public float rightLimit = 10f;

    void Start()
    {
        // Obtener la cámara principal y su posición inicial
        cam = Camera.main.transform;
        previousCamPos = cam.position;
    }

    void Update()
    {
        // Calcular cuánto se ha movido la cámara en el eje X
        float parallaxAmount = cam.position.x - previousCamPos.x;

        // Recorrer todas las capas de fondo
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Mover cada capa de fondo con una velocidad de parallax personalizada
            float targetPosX = backgrounds[i].position.x + parallaxAmount * parallaxScales[i];

            // Establecer el nuevo objetivo para cada capa de fondo
            Vector3 targetPos = new Vector3(targetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = targetPos;

            // Repetir el fondo si se mueve fuera de los límites
            RepeatBackground(backgrounds[i], i);
        }

        // Actualizar la posición anterior de la cámara
        previousCamPos = cam.position;
    }

    // Método para hacer que los fondos se repitan cuando se mueven fuera de la vista
    void RepeatBackground(Transform background, int index)
    {
        // Obtener la distancia entre la cámara y el fondo
        float backgroundWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;

        // Si el fondo se mueve más allá de los límites de la cámara (derecha o izquierda), reposicionarlo
        if (background.position.x < cam.position.x - backgroundWidth)
        {
            background.position = new Vector3(cam.position.x + backgroundWidth * 2f, background.position.y, background.position.z);
        }
        else if (background.position.x > cam.position.x + backgroundWidth)
        {
            background.position = new Vector3(cam.position.x - backgroundWidth * 2f, background.position.y, background.position.z);
        }

        // Limitar el movimiento del fondo dentro de los límites configurados
        float clampedX = Mathf.Clamp(background.position.x, leftLimit, rightLimit);
        background.position = new Vector3(clampedX, background.position.y, background.position.z);
    }
}
