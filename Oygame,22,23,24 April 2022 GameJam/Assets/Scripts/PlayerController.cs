using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gun;
    [SerializeField] private ParticleSystem particle;
    
    private Rigidbody2D m_Rigidbody;
    private BoxCollider2D m_BoxCollider;
    private PolygonCollider2D m_PolygonCollider;
    private Animator m_Animator;
    
    private Vector2 _moveInput;
    private bool _isAlive = true;

    private bool _playerHasHorizontalSpeed;
    private bool _playerHasVerticalSpeed;

    private bool _isVertical;

    private int _xScale = 1;
    private int _yScale = 1;
    
    // Start is called before the first frame update
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_PolygonCollider = GetComponent<PolygonCollider2D>();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_isAlive)
        {
            StopParticleSystem();
            
            return;
        }

        Walk();
        FlipSprite();

        Keyboard keyboard = InputSystem.GetDevice<Keyboard>();

        if (_isVertical)
        {
            m_Animator.SetBool("isMining", false);   
            m_Animator.SetBool("isVerticalMining", keyboard.shiftKey.isPressed);   
            m_Animator.SetBool("isVertical", true);
            
            // Collider güncelle
            m_BoxCollider.enabled = false;
            m_PolygonCollider.enabled = true;
        }
        else
        {
            m_Animator.SetBool("isVerticalMining", false);
            m_Animator.SetBool("isMining", keyboard.shiftKey.isPressed);
            m_Animator.SetBool("isVertical", false);   
            
            // Collider güncelle
            m_BoxCollider.enabled = true;
            m_PolygonCollider.enabled = false;
        }
    }

    private void Walk()
    {
        Vector2 playerVelocity = new Vector2(_moveInput.x * speed, _moveInput.y * speed);
        m_Rigidbody.velocity = playerVelocity;
        
        _playerHasHorizontalSpeed = Mathf.Abs(m_Rigidbody.velocity.x) > Mathf.Epsilon;
        _playerHasVerticalSpeed = Mathf.Abs(m_Rigidbody.velocity.y) > Mathf.Epsilon;
        
        if (Mathf.Abs(m_Rigidbody.velocity.x) > Mathf.Abs(m_Rigidbody.velocity.y))
        {
            _isVertical = false;
        } 
        else if (Mathf.Abs(m_Rigidbody.velocity.x) == Mathf.Abs(m_Rigidbody.velocity.y))
        {
            // Don't anything
        }
        else
        {
            _isVertical = true;
        }

        if (_isVertical)
        {
            m_Animator.SetBool("isWalking", false);
            m_Animator.SetBool("isVerticalWalking", _playerHasVerticalSpeed);
        }
        else
        {
            m_Animator.SetBool("isVerticalWalking", false);
            m_Animator.SetBool("isWalking", _playerHasHorizontalSpeed);
        }
    }
    
    private void OnMove(InputValue value)
    {
        if (!_isAlive) { return; }
        
        _moveInput = value.Get<Vector2>();
    }

    private void OnMining(InputValue value)
    {
        // Debug.Log("Space");
        //
        // m_Animator.SetBool("isMining", true);
    }
    
    private void OnFire(InputValue value) 
    {
        if (!_isAlive) { return; }

        if (_isVertical)
        {
            Debug.Log("Vertical");
            GameObject bulletObject = Instantiate(bullet, gun.position + new Vector3(.5f, .5f), transform.rotation);   
        }
        else
        {
            Debug.Log("Horizontal");
            GameObject bulletObject = Instantiate(bullet, gun.position, transform.rotation);
        }
    } 

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(m_Rigidbody.velocity.x) > Mathf.Epsilon;

        if (_isVertical)
        {
            if (m_Rigidbody.velocity.y != 0)
            {
                transform.localScale = new Vector2(1f, Mathf.Sign(m_Rigidbody.velocity.y));   
            }
        }
        else
        {
            if (m_Rigidbody.velocity.x != 0)
            {
                transform.localScale = new Vector2(Mathf.Sign(-m_Rigidbody.velocity.x), 1f);      
            }
        }
    }

    public void SetIsAlive(bool value)
    {
        _isAlive = value;
    }

    public bool GetIsVertıcal()
    {
        return _isVertical;
    }

    public void PlayParticleSystem()
    {
        Debug.Log("PlayParticleSystem");
        particle.Play();
        
        Invoke("StopParticleSystem", 5f);
    }
    
    public void StopParticleSystem()
    {
        particle.Stop();
    }
}
