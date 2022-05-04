using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEvents : MonoBehaviour
{
    private BoxCollider2D m_BoxCollider;
    private Animator m_Animator;
    private Animator _playerAnimator;

    public float damage= 10f;

   


    public GameObject _Player;
    
    private bool _isTouchedPlayer;

    private GameSession _gameSession;


    // Start is called before the first frame update
    void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();

        m_BoxCollider = GetComponent<BoxCollider2D>();

        _playerAnimator = _Player.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        


        // DUVAR KIRABÝLME ÞARTLARI

        if (_isTouchedPlayer && (_playerAnimator.GetBool("isMining") || _playerAnimator.GetBool("isVerticalMining")))
        {
            if (gameObject.CompareTag("Wall_1"))
            {
                if (_gameSession.GetPlayerPower1() >= damage)
                {
                    _gameSession.SetPlayerPower1(-damage);
                    Destroy(gameObject);
                }

            }
            else if (gameObject.CompareTag("Wall_2"))
            {
                if (_gameSession.GetPlayerPower2() >= damage)
                {
                    _gameSession.SetPlayerPower2(-damage);
                    Destroy(gameObject);
                }

            }
            else if (gameObject.CompareTag("Wall_3"))
            {
                if (_gameSession.GetPlayerPower3() >= damage)
                {
                    _gameSession.SetPlayerPower3(-damage);
                    Destroy(gameObject);
                }

            }
            
            
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isTouchedPlayer = true;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isTouchedPlayer = false;
        }
    }



}
