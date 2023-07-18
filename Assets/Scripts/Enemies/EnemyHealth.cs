using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyHealth : MonoBehaviour
{
    public static Action<EnemyManager> OnEnemyKilled;
    public static Action<EnemyManager> OnEnemyHit;

    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Transform barPosition;

    [SerializeField] private float initialHealth = 10f;
    [SerializeField] private float maxHealth = 10f;

    public float CurrentHealth { get; set; }

    private Image _healthBar;
    private EnemyManager _enemy;
    private bool _isDead;
    
    void Start()
    {
        CreateHealthBar();
        CurrentHealth = initialHealth;
        _enemy = GetComponent<EnemyManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DealDamage(5f);
        }

        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount,
            CurrentHealth / maxHealth, Time.deltaTime * 10f);
    }


    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(transform);

        EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
        _healthBar = container.FillAmountImage;
    }

    public void DealDamage(float damageReceived)
    {
        if (this._isDead)
        {
            return;
        }
        CurrentHealth -= damageReceived;
        if (CurrentHealth <= 0)
        {
            this._isDead = true;
            CurrentHealth = 0;
            Die();
            
        }
        else
        {
            OnEnemyHit?.Invoke(_enemy);
        }
    }

    private void Die()
    {
        OnEnemyKilled?.Invoke(_enemy);
    }

    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
        _healthBar.fillAmount = 1f;
        this._isDead = false;
    }

}
