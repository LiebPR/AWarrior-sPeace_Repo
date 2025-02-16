using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Variables de Referencia
    private Rigidbody2D playerAWP;
    private Animator anim;
    private float horizontalInput;

    //Variables attack
    public GameObject attackPoint;
    public float radius;
    public LayerMask enemies;
    public float damage;

    //Variables de estadísticas del player
    public float speed;
    public float jumpForce;
    private bool isFacingRight = true;
    [SerializeField] bool isGrounded;
    [SerializeField] GameObject groundCheck;
    [SerializeField] LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        playerAWP = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, 0.1f, groundLayer);
        Movement();
        Jump();
        
        //Ataque animación
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }
   
    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        playerAWP.velocity = new Vector2(horizontalInput *  speed, playerAWP.velocity.y);

        //Flip: si el valor del imput es igual a 0
        if (horizontalInput > 0)
        {
            anim.SetBool("Run", true);
            if (!isFacingRight)
            {
                Flip();
            }
        }
        if (horizontalInput < 0)
        {
            anim.SetBool("Run", true);
            if (isFacingRight)
            {
                Flip();
            } 
        }
        if (horizontalInput == 0)
        {
            anim.SetBool("Run", false);
        }
    }

    void Jump()
    {
        anim.SetBool("Jump", !isGrounded);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            playerAWP.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
        
        
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }

    //Attack
    public void attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        foreach (Collider2D enemyGameObject in enemy)
        {
            Debug.Log("Hit enemy");
            enemyGameObject.GetComponent<EnemyHealth>().health -= damage;
        }
    }

    //Ver el círculo attack point
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}
