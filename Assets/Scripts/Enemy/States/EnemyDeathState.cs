using MyTonaTest.Common;

namespace MyTonaTest.Enemy.States
{
  public class EnemyDeathState : IState
  {
    private readonly AgentMoveToTarget _agentMove;
    private readonly CapsuleAnimator _animator;
    private readonly EnemyDeath _enemyDeath;
    
    public EnemyDeathState(AgentMoveToTarget agentMove, CapsuleAnimator animator, EnemyDeath enemyDeath)
    {
      _agentMove = agentMove;
      _animator = animator;
      _enemyDeath = enemyDeath;
    }
    
    public void Enter()
    {
      _agentMove.enabled = false;
      _animator.PlayDeath();
      _enemyDeath.StartDestroyTimer();
    }

    public void Exit()
    {
    }
  }
}