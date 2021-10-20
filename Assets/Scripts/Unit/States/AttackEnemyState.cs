using MyTonaTest.Common;
using MyTonaTest.Enemy;
using UnityEngine;

namespace MyTonaTest.Unit.States
{
  public class AttackEnemyState : IPayloadedState<UnitTarget>
  {
    private readonly UnitStateMachine _stateMachine;
    private readonly AgentMoveToTarget _agentMove;
    private readonly CapsuleAttack _attack;
    
    private UnitTarget _currentTarget;

    public AttackEnemyState(UnitStateMachine stateMachine, AgentMoveToTarget agentMove, CapsuleAttack attack)
    {
      _stateMachine = stateMachine;
      _agentMove = agentMove;
      _attack = attack;
    }

    public void Enter(UnitTarget target)
    {
      _currentTarget = target;
      _agentMove.SetTarget(target.transform);
      _currentTarget.OnDeath += StopAttack;
      _agentMove.DestinationReached += StartAttack;
    }

    public void Exit()
    {
      _agentMove.DestinationReached -= StartAttack;
      _currentTarget.OnDeath -= StopAttack;
      _attack.DisableAttack();
      _agentMove.ClearTarget();
    }

    private void StartAttack()
    {
      _agentMove.ClearTarget();
      _agentMove.DestinationReached -= StartAttack;
      _attack.EnableAttack(_currentTarget.transform);
    }

    private void StopAttack(UnitTarget target)
    {
      target.OnDeath -= StopAttack;
      _stateMachine.Enter<MoveToPositionState, Vector3>(_agentMove.transform.position);
    }
  }
}