using System;
using System.Collections;
using MyTonaTest.Common;
using MyTonaTest.Enemy.States;
using UnityEngine;

namespace MyTonaTest.Enemy
{
  public class EnemyDeath : MonoBehaviour
  {
    [SerializeField] private Health health;
    [SerializeField] private float destroyTime = 2f;
    
    private EnemyStateMachine _enemyStateMachine;
    
    public event Action OnDeath;
    
    public void Construct(EnemyStateMachine stateMachine)
    {
      _enemyStateMachine = stateMachine;
      health.Current.OnChanged += HealthChanged;
    }
    
    private void OnDestroy() => 
      health.Current.OnChanged -= HealthChanged;
    
    private void HealthChanged()
    {
      if (health.Current.Value <= 0)
        Die();
    }
    private void Die()
    {
      health.Current.OnChanged -= HealthChanged;
      OnDeath?.Invoke();
      _enemyStateMachine.Enter<EnemyDeathState>();
    }

    public void StartDestroyTimer() =>
      StartCoroutine(DestroyTimer());

    private IEnumerator DestroyTimer()
    {
      yield return new WaitForSeconds(destroyTime);
      Destroy(gameObject);
    }
  }
}