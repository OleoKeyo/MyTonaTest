using System;
using UnityEngine;

namespace MyTonaTest.CameraBehaviour
{
  public class CameraMouse : MonoBehaviour
  {
    [SerializeField] private float zoomSpeed = 50f;
    [SerializeField] private string zoomInputAxis = "Mouse ScrollWheel"; 
    [SerializeField] private CameraControl cameraControl;
    
    private void Update() =>
      Zoom();

    private void Zoom()
    {
      float offset = Input.GetAxis(zoomInputAxis);
      if (Math.Abs(offset) > 0.01f)
        cameraControl.AddForZoom(offset * zoomSpeed * Time.deltaTime);
    }
  }
}