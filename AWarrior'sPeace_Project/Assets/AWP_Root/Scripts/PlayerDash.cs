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
    [SerializeField] private float _timeCanDash = 1f;
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
        Debug.Log("Vertical Input: " + _player.VerticalInput);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {

        if ((_player.HorizontalInput != 0 || _player.VerticalInput != 0) && _canDash)
        {
            anim.SetBool("Dash", true);

            _isDashing = true;
            _canDash = false;
            _rb.gravityScale = 0f;
            _rb.velocity = new Vector2(_player.HorizontalInput * _dashForce, _player.VerticalInput * _dashForce);
            yield return new WaitForSeconds(_dashingTime);
            _isDashing = false;
            _rb.gravityScale = _baseGravity;
            yield return new WaitForSeconds(_timeCanDash);
            _canDash = true;
        }

        

    }

}
