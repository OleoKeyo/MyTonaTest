using System;
using UnityEngine;
using UnityEngine.AI;

namespace MyTonaTest.Common
{
  public class AgentMoveToTarget : MonoBehaviour
  {
    [SerializeField] private NavMeshAgent agent;
    
    private Transform _targetTransform;
    private Vector3 _currentDestination;
    
    public event Action DestinationReached;
    private bool _destinationIsReached;
    
    private void Update() =>
      FollowTarget();

    public void SetTarget(Transform targetTransform)
    {
      _targetTransform = targetTransform;
      SetDestination(_targetTransform.position);
    }

    public void SetDestination(Vector3 destination)
    {
      if (NeedToMove(destination))
      {
        _currentDestination = destination;
        _destinationIsReached = false;
        agent.SetDestination(_currentDestination);
        agent.isStopped = false;
      }
    }

    public bool DestinationIsReached() =>
      _destinationIsReached;

    public void ClearTarget() =>
      _targetTransform = null;

    private void FollowTarget()
    {
      CheckDestination();
      if (_targetTransform != null)
      {
        SetDestination(_targetTransform.position);
      }
    }

    private bool NeedToMove(Vector3 destination) =>
      Vector3.Distance(destination, transform.position) > agent.stoppingDistance;

    private void CheckDestination()
    {
      if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
      {
        _currentDestination = transform.position;
        _destinationIsReached = true;
        agent.isStopped = true;
        DestinationReached?.Invoke();
      }
      else
      {
        agent.isStopped = false;
        _destinationIsReached = false;
      }
    }
  }
}
