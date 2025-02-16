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
    private float verticalInput;
    private PlayerDash _playerDash;
    public float HorizontalInput => horizontalInput;
    public float VerticalInput => verticalInput;
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

    private void Awake()
    {
        playerAWP = GetComponent<Rigidbody2D>();
        _playerDash = GetComponent<PlayerDash>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, 0.1f, groundLayer);
       
        
        if (!_playerDash.IsDashing)
        {
            Jump();
        }
        if (!_playerDash.IsDashing)
        {
            Movement();
        }

        //Ataque animación
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }
   
    void Movement()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        float currentYVelocity = playerAWP.velocity.y; // Mantiene la caída natural

        if (_playerDash.IsDashing)
        {
            // Permitir movimiento libre en ambos ejes durante el dash
            playerAWP.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);
        }
        else
        {
            // Mantener la gravedad cuando no está dashing
            playerAWP.velocity = new Vector2(horizontalInput * speed, currentYVelocity);
        }

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
