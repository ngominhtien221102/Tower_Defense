using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] protected Transform projectileSpawnPosition;
    [SerializeField] protected float delayBtwAttacks = 2f;

    protected float _nextAttackTime;
    protected ObjectPooler _pooler;
    protected Projectile _currentProjecttileLoaded;
    protected Turret _turret;

    private void Start()
    {   
        _turret = GetComponent<Turret>();
        _pooler = GetComponent<ObjectPooler>();
        //LoadProjectile();
    }

    protected virtual void Update()
    {
        //if (IsTurretEmpty())
        //{
        //    LoadProjectile();
        //}

        if (Time.time > _nextAttackTime)
        {
            if (_turret.CurrentEnemyTarget != null && _currentProjecttileLoaded != null
            && _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f)
            {
                _currentProjecttileLoaded.transform.parent = null;
                _currentProjecttileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
            }

            _nextAttackTime = Time.time + delayBtwAttacks;
        }
    }

    protected virtual void LoadProjectile()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.transform.localPosition = projectileSpawnPosition.position;
        newInstance.transform.SetParent(projectileSpawnPosition);

        Projectile projectile = newInstance.GetComponent<Projectile>();

        _currentProjecttileLoaded = projectile;
        _currentProjecttileLoaded.TurretOwner = this;
        _currentProjecttileLoaded.ResetProjectile();
        newInstance.SetActive(true);
    }

    private bool IsTurretEmpty()
    {
        return _currentProjecttileLoaded == null;
    }
    public void ResetTurretProjectile()
    {
        _currentProjecttileLoaded = null;

    }
}
