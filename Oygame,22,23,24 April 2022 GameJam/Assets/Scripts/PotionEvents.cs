using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEvents : MonoBehaviour
{
    public GameObject _Player;
    private GameSession _gameSession;

    public float potion = 10f;
    

    void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();


    }

    
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.CompareTag("Potion_1"))
        {
            if(collision.gameObject == _Player)
            {
                _gameSession.SetPlayerPower1(+potion);
                Destroy(gameObject);
                Debug.Log("Power 1: " + _gameSession.GetPlayerPower1());
            }

        }
        else if (gameObject.CompareTag("Potion_2"))
        {
            if (collision.gameObject == _Player)
            {
                _gameSession.SetPlayerPower2(+potion);
                Destroy(gameObject);
                Debug.Log("Power 2: " + _gameSession.GetPlayerPower2());
            }

        }
        else if (gameObject.CompareTag("Potion_3"))
        {
            if (collision.gameObject == _Player)
            {
                _gameSession.SetPlayerPower3(+potion);
                Destroy(gameObject);
                Debug.Log("Power 3: " + _gameSession.GetPlayerPower3());
            }

        }
    }
}
