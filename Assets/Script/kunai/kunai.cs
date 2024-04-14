using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kunai : MonoBehaviour
{
    [SerializeField] private GameObject hit_VFX;
    [SerializeField] private Rigidbody2D rb;
    private void Start()
    {
        OnInt();
    }
    public void OnInt()
    {
        rb.velocity = transform.right * 5f;
        Invoke(nameof(OnDespawn),4f);
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy") 
        {
            collision.GetComponent<Character>().OnHit(30f);
            Instantiate(hit_VFX,transform.position,transform.rotation);
            OnDespawn();    
        }
    }
}
