using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Action<EnemyManager, float> OnEnemyHit;
    [SerializeField] protected float moveSpeed = 40f;
    [SerializeField] protected float damage = 2f;
    [SerializeField] private float minDistanceToDame = 0.1f;

    public TurretProjectile TurretOwner { get; set; }

    protected EnemyManager _enemyTarget;

    protected virtual void Update()
    {
        if(_enemyTarget != null)
        {
            MoveProjectile();
            RotateProjectile();
        }
    }

    protected virtual void MoveProjectile()
    {
        transform.position = Vector2.MoveTowards(transform.position, _enemyTarget.transform.position, moveSpeed * Time.deltaTime);

        float distanceToTarget = (_enemyTarget.transform.position - transform.position).magnitude;
        if(distanceToTarget < minDistanceToDame)
        {
            _enemyTarget.EnemyHealth.DealDamage(damage);
            TurretOwner.ResetTurretProjectile();
            ObjectPooler.ReturnToPool(gameObject);

        }
    }

    public void SetEnemy(EnemyManager enemy)
    {
        _enemyTarget = enemy;
    }

    private void RotateProjectile()
    {
        Vector3 enemyPos = _enemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, enemyPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }

    public void ResetProjectile()
    {
        _enemyTarget = null;
        transform.localRotation = Quaternion.identity;
    }
}
