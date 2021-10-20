using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyTonaTest.CameraBehaviour
{
  public static class Globals
  {
    private static readonly List<RaycastResult> Raycasts = new List<RaycastResult>();
    private static readonly PointerEventData PointerEventData = new PointerEventData(EventSystem.current);

    public static bool PointerOverUI(Vector2 position)
    {
      PointerEventData.position = position;
      EventSystem.current.RaycastAll(PointerEventData, Raycasts);
      if (Raycasts.Count > 0)
      {
        if (Raycasts[0].gameObject.GetComponent<RectTransform>() != null)
        {
          return true;
        }
      }
        
      return false;
    }
  }
}