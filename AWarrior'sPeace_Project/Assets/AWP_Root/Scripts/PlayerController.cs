using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Variables de Referencia
    private Rigidbody2D playerAWP;
    private Animator anim;
    private float horizontalInput;


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
}
