using MyTonaTest.Common;
using MyTonaTest.Unit;
using UnityEngine;

namespace MyTonaTest.Enemy.States
{
  public class AggroState : IState
  {
    private readonly EnemyStateMachine _stateMachine;
    private readonly AggroList _aggroList;
    private EnemyTarget _currentTarget;
    
    public AggroState(EnemyStateMachine stateMachine, AggroList aggroList)
    {
      _stateMachine = stateMachine;
      _aggroList = aggroList;
    }

    public void Enter()
    {
      _currentTarget = _aggroList.GetCurrentTarget();
      if (_currentTarget == null)
        _stateMachine.Enter<PatrolState>();
      else
        _stateMachine.Enter<AttackUnitState, Transform>(_currentTarget.transform);
    }

    public void Exit()
    {
    }
  }
}