using UnityEngine;

namespace MyTonaTest.CameraBehaviour
{
  public class CameraControl : MonoBehaviour
    {
	    [SerializeField] private Vector3 minBoarder;
	    [SerializeField] private Vector3 maxBoarder;
	    [SerializeField] private Camera mainCamera;
	    [SerializeField] private CameraTouch cameraTouch;

	    private Transform _cameraTransform;
	    private Vector3 _moveVector;
	    private Quaternion _rotationAngleY;

	    private bool _movingIsStopped;

	    private void Awake()
	    {
		    _cameraTransform = transform;
		    _rotationAngleY = Quaternion.AngleAxis(_cameraTransform.rotation.eulerAngles.y, Vector3.up);
	    }

	    private void LateUpdate() =>
				MoveCamera();

	    public Camera GetCamera() =>
		    mainCamera;

	    public void AddToPosition(Vector3 delta) =>
	      _moveVector += _rotationAngleY * delta;

      public void AddForZoom(float delta)
      {
	      Vector3 zoomVector = _cameraTransform.forward * delta;
	      if (AllowZoom(_cameraTransform.position + zoomVector))
		      _moveVector += zoomVector;
      }
      
      public void EnableDragMode()
      {
	      _movingIsStopped = true;
	      cameraTouch.EnableDragMode();
      }
      
      public void DisableDragMode()
      {
	      _movingIsStopped = false;
	      cameraTouch.DisableDragMode();
      }

      private void MoveCamera()
      {
	      if(_movingIsStopped)
		      return;

	      Vector3 newPosition = _cameraTransform.position + _moveVector;
	      Vector3 clampedPosition = new Vector3(
		      Mathf.Clamp(newPosition.x, minBoarder.x, maxBoarder.x), 
		      Mathf.Clamp(newPosition.y, minBoarder.y, maxBoarder.y),
		      Mathf.Clamp(newPosition.z, minBoarder.z, maxBoarder.z)
	      );
	      _cameraTransform.position = clampedPosition;
	      _moveVector = Vector3.zero;
      }

      private bool AllowZoom(Vector3 position) =>
				position.y > minBoarder.y && position.y < maxBoarder.y;
    }
}