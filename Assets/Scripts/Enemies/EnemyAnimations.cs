using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator _animator;
    private EnemyManager _enemy;
    private EnemyHealth _enemyHealth;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<EnemyManager>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    private void PlayHurtAnimation()
    {
        _animator.SetTrigger("Hurt");
    }

    private void PlayDieAnimation()
    {
        _animator.SetTrigger("Die");
    }

    private float GetCurrentAnimationLength()
    {
        float animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
        return animationLength;
    }

    private IEnumerator PlayHurt()
    {
        _enemy.StopMovement();
        PlayHurtAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLength()*0.3f + 0.3f);
        _enemy.ResumeMovement();
    }

    private IEnumerator PlayDead()
    {
        _enemy.StopMovement();
        PlayDieAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLength() + 0.3f);
        _enemy.ResumeMovement();
        _enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(_enemy.gameObject);
    }

    private void EnemyHit(EnemyManager enemy)
    {
        if (_enemy == enemy)
        {
            StartCoroutine(PlayHurt());
        }
    }

    private void EnemyDead(EnemyManager enemy)
    {
        if (_enemy == enemy)
        {
            StartCoroutine(PlayDead());
        }
    }


    private void OnEnable()
    {
        EnemyHealth.OnEnemyHit += EnemyHit;
        EnemyHealth.OnEnemyKilled += EnemyDead;
    }


    private void OnDisable()
    {
        EnemyHealth.OnEnemyHit -= EnemyHit;
        EnemyHealth.OnEnemyKilled -= EnemyDead;
    }
}
