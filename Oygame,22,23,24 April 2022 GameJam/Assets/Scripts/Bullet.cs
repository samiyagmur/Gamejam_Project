using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 20f;

    private Rigidbody2D _rigidbody;
    private PlayerController _player;

    private float _xSpeed;
    private float _ySpeed;
    
    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerController>();

        _xSpeed = -_player.transform.localScale.x * speed;
        _ySpeed = -_player.transform.localScale.y * speed;
        
        Destroy(gameObject, 3f);  
    }

    // Update is called once per frame
    private void Update()
    {
        if (_player.GetIsVertıcal())
        {
            _rigidbody.velocity = new Vector2(0f, -_ySpeed);
        }
        else
        {
            _rigidbody.velocity = new Vector2(_xSpeed, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Enemy") && !col.tag.Equals("Player"))
        {
            // TODO: Düşmana vur
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);  
        }
    }
}