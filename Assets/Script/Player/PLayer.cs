using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    private bool isGround;
    private bool isAttack;
    private bool isJump = false;
    private float horizontal;
    public float speed;
    public float jumpSpeed;
    private string currentAni;
    private int coin;
    private Vector2 checkPoint;
    [SerializeField] private GameObject kunaiPrefab;
    [SerializeField] private Transform throwPos;
    [SerializeField] private GameObject attackAren;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coin = PlayerPrefs.GetInt("coin",0);
        checkPoint =  transform.position;
        OnInit();
        
    }
    void Update()
    {
        if (isDead) return;
       // AttackCheck();
        InputMovement();
        if (isAttack && isGround)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        // Jump();
    }
    private void FixedUpdate()
    {
       isGround = CheckGround();
    }
    private void InputMovement()
    {
       // horizontal = Input.GetAxisRaw("Horizontal");
        if(Mathf.Abs(horizontal) > 0.1f)
        {
            ChangeAnimator("run");
            rb.velocity = new Vector2(horizontal * speed , rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        else if (isGround)
        {
            ChangeAnimator("idle");
            rb.velocity = Vector2.zero;
        }
    }
    private void Jump()
    {
        if (isGround)
        {
            isJump = true;
            ChangeAnimator("jump");
            rb.AddForce(jumpSpeed * Vector2.up);
        }
        if (!isGround && rb.velocity.y < 0)
        {
            ChangeAnimator("fall");
            isJump = false;
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        isAttack = false;
        transform.position = checkPoint;
        ChangeAnimator("idle");
        UiManager.instance.SetTextCoin(coin);
    }
    private bool CheckGround()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.3f,Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down , 1.3f,groundLayer);
        return hit.collider != null;
    }
    private void AttackCheck()
    {
        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Throw();
            }
            if (isAttack)
            {
                rb.velocity = Vector2.zero;
                return;
            }

        }

    }
    private void Attack()
    {
        isAttack = true;
        ChangeAnimator("attack");
        attackAren.SetActive(true);
        Invoke(nameof(ResetAttack), 0.5f);
    }
    private void ResetAttack()
    {
        isAttack = false;
        attackAren.SetActive(false);
        ChangeAnimator("idle");
    }
    private void Throw()
    {
        isAttack = true;
        rb.velocity = Vector2.zero;
        ChangeAnimator("throw");
        Invoke(nameof(ResetAttack), 0.5f);
        Instantiate(kunaiPrefab, throwPos.position, throwPos.rotation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "item")
        {
            coin++;
            PlayerPrefs.GetInt("coin", coin);
            UiManager.instance.SetTextCoin(coin);
            Destroy(collision.gameObject);
        }
        if(collision.tag == "dead")
        {
            
            ChangeAnimator("dead");
            Invoke(nameof(RestPlayer), 1f);
        }
        if(collision.tag == "checkpoint")
        {
            checkPoint = collision.transform.position;
        }
    }
    private void RestPlayer()
    {
        transform.position = checkPoint;
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        OnInit();
    }
    public void SetInputMove(float horizontal)
    {
        this.horizontal = horizontal;
    }
    public void SetInputJump()
    {
        Jump();

    }
    public void SetInputAttack()
    {
        if(isGround) Attack();
    }
    public void SetInputThrow()
    {
        if (isGround) Throw();
    }
}
