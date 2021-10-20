using System;
using MyTonaTest.Enemy;
using UnityEngine;

namespace MyTonaTest.Unit
{
  public class EnemyTarget : MonoBehaviour
  {
    [SerializeField] private UnitDeath unitDeath;

    public event Action<EnemyTarget> OnDeath;

    private void Awake() =>
      unitDeath.OnDeath += OnUnitDeath;

    private void OnDestroy() =>
      unitDeath.OnDeath -= OnUnitDeath;

    private void OnUnitDeath() =>
      OnDeath?.Invoke(this);
  }
}