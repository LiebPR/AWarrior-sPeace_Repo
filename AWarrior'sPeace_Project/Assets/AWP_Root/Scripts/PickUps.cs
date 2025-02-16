using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    //Para examen: poner la líne 
    //public int pointSum;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PointsUp(1);
            gameObject.SetActive(false);
        }
        AudioManager.Instance.PlaySFX("PickUp");
    }
}
