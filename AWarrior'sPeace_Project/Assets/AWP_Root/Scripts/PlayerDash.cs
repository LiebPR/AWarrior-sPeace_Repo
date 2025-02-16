using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerController _player;
    private float _baseGravity;
    private Animator anim;
    private float VerticalInput;
    

    [Header("Dash")]
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashForce = 20f;
    private bool _isDashing;
    private bool _canDash = true;
    public bool IsDashing => _isDashing;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<PlayerController>();
        _baseGravity = _rb.gravityScale;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

 
    void Update()
    {
       
        
        if (_canDash &&  Input.GetKeyDown(KeyCode.Q) && !_isDashing)
        {
            StartCoroutine(Dash());
        }
        if(_player.IsGrounded && !_canDash)
        {
            _canDash = true; 
        }
    }

   private IEnumerator Dash()
    {

        if ((_player.HorizontalInput != 0 || _player.VerticalInput != 0) && _canDash)
        {
            anim.SetBool("Dash", true);

            _isDashing = true;
            _canDash = false;

            Vector2 dashDirection = new Vector2(_player.HorizontalInput, _player.VerticalInput).normalized;
            _rb.velocity = new Vector2(dashDirection.x * _dashForce, dashDirection.y * _dashForce);
            yield return new WaitForSeconds(_dashingTime);
            _isDashing = false;
            _rb.gravityScale = _baseGravity;
           
            
        }

        

    }

}
