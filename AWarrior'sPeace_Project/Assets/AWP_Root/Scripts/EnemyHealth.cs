using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float health;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            anim.SetTrigger("Death");
            Debug.Log("Enemy is dead");
        }
    }
    public void apagarEnemigo()
    {
        gameObject.SetActive(false);
    }
}
