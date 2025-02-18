using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeFall : MonoBehaviour
{
    [SerializeField] float speed = 8; 
    [SerializeField] Transform[] points;
    [SerializeField] private Collider2D triggerCollider;

    private int targetIndex = 1;
    private bool spikeFall = false;

    // Start is called before the first frame update
    void Start()
    {
        
        transform.position = points[0].position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spikeFall && points.Length > 1) 
        {
            MoveSpike();
        }
    }

    void MoveSpike()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[targetIndex].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, points[targetIndex].position) < 0.02f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spikeFall = true;
            triggerCollider.enabled = false;
            

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("La estalactita te ha golpeado");
        }
    }

}
