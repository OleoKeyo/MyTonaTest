using UnityEngine;

namespace MyTonaTest.Common
{
  public class CapsuleAnimator : MonoBehaviour, IAnimationStateReader
  {
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    
    private readonly int _idleStateHash = Animator.StringToHash("idle");
    private readonly int _attackStateHash = Animator.StringToHash("attack");
    private readonly int _deathStateHash = Animator.StringToHash("die");

    [SerializeField] private Animator animator;
    
    public AnimatorState State { get; private set; }

    private void Awake() =>
      animator = GetComponent<Animator>();

    public void PlayDeath() =>
      animator.SetTrigger(Die);
    
    public void Move() =>
      animator.SetBool(IsMoving, true);

    public void StopMoving() =>
      animator.SetBool(IsMoving, false);

    public void PlayAttack() =>
      animator.SetBool(IsAttack, true);

    public void StopAttack() =>
      animator.SetBool(IsAttack, false);

    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
    }
    
    private AnimatorState StateFor(int stateHash)
    {
      AnimatorState state;
      if (stateHash == _idleStateHash)
        state = AnimatorState.Idle;
      else if (stateHash == _attackStateHash)
        state = AnimatorState.Attack;
      else if (stateHash == _deathStateHash)
        state = AnimatorState.Died;
      else
        state = AnimatorState.Unknown;

      return state;
    }
  }
}