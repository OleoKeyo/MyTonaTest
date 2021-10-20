using MyTonaTest.Common;
using UnityEngine;

namespace MyTonaTest.Enemy.States
{
  public class AttackUnitState : IPayloadedState<Transform>
  {
    private readonly EnemyStateMachine _stateMachine;
    private readonly AgentMoveToTarget _agentMove;
    private readonly AggroList _aggroList;
    private readonly CapsuleAttack _attack;
    
    private Transform _currentTarget;

    public AttackUnitState(EnemyStateMachine enemyStateMachine, AgentMoveToTarget agentMove, AggroList aggroList, CapsuleAttack attack)
    {
      _stateMachine = enemyStateMachine;
      _agentMove = agentMove;
      _aggroList = aggroList;
      _attack = attack;
    }

    public void Enter(Transform target)
    {
      _currentTarget = target;
      _agentMove.SetTarget(_currentTarget);
      _agentMove.DestinationReached += StartAttack;
      _aggroList.OnChangeTarget += FindAnotherTarget;
    }
    
    public void Exit()
    {
      _agentMove.DestinationReached -= StartAttack;
      _aggroList.OnChangeTarget -= FindAnotherTarget;
    }

    private void FindAnotherTarget()
    {
      StopAttack();
      _agentMove.ClearTarget();
      _stateMachine.Enter<AggroState>();
    }

    private void StartAttack()
    {
      _agentMove.DestinationReached -= StartAttack;
      _attack.EnableAttack(_currentTarget);
    }

    private void StopAttack() =>
      _attack.DisableAttack();
  }
}