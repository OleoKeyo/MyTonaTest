using MyTonaTest.Common;
using MyTonaTest.Enemy;

namespace MyTonaTest.Unit.States
{
  public class UnitStateMachine : StateMachine
  {
    public UnitStateMachine(AgentMoveToTarget agentMove, CapsuleAttack attack, CapsuleAnimator animator, UnitDeath unitDeath)
    {
      AddState(new MoveToPositionState(agentMove));
      AddState(new AttackEnemyState(this, agentMove, attack));
      AddState(new UnitDeathState(agentMove, animator, unitDeath));
    }
  }
}