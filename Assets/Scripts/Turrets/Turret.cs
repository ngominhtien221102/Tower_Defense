using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float attackRange = 3f;

    private bool _gameStarted;

    private void Start()
    {
        _gameStarted = true;
    }

    private void OnDrawGizmos()
    {   
        if (!_gameStarted)
        {   
            //??ng b? collider v?i ph?m vi t?n công
            GetComponent<CircleCollider2D>().radius = attackRange;
        }
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


    
}
