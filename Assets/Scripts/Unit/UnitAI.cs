using MyTonaTest.Common;
using MyTonaTest.Enemy;
using MyTonaTest.Unit.States;
using UnityEngine;

namespace MyTonaTest.Unit
{
  public class UnitAI : MonoBehaviour
  {
    [SerializeField] private AgentMoveToTarget agentMove;
    [SerializeField] private CapsuleAttack attack;
    [SerializeField] private CapsuleAnimator animator;
    [SerializeField] private UnitDeath unitDeath;
    [SerializeField] private UnitClick unitClick;
    
    private UnitStateMachine _stateMachine;

    public void Construct(Vector3 startPosition)
    {
      _stateMachine = new UnitStateMachine(agentMove, attack, animator, unitDeath);
      SetPosition(startPosition);
      unitDeath.Construct(_stateMachine);
    }

    public void SetPosition(Vector3 position) =>
      _stateMachine.Enter<MoveToPositionState, Vector3>(position);

    public void SetTarget(UnitTarget target) =>
      _stateMachine.Enter<AttackEnemyState, UnitTarget>(target);

    public void Select() =>
      unitClick.ShowTargetImage();

    public void Deselect() =>
      unitClick.HideTargetImage();
  }
}