using MyTonaTest.Common;
using UnityEngine;

namespace MyTonaTest.UnitsFactory
{
  public class GridSnap : MonoBehaviour
  {
    private const float YOffset = 0.5f;
    
    [SerializeField] private int snapValue = 1;
    private LayerMask _layerMask;
    private Transform _transform;
    
    public Vector3 _targetPosition;

    private void Awake()
    {
      _transform = GetComponent<Transform>();
      _layerMask = LayerMask.GetMask(Constants.GroundLayerMask);
      SetOnGround();
      _transform.position = GetSnapPosition(_transform.position);
      _targetPosition = _transform.position;
    }

    private void FixedUpdate() =>
      MoveToPosition();
    
    public void ChangePosition(Vector3 snappedPosition)
    {
      _targetPosition = snappedPosition;
    }
    
    private void MoveToPosition()
    {
      if (TargetPositionNotReached())
        _transform.position = _targetPosition;
    }

    private void SetOnGround()
    {
      if (Physics.Raycast(_transform.position, Vector3.down, out RaycastHit hitInfo, 10, _layerMask))
      {
        Vector3 oldPosition = _transform.position;
        Vector3 groundedPosition = new Vector3(oldPosition.x, hitInfo.point.y + YOffset, oldPosition.z);
        _transform.position = groundedPosition;
      }
    }
    
    public Vector3 GetSnapPosition(Vector3 position)
    {
      int x = Mathf.RoundToInt(position.x / snapValue);
      int z = Mathf.RoundToInt(position.z / snapValue);
      return new Vector3(x * snapValue, _transform.position.y, z * snapValue);
    }

    private bool TargetPositionNotReached() =>
      Vector3.Distance(_transform.position, _targetPosition) > Constants.Epsilon;
  }
}