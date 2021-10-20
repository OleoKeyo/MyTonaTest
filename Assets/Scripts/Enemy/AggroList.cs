using System;
using System.Collections.Generic;
using MyTonaTest.Common;
using MyTonaTest.Unit;
using UnityEngine;

namespace MyTonaTest.Enemy
{
  public class AggroList : MonoBehaviour
  {
    private const string PlayerTag = "Player";
    
    private TriggerObserver _aggroTrigger;
    private List<EnemyTarget> _units = new List<EnemyTarget>();
    private EnemyTarget _currentTarget;

    public float Radius { get; private set; }
    public event Action OnChangeTarget; 
    
    public void Construct(TriggerObserver aggroTrigger, float radius)
    {
      _aggroTrigger = aggroTrigger;
      _aggroTrigger.TriggerEnter += AggroTriggerEnter;
      _aggroTrigger.TriggerExit += AggroTriggerExit;
      Radius = radius;
    }

    private void OnDestroy()
    {
      _aggroTrigger.TriggerEnter -= AggroTriggerEnter;
      _aggroTrigger.TriggerExit -= AggroTriggerExit;
    }

    public EnemyTarget GetCurrentTarget() =>
      _currentTarget;

    private void AggroTriggerEnter(Collider obj)
    {
      if (obj.CompareTag(PlayerTag))
      {
        EnemyTarget unit = obj.gameObject.GetComponent<EnemyTarget>();
        AddPlayerUnit(unit);
      }
    }

    private void AggroTriggerExit(Collider obj)
    {
      if (obj.CompareTag(PlayerTag))
      {
        EnemyTarget unit = obj.gameObject.GetComponent<EnemyTarget>();
        RemoveUnit(unit);
      }
    }

    private void RemoveUnit(EnemyTarget unit)
    {
      unit.OnDeath -= RemoveUnit;
      _units.Remove(unit);
      
      if (_currentTarget == unit)
        FindNextTarget();
    }
    
    private void FindNextTarget()
    {
      if (_units.Count == 0)
        _currentTarget = null;
      else
        _currentTarget = _units[0];

      OnChangeTarget?.Invoke();
    }

    private void AddPlayerUnit(EnemyTarget unit)
    {
      if(_units.Contains(unit)) 
        return;
      
      _units.Add(unit);
      unit.OnDeath += RemoveUnit;
      
      if (_currentTarget == null)
      {
        _currentTarget = unit;
        OnChangeTarget?.Invoke();
      }
    }
  }
}