using MyTonaTest.Selection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyTonaTest.Unit
{
  public class UnitClick : MonoBehaviour, IPointerClickHandler
  {
    [SerializeField] private UnitAI unitAI;
    [SerializeField] private Image targetImage;
    [SerializeField] private float clickDelay = 0.5f;
    private PlayerSquad _playerSquad;

    private float _lastClickTime;
    
    public void Construct(PlayerSquad playerSquad) =>
      _playerSquad = playerSquad;
    
    public void ShowTargetImage() =>
      targetImage.enabled = true;

    public void HideTargetImage() =>
      targetImage.enabled = false;

    public void OnPointerClick(PointerEventData eventData)
    {
      if(Time.time - _lastClickTime < clickDelay)
        _playerSquad.SelectAllVisibleUnits();
      else
        _playerSquad.ChoiceUnit(unitAI);

      _lastClickTime = Time.time;
    }
  }
}