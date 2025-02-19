using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    [Header("PlayerDeath")]
    public GameObject respawnPoint;
    private Animator anim;
    public float playerHealth;
    public float trapDamage;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerHealth <= 0)
        {
            anim.SetTrigger("Death");
            Debug.Log("Player is dead");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            playerHealth -= trapDamage;
        }
    }

    public void RespawnPlayer()
    {
        anim.SetBool("Idle",true);
        transform.position = respawnPoint.transform.position;
        playerHealth = 1;
    }

}
