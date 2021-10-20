using MyTonaTest.UnitsFactory;
using UnityEngine;
using UnityEngine.UI;

namespace MyTonaTest.UI.Windows
{
  public class AddUnitButton : MonoBehaviour
  {
    [SerializeField] private Text text;
    [SerializeField] private Button addUnitButton;
    [SerializeField] private Color availableTextColor = Color.white;
    [SerializeField] private Color notAvailableTextColor = Color.red;

    public Button.ButtonClickedEvent Button => addUnitButton.onClick;
    private UnitFactory _unitFactory;

    public void Construct(UnitFactory factory)
    {
      _unitFactory = factory;
      UpdateView();
    }

    public void UpdateView()
    {
      UpdateText();
      _unitFactory.AvailableUnitsCount.OnChanged += UpdateText;
    }

    public void Hide() =>
      _unitFactory.AvailableUnitsCount.OnChanged -= UpdateText;

    private void UpdateText()
    {
      text.text = $"{_unitFactory.AvailableUnitsCount}";
      text.color = _unitFactory.AvailableUnitsCount.Value > 0 ? availableTextColor : notAvailableTextColor;
    }
  }
}