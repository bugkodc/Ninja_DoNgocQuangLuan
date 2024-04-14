using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatsform : MonoBehaviour
{
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    private Vector3 target;
    [SerializeField] private float speed;
    void Start()
    {
        transform.position = pos1.position;
        target = pos2.position;

    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, pos1.position) < 0.1f)
        {
            target = pos2.position;
        }
        else if(Vector2.Distance(transform.position, pos2.position) <0.1f)
        {
            target = pos1.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            collision.transform.SetParent(null) ;
        }
    }
}
