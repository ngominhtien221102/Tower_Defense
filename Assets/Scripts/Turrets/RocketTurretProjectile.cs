using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurretProjectile : TurretProjectile
{
    protected override void Update()
    {
        if (Time.time > _nextAttackTime)
        {
            if (_turret.CurrentEnemyTarget != null && _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0)
            {
                FireProjectile(_turret.CurrentEnemyTarget);
            }

            _nextAttackTime = Time.time + delayBtwAttacks;
        }
    }

    protected override void LoadProjectile()
    {

    }

    private void FireProjectile(EnemyManager enemy)
    {

        GameObject instance = _pooler.GetInstanceFromPool();
        instance.transform.position = projectileSpawnPosition.position;

        Projectile projectile = instance.GetComponent<Projectile>();

        _currentProjecttileLoaded = projectile;
        _currentProjecttileLoaded.TurretOwner = this;
        _currentProjecttileLoaded.ResetProjectile();
        _currentProjecttileLoaded.SetEnemy(enemy);
        instance.SetActive(true);
    }
}
