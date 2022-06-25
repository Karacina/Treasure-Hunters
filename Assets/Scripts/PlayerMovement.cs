using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState
{
    Idle, Run, Jump, Fall, Ground
};

public class PlayerMovement : MonoBehaviour
{
    // COMPONENTS
    private Animator _animator;
    private Rigidbody2D _rb;
    private BoxCollider2D _colider;

    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private float _groundCheckSize;
    [Space]
    [Header("Horizontal Movement")]
    [SerializeField] private float WalkSpeed = 10f;

    public bool canMove;

    [Space]
    [Header("Vertical Movement")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = 1f;
    //[SerializeField] private float gravityScale = 10;
    [SerializeField] private float fallingGravityScale = 5f;
    public bool isGrounded = false;
    public bool isFalling = false;

    [Header("Parametrs")]
    [SerializeField] private float healPoints = 100f;

    [Space]
    [Header("Booleans")]
    public bool isDead = false;

    // STATE PARAMETRS
    private bool _isFacingRight = true;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [Space]

    public MovementState mState = MovementState.Idle;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] GameObject Inventory;
    [SerializeField] DisplayInventory display;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _colider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Ground check


        isGrounded = IsGrounded();
        _animator.SetBool("isGround", isGrounded);

        float horizontalMove = Input.GetAxisRaw("Horizontal");

        // Walk
        Move(horizontalMove);
        _animator.SetFloat("Move", Mathf.Abs(horizontalMove));

        // JUMP

        // Coyote Time
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            _animator.SetTrigger("Jump");
            Jump();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            coyoteTimeCounter = 0;
        }

        // Inventory
        if (Input.GetKeyDown(KeyCode.LeftShift))
            FindObjectOfType<InventorySystem>().ListItems();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Inventory.SetActive(!Inventory.activeSelf);
            display.UpdateInventory();
        }

        // Jump force
        modifyPhysics();
        _animator.SetBool("isFalling", isFalling);
    }
    private void modifyPhysics()
    {
        isFalling = false;
        if (!isGrounded)
        {

            if (_rb.velocity.y < 0)
            {
                _rb.gravityScale = gravity * fallingGravityScale;
                isFalling = true;
            }
            else if (_rb.velocity.y > 0)
            {
                _rb.gravityScale = gravity * (fallingGravityScale / 1.5f);
            }
        }
    }

    private void Move(float horizontal)
    {
        _rb.velocity = new Vector2(horizontal * WalkSpeed, _rb.velocity.y);
        if (horizontal != 0)
            mState = MovementState.Run;
        else
            mState = MovementState.Idle;

        //Flip direction
        if (horizontal > 0 && !_isFacingRight) FlipDirection();
        if (horizontal < 0 && _isFacingRight) FlipDirection();
    }
    private void Jump()
    {
        float jumpForceTest = Mathf.Sqrt(-2 * jumpHeight * (Physics2D.gravity.y * _rb.gravityScale));

        _rb.AddForce(new Vector2(0, jumpForceTest), ForceMode2D.Impulse);
        mState = MovementState.Jump;
        jumpBufferCounter = 0f;
    }
    private void FlipDirection()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        _isFacingRight = !_isFacingRight;    //flip bool
    }
    private bool IsGrounded()
    {
        //return _colider.IsTouchingLayers(jumpableGround);
        //return Physics2D.BoxCast(_colider.bounds.center, _colider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        return Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckSize, jumpableGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheckPoint.position, _groundCheckSize);
    }
    public void TakeHit()
    {
        float damage = 20f;
        healPoints -= damage;
        if (healPoints <= 0) isDead = true;
        if (isDead)
        {
            Debug.Log("Player is dead");
            _animator.SetBool("isDead", isDead);
        }
        _animator.SetTrigger("isHit");
    }
}