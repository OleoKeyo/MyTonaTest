using System;
using System.Collections;
using MyTonaTest.Common;
using MyTonaTest.Unit.States;
using UnityEngine;

namespace MyTonaTest.Unit
{
  public class UnitDeath : MonoBehaviour
  {
    [SerializeField] private Health health;
    [SerializeField] private float destroyTime = 2f;
    
    private UnitStateMachine _stateMachine;
    
    public event Action OnDeath;
    
    public void Construct(UnitStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
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
      _stateMachine.Enter<UnitDeathState>();
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