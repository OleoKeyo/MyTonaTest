using MyTonaTest.Common;
using MyTonaTest.Enemy;

namespace MyTonaTest.Unit.States
{
  public class UnitDeathState : IState
  {
    private readonly AgentMoveToTarget _agentMove;
    private readonly CapsuleAnimator _animator;
    private readonly UnitDeath _unitDeath;
    
    public UnitDeathState(AgentMoveToTarget agentMove, CapsuleAnimator animator, UnitDeath unitDeath)
    {
      _agentMove = agentMove;
      _animator = animator;
      _unitDeath = unitDeath;
    }
    
    public void Enter()
    {
      _agentMove.enabled = false;
      _animator.PlayDeath();
      _unitDeath.StartDestroyTimer();
    }

    public void Exit()
    {
    }
  }
}