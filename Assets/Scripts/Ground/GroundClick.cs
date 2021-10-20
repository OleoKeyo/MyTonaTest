using MyTonaTest.Selection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyTonaTest.Ground
{
  public class GroundClick : MonoBehaviour, IPointerClickHandler
  {
    [SerializeField] private PlayerSquad playerSquad;
    
    public void OnPointerClick(PointerEventData eventData)
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      
      if (Physics.Raycast(ray, out RaycastHit hit))
      {
        Vector3 position = new Vector3(hit.point.x, 0, hit.point.z);
        playerSquad.GroundClick(position);
      }
    }
  }
}