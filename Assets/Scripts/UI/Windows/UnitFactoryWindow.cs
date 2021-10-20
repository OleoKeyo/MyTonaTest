using System.Collections.Generic;
using MyTonaTest.UnitsFactory;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace MyTonaTest.UI.Windows
{
  public class UnitFactoryWindow : MonoBehaviour
  {
    [SerializeField] private Button closeButton;
    [SerializeField] private AddUnitButton addUnitButton;
    [SerializeField] private GameObject unitItemPrefab;
    [SerializeField] private Transform content;
    private UnitFactory _unitFactory;

    private readonly Queue<UnitItem> _unitItems = new Queue<UnitItem>();

    public void Construct(UnitFactory factory)
    {
      _unitFactory = factory;
      _unitFactory.TimeRemaining.OnChanged += UpdateUnitItems;
      addUnitButton.Construct(factory);
      addUnitButton.Button.AddListener(AddUnitToFactory);
    }

    public void Awake() =>
      closeButton.onClick.AddListener(Hide);
    
    public void Show()
    {
      gameObject.SetActive(true);
      addUnitButton.UpdateView();
      addUnitButton.Button.AddListener(AddUnitToFactory);
    }

    private void UpdateUnitItems()
    {
      UnitItem first = _unitItems.Peek();
      first.UpdateBar(_unitFactory.TimeRemaining.Value, _unitFactory.UnitProductionTime);
      
      if (_unitFactory.TimeRemaining.Value <= 0)
      {
        _unitItems.Dequeue();
        Destroy(first.gameObject);
      }
    }

    private void Hide()
    {
      addUnitButton.Hide();
      addUnitButton.Button.RemoveAllListeners();
      gameObject.SetActive(false);
    }

    private void AddUnitToFactory()
    {
      if(_unitFactory.AvailableUnitsCount.Value > 0)
      {
        GameObject unitItemGo = Instantiate(unitItemPrefab, content);
        UnitItem unitItem = unitItemGo.GetComponent<UnitItem>();
        _unitItems.Enqueue(unitItem);
        _unitFactory.AddUnit();
      }
    }
  }
}