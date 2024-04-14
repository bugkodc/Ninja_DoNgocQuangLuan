using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject attackAren;
    private ISate currentState;
    private bool isRight = true;
    private Character target;
    public Character Target => target;
    private void Update()
    {
        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(gameObject);
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeSate(new idleSate());
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        ChangeAnimator(null);
    }
    public void Moving()
    {
         ChangeAnimator("run");
        rigidbody2D.velocity = transform.right * moveSpeed;
    }
    public void StopMoving()
    {
        ChangeAnimator("idle");
        rigidbody2D.velocity = Vector2.zero;
    }
    public void Attack()
    {
        ChangeAnimator("attack");
        attackAren.SetActive(true);

    }
    public bool IsTargetInRange()
    {
        if(target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            attackAren.SetActive(false);
            return false;

        }
        
    }
    public void ChangeSate(ISate sate)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = sate;
        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemywall") 
        {
            ChangeDirection(!isRight);
        }
    }
    public virtual void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation =isRight ?  Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }
    public virtual void SetTarget(Character character)
    {
        this.target = character;
        if (IsTargetInRange())
        {
            ChangeSate(new attackState());
        }
        else if(target != null)
        {
            ChangeSate(new patrolState());
        }
        else
        {
            ChangeSate(new idleSate());
        }
    }
}
