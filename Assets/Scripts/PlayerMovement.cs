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
    [SerializeField] private Animator _animator;
    private Rigidbody2D _rb;
    private BoxCollider2D _colider;

    [Space]
    [Header("Horizontal Movement")]
    [SerializeField] private float WalkSpeed = 10f;
    private bool isRight = true;
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
    public bool isDead=false;
    
    [Space]

    public MovementState mState = MovementState.Idle;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] GameObject Inventory;
    [SerializeField] DisplayInventory display;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _colider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //Get ground
        isGrounded = IsGrounded();
        _animator.SetBool("isGround", isGrounded);

        float horizontalMove = Input.GetAxisRaw("Horizontal");

        //Walk
        Move(horizontalMove);
        _animator.SetFloat("Move", Mathf.Abs(horizontalMove));

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Jump");
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
            FindObjectOfType<InventorySystem>().ListItems();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Inventory.SetActive(!Inventory.activeSelf);
            display.UpdateInventory();
        }


        //Jump force
        //modifyPhysics();
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
        if (horizontal > 0 && !isRight) FlipDirection();
        if (horizontal < 0 && isRight) FlipDirection();
    }
    private void Jump()
    {
        if (isGrounded)
        {
            float jumpForceTest = Mathf.Sqrt(-2 * jumpHeight * (Physics2D.gravity.y * _rb.gravityScale));
            _rb.AddForce(new Vector2(0, jumpForceTest), ForceMode2D.Impulse);
            mState = MovementState.Jump;
        }
    }
    private void FlipDirection()
    {
        isRight =!isRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    private bool IsGrounded()
    {
        Vector2 position = _colider.bounds.center;
        position.y -= _colider.size.y;

        Vector2 position2 = position;
        position2.x+=_colider.size.x;

        Vector2 direction = Vector2.down;
        float distance = .1f;

        Debug.DrawRay(position, direction, Color.yellow);
        Debug.DrawRay(_colider.bounds.min, direction, Color.yellow);
        Debug.DrawRay(position2, direction, Color.yellow);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, jumpableGround);
        RaycastHit2D hit2 = Physics2D.Raycast(_colider.bounds.min, direction, distance, jumpableGround);
        RaycastHit2D hit3 = Physics2D.Raycast(position2, direction, distance, jumpableGround);
        if (hit.collider != null || hit2.collider != null || hit3.collider != null)
        {
            return true;
        }

        return false;
        //return Physics2D.BoxCast(_colider.bounds.center, _colider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
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
