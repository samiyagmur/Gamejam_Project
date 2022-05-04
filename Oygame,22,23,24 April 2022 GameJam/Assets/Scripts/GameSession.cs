using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private float playerHealth = 100f;
    [SerializeField] private float playerPower1 = 100f;
    [SerializeField] private float playerPower2 = 0f;
    [SerializeField] private float playerPower3 = 0f;

    private bool _gameOver;
    private float _maxPlayerHealthHealth = 100f;

    private void Awake()
    {
        var numGameSessions = FindObjectsOfType<GameSession>().Length;
        
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // TODO: Power'ları ve health'i göster
    }

    public float GetPlayerHealth()
    {
        return playerHealth;
    } 
    
    public void SetPlayerHealth(float value)
    {
        playerHealth += value;

        if (playerHealth >= _maxPlayerHealthHealth)
        {
            playerHealth = _maxPlayerHealthHealth;
        }
        
        Debug.Log("playerHealth : " + playerHealth);
        
        if (playerHealth <= 0)
        {
            _gameOver = true;
            PlayerController player = FindObjectOfType<PlayerController>();
            player.SetIsAlive(false);
            player.PlayParticleSystem();
            
            // TODO: Game Over yazısıını göster
            
            // Oyuna x saniye sonra yeniden başlat
            Invoke("ResetGameSession", 5f);
        }
    } 
    
    public float GetPlayerPower1()
    {
        return playerPower1;
    } 
    
    public void SetPlayerPower1(float value)
    {
        playerPower1 += value;
    } 
    
    public float GetPlayerPower2()
    {
        return playerPower2;
    } 
    
    public void SetPlayerPower2(float value)
    {
        playerPower2 += value;
    } 
    
    public float GetPlayerPower3()
    {
        return playerPower3;
    } 
    
    public void SetPlayerPower3(float value)
    {
        playerPower3 += value;
    } 

    private void ResetGameSession()
    {
        // FindObjectOfType<ScenePersist>().ResetScenePersist();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject);
    }
}
