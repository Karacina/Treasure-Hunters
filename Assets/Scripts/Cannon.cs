using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform StartPosition;
    [SerializeField] GameObject Ball;
    [SerializeField] Cooldown timer;
    [Space]
    public Vector2  direction;
    public float distance;
    private Animator animator;

    public bool IsFire;
    private void Start()
    {
        IsFire = false;
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(StartPosition.position, direction, Color.yellow);
        RaycastHit2D hit = Physics2D.Raycast(StartPosition.position, direction, distance);
        if(hit)
            if (hit.collider.gameObject.tag=="Player" && timer.isReady)
                Fire();
    }
    public void Fire()
    {
        //Debug.Log("Fire");
        GameObject ball = Instantiate(Ball,gameObject.transform);
        ball.transform.position = StartPosition.position;
        animator.SetTrigger("isFire");
        timer.Reset();
    }
}
