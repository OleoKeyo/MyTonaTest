using MyTonaTest.CameraBehaviour;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyTonaTest.UnitsFactory
{
  public class DragBehaviour : MonoBehaviour, IDragHandler, IEndDragHandler
  {
    [SerializeField] private GridSnap gridSnap;
    [SerializeField] private ChangeColorTrigger trigger;
    [SerializeField] private CameraControl cameraControl;
    [SerializeField] private UnitsFactoryClick unitsFactoryClick;
    
    private Camera _mainCamera;
    private Vector3 _positionBeforeDrag;
    private bool _isOnDrag;

    private void Awake()
    {
      _positionBeforeDrag = gridSnap.transform.position;
      _mainCamera = cameraControl.GetCamera();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
      if (_isOnDrag == false)
        EnableDrag();
      
      Vector3 newPosition = GetSnappedPosition();
      gridSnap.ChangePosition(newPosition);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
      if (trigger.TriggersCount == 0)
        _positionBeforeDrag = gridSnap.transform.position;
      
      gridSnap.ChangePosition(_positionBeforeDrag);
      
      DisableDrag();
    }

    private void EnableDrag()
    {
      cameraControl.EnableDragMode();
      _isOnDrag = true;
      trigger.Enable();
      unitsFactoryClick.Disable();
    }

    private void DisableDrag()
    {
      cameraControl.DisableDragMode();
      _isOnDrag = false;
      trigger.Disable();
      unitsFactoryClick.Enable();
    }

    private Vector3 GetSnappedPosition()
    {
      Vector3 mousePosition = Input.mousePosition;
      mousePosition.z = _mainCamera.WorldToScreenPoint(transform.position).z;
      Vector3 worldPoint = _mainCamera.ScreenToWorldPoint(mousePosition);
      return gridSnap.GetSnapPosition(worldPoint);
    }
  }
}