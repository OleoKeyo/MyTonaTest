using UnityEngine;

namespace MyTonaTest.Common
{
  public class AnimateAlongAgent : MonoBehaviour
  {
    [SerializeField] private AgentMoveToTarget agentMove;
    [SerializeField] private CapsuleAnimator animator;

    private void Update()
    {
      if (ShouldMove())
        animator.Move();
      else
        animator.StopMoving();
    }

    private bool ShouldMove() =>
      !agentMove.DestinationIsReached();
  }
}