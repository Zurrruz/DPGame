using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private float _hitPoints;
    [SerializeField]
    private float _speed;
    [SerializeField, Tooltip("Сила прыжка")]
    private float _jumpForce = 7f;
    private bool _jumpControl;
    private float jumpIteration;
    private float jumpValueIteration = 60;
    [SerializeField, Tooltip("Количество повторяемых прыжков")]
    private int _extraJumpVelue;
    private int _extraJump;
    [SerializeField, Tooltip("Сила второго прыжка")]
    private float _doubleJump = 10f;
    [SerializeField]
    private float _attackDamage;

    private bool _onGround;
    [SerializeField]
    private Transform _groundCheck;
    private float _checkRadius = 0.3f;
    [SerializeField]
    private LayerMask Ground;
        
    public static int strength;   
    public static int agility;
    public static int intellect = 50;

    private Rigidbody2D _rb;
    private Animator _anim;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
        
    void Update()
    {
        Move();
        Checking();
        Jump();
    }

    private void Move()
    {
        var hor = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(hor * _speed, _rb.velocity.y);
    }

    public void Jump()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            if (_onGround)
                _jumpControl = true;
        }
        else
        {
            _jumpControl = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_onGround && (++_extraJump < _extraJumpVelue))
        {
            _rb.velocity = new Vector2(0, _doubleJump);
        }
        if (_onGround)
            _extraJump = 0;

        if (_jumpControl)
        {
            if (jumpIteration++ < jumpValueIteration)
            {
                _rb.AddForce(Vector2.up * _jumpForce / jumpIteration);
            }
        }
        else
        {
            jumpIteration = 0;
        }
    }

    void Checking()
    {
        _onGround = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, Ground);
    }
}
