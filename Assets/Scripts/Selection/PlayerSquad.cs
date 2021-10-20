using System.Collections.Generic;
using MyTonaTest.Enemy;
using MyTonaTest.Unit;
using UnityEngine;

namespace MyTonaTest.Selection
{
  public class PlayerSquad : MonoBehaviour
  {
    private readonly List<UnitAI> _selectedUnits = new List<UnitAI>();
    private readonly List<UnitAI> _visibleUnits = new List<UnitAI>();

    public void ChoiceUnit(UnitAI unit)
    {
      DeselectUnits();
      _selectedUnits.Add(unit);
      unit.Select();
    }

    public void GroundClick(Vector3 position)
    {
      if (_selectedUnits.Count == 0)
        return;
      
      foreach (UnitAI unit in _selectedUnits)
        unit.SetPosition(position);
    }

    public void ChoiceEnemy(UnitTarget target)
    {
      if(_selectedUnits.Count == 0)
        return;
      
      foreach (UnitAI unit in _selectedUnits)
        unit.SetTarget(target);
    }

    public void SelectAllVisibleUnits()
    {
      DeselectUnits();
      
      foreach (UnitAI unit in _visibleUnits)
      {
        _selectedUnits.Add(unit);
        unit.Select();
      }
    }

    public void AddVisibleUnit(UnitAI unitAI)
    {
      if(!_visibleUnits.Contains(unitAI))
        _visibleUnits.Add(unitAI);
    }

    public void RemoveVisibleUnit(UnitAI unitAI)
    {
      _visibleUnits.Remove(unitAI);
      _selectedUnits.Remove(unitAI);
    }

    private void DeselectUnits()
    {
      foreach (UnitAI unit in _selectedUnits)
        unit.Deselect();
      
      _selectedUnits.Clear();
    }
  }
}