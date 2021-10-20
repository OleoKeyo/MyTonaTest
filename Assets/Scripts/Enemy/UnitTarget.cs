using System;
using UnityEngine;

namespace MyTonaTest.Enemy
{
  public class UnitTarget : MonoBehaviour
  {
    [SerializeField] private EnemyDeath enemyDeath;

    public event Action<UnitTarget> OnDeath;

    private void Awake() =>
      enemyDeath.OnDeath += OnUnitDeath;

    private void OnDestroy() =>
      enemyDeath.OnDeath -= OnUnitDeath;

    private void OnUnitDeath() =>
      OnDeath?.Invoke(this);
  }
}