using MyTonaTest.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyTonaTest.UnitsFactory
{
  public class UnitsFactoryClick: MonoBehaviour, IPointerClickHandler
  {
    [SerializeField] private UnitFactory unitFactory;
    [SerializeField] private UIService uiService;

    private bool _isEnabled = true;

    public void OnPointerClick(PointerEventData eventData)
    {
      if (_isEnabled)
        uiService.OpenUnitFactoryWindow(unitFactory);
    }

    public void Enable() => 
      _isEnabled = true;

    public void Disable() =>
      _isEnabled = false;
  }
}