using UnityEngine;

namespace MyTonaTest.CameraBehaviour
{
	public class CameraTouch : MonoBehaviour
	{
		[SerializeField] private float moveSpeed = 50f;
		[SerializeField] private CameraControl cameraControl;
		[SerializeField] private float sensitive = 0.1f;

		private bool _isTouchEnable;
		private bool _isDragMode;

		private void Awake() =>
			_isTouchEnable = Application.platform == RuntimePlatform.Android 
			                 || Application.platform == RuntimePlatform.IPhonePlayer;
		
		private void Update()
		{
			if(_isTouchEnable)
				TouchInput();
		}

		public void EnableDragMode() => 
			_isDragMode = true;

		public void DisableDragMode() =>
			_isDragMode = false;

		private void TouchInput()
		{
			if (_isDragMode || IsPointerOverUI())
				return;

			if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				Vector3 touchDeltaPosition = new Vector3(-Input.GetTouch(0).deltaPosition.x, 0, -Input.GetTouch(0).deltaPosition.y);
				Vector3 delta = touchDeltaPosition * moveSpeed * Time.deltaTime;

				if(delta.magnitude > sensitive)
					cameraControl.AddToPosition(delta);
			}

			if (Input.touchCount == 2)
			{
				Touch firstTouch = Input.GetTouch(0);
				Touch secondTouch = Input.GetTouch(1);

				Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
				Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

				float touchDeltaMagnitude = (firstTouch.position - secondTouch.position).magnitude;
				float prevTouchDeltaMagnitude = (firstTouchPrevPos - secondTouchPrevPos).magnitude;

				float delta = prevTouchDeltaMagnitude - touchDeltaMagnitude;
				cameraControl.AddForZoom(-delta * Time.deltaTime);
			}
		}

		private bool IsPointerOverUI()
		{
			foreach (Touch touch in Input.touches)
				return Globals.PointerOverUI(touch.position);
			
			return false;
		}
	}
}
