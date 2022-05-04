using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float damageAmount = 10f;
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
   

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }
    private void FixedUpdate()
    {
        moveCharacter(movement);
    }
    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            FindObjectOfType<GameSession>().SetPlayerHealth(-damageAmount);
        }

        if (col.gameObject.tag.Equals("Bullet"))
        {
            health -= 20;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
            
        }
    }

    public void SetHealth(float value)
    {
        health += value;

        CheckDie();
    }

    private void CheckDie()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

   
}
