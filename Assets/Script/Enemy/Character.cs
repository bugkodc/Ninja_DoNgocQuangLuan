using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float hp;
    private string currentAni;
    [SerializeField] private Animator animator;
    [SerializeField] private UiHealth uiHealth;
    [SerializeField] private CombatText combatText;
    public bool isDead => hp <= 0;
    private void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        hp = 100;
        uiHealth.OnInit(hp,transform);
    }
    public virtual void OnDespawn()
    {
        Destroy(uiHealth.gameObject);
    }
    public void OnHit(float damage)
    {
        if (!isDead)
        {
            hp -= damage;
            Instantiate(combatText , transform.position + Vector3.up, Quaternion.identity).OnInit(damage);

            if (isDead)
            {
                hp = 0;
                OnDeath();
            }
            uiHealth.SetNewHp(hp);
        }
    }
    protected virtual void OnDeath()
    {
        ChangeAnimator("dead");
        Invoke(nameof(OnDespawn),2f);
    }
    protected void ChangeAnimator(string nameAnimator)
    {
        if (currentAni != nameAnimator)
        {
            animator.ResetTrigger(nameAnimator);
            currentAni = nameAnimator;
            animator.SetTrigger(currentAni);
        }
    }
    
}
