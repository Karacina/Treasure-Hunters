using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator _animator;
    

    [Header("Animations")]
    [SerializeField] private AnimatorOverrideController animatorOverride;
    [SerializeField] private RuntimeAnimatorController controller;
    
    [Space]
    [SerializeField] private Transform attackHitBoxPos;
    [SerializeField] float attackRadius, attackDamage;
    [SerializeField] LayerMask isDamagable;
    [SerializeField] private float lastInputTime=Mathf.NegativeInfinity;
    [SerializeField] private float inputTimer;

    [Space]
    public bool withSword = false;
    public bool attacked = false;
    public float ATTACKENUM = 0;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Attack
        CheckCombatInput();
        SetCombatAnimation();
        CheckAttacks();  
    }

    private void CheckCombatInput()
    {
        if(Input.GetMouseButtonDown(0) )
        {
            withSword = true;
            attacked = true;
            _animator.SetBool("withSword", true);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            withSword = !withSword;
            _animator.SetBool("withSword", withSword);
        }
    }
    private void SetCombatAnimation()
    {
        if (withSword)
        {
            _animator.runtimeAnimatorController = animatorOverride;
        }
        else
        {
            _animator.runtimeAnimatorController = controller;
        }
    }

    private void CheckAttacks()
    {
        if (attacked)
        {
            // perform attack;
            _animator.SetTrigger("attack");
            _animator.SetFloat("Attack", ATTACKENUM);
            attacked = false;
        }

        //if (Time.time >= lastInputTime + inputTimer)
        //{
        //    withSword = false;
        //}
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attackRadius,isDamagable);
        
        foreach(Collider2D collider in detectedObjects)
        {
            Debug.Log("destroyed");
            Destroy(collider.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attackRadius);
    }
}
