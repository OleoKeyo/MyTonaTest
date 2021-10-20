using MyTonaTest.Common;
using UnityEngine;

namespace MyTonaTest.Enemy.States
{
  public class PatrolState : IState
  {
    private readonly EnemyStateMachine _stateMachine;
    private readonly AgentMoveToTarget _agentMove;
    private readonly AggroList _aggroList;
    private readonly Vector3[] _patrolPoints = new Vector3[4];
    
    private int _lastPointIndex;
    
    public PatrolState(EnemyStateMachine stateMachine, AgentMoveToTarget agentMove, AggroList aggroList)
    {
      _stateMachine = stateMachine;
      _agentMove = agentMove;
      _aggroList = aggroList;
      GeneratePatrolPoints(aggroList.transform.position, _aggroList.Radius);
    }

    public void Enter()
    {
      MoveToNextPoint();
      _agentMove.DestinationReached += MoveToNextPoint;
      _aggroList.OnChangeTarget += AttackUnit;
    }

    public void Exit()
    {
      _agentMove.DestinationReached -= MoveToNextPoint;
      _aggroList.OnChangeTarget -= AttackUnit;
    }

    private void AttackUnit()
    {
      _stateMachine.Enter<AggroState>();
    }

    private void MoveToNextPoint()
    {
      Vector3 destination = GetNextPoint();
      _agentMove.SetDestination(destination);
    }

    private Vector3 GetNextPoint()
    {
      if (_lastPointIndex >= _patrolPoints.Length)
        _lastPointIndex = Random.Range(0, _patrolPoints.Length);
      
      Vector3 nextPoint = _patrolPoints[_lastPointIndex];
      _lastPointIndex++;
      return nextPoint;
    }

    private void GeneratePatrolPoints(Vector3 startPosition, float radius)
    {
      _patrolPoints[0] = new Vector3(startPosition.x + radius, startPosition.y, startPosition.z);
      _patrolPoints[1] = new Vector3(startPosition.x, startPosition.y, startPosition.z + radius);
      _patrolPoints[2] = new Vector3(startPosition.x, startPosition.y, startPosition.z - radius);
      _patrolPoints[3] = new Vector3(startPosition.x - radius, startPosition.y, startPosition.z);
    }
  }
}