using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int lives = 10;
    [SerializeField] public Canvas gameOverPanel;

    public int TotalLives { get; set; }
    

    private void Start()
    {
        TotalLives = lives;      
    }

    private void ReduceLives(EnemyManager enemy)
    {
        TotalLives--;
        if (TotalLives <= 0)
        {
            gameOverPanel.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void OnEnable()
    {
        EnemyManager.OnEndReached += ReduceLives;
    }
    private void OnDisable()
    {
        EnemyManager.OnEndReached -= ReduceLives;
    }
}
