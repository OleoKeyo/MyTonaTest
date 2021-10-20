using MyTonaTest.UI.Windows;
using MyTonaTest.UnitsFactory;
using UnityEngine;

namespace MyTonaTest.UI
{
  public class UIService : MonoBehaviour
  {
    [SerializeField] private Transform uiRoot;
    [SerializeField] private UnitFactoryWindow unitFactoryWindowPrefab;

    private UnitFactoryWindow _unitFactoryWindow;
    
    public void OpenUnitFactoryWindow(UnitFactory factory)
    {
      if (_unitFactoryWindow == null)
      {
        _unitFactoryWindow = Instantiate(unitFactoryWindowPrefab, uiRoot);
        _unitFactoryWindow.Construct(factory);
      }

      if (_unitFactoryWindow.isActiveAndEnabled)
        return;
      _unitFactoryWindow.Show();
    }
  }
}