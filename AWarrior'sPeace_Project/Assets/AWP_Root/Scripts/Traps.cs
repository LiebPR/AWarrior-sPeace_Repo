using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    [Header("")]
    public GameObject respawnPoint;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            anim.SetTrigger("Death");

        }
    }

    public void RespawnPlayer()
    {
        anim.SetBool("Idle",true);
        transform.position = respawnPoint.transform.position;
    }

}
