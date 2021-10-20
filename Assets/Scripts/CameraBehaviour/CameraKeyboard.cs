using MyTonaTest.Common;
using UnityEngine;

namespace MyTonaTest.CameraBehaviour
{
  public class CameraKeyboard : MonoBehaviour
  {
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private string horizontalInputAxis = "Horizontal";
    [SerializeField] private string verticalInputAxis = "Vertical";
    [SerializeField] private CameraControl cameraControl;
    
    private void Update() =>
      Move();
    
    private void Move()
    {
      Vector3 movementVector = new Vector3(Input.GetAxis(horizontalInputAxis), 0, Input.GetAxis(verticalInputAxis));
      if (movementVector.sqrMagnitude > Constants.Epsilon)
      {
        Vector3 delta = movementVector * moveSpeed * Time.deltaTime;
        cameraControl.AddToPosition(delta);
      }
    }
  }
}