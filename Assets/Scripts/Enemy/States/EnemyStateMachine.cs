using MyTonaTest.Common;

namespace MyTonaTest.Enemy.States
{
  public class EnemyStateMachine : StateMachine
  {
    public EnemyStateMachine(
      AggroList aggroList, 
      AgentMoveToTarget agentMove, 
      CapsuleAttack attack, 
      CapsuleAnimator animator,
      EnemyDeath enemyDeath)
    {
      AddState(new AggroState(this, aggroList));
      AddState(new PatrolState(this, agentMove, aggroList));
      AddState(new AttackUnitState(this, agentMove, aggroList, attack));
      AddState(new EnemyDeathState(agentMove, animator, enemyDeath));
    }
  }
}