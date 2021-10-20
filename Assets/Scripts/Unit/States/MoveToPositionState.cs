using MyTonaTest.Common;
using UnityEngine;

namespace MyTonaTest.Unit.States
{
  public class MoveToPositionState : IPayloadedState<Vector3>
  {
    private readonly AgentMoveToTarget _agentMove;

    public MoveToPositionState(AgentMoveToTarget agentMove) =>
      _agentMove = agentMove;
    
    public void Enter(Vector3 destination) =>
      _agentMove.SetDestination(destination);
    
    public void Exit()
    {
    }
  }
}