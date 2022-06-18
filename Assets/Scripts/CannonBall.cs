using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] float BallSpeed;
    private Animator animator;
    private Rigidbody2D _rb;
    private Cannon cannon;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cannon=GetComponentInParent<Cannon>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = new Vector2(cannon.direction.x * BallSpeed, _rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //Debug.Log("BOOM");
            animator.SetTrigger("isBoom");
            collision.GetComponent<PlayerMovement>().TakeHit();
            BallSpeed = 0;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
