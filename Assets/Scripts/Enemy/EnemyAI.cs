using MyTonaTest.Common;
using MyTonaTest.Enemy.States;
using UnityEngine;

namespace MyTonaTest.Enemy
{
  public class EnemyAI : MonoBehaviour
  {
    [SerializeField] private AggroList aggroList;
    [SerializeField] private AgentMoveToTarget agentMove;
    [SerializeField] private CapsuleAttack attack;
    [SerializeField] private EnemyDeath enemyDeath;
    [SerializeField] private CapsuleAnimator animator;

    private EnemyStateMachine _stateMachine;

    public void Construct()
    {
      _stateMachine = new EnemyStateMachine(aggroList, agentMove, attack, animator, enemyDeath);
      _stateMachine.Enter<PatrolState>();
      enemyDeath.Construct(_stateMachine);
    }
  }
}