using MyTonaTest.Selection;
using UnityEngine;

namespace MyTonaTest.Unit
{
  public class UnitVisibleTrigger : MonoBehaviour
  {
    [SerializeField] private UnitAI unitAI;
    [SerializeField] private UnitDeath unitDeath;
    private PlayerSquad _playerSquad;

    public void Construct(PlayerSquad playerSquad)
    {
      _playerSquad = playerSquad;
      unitDeath.OnDeath += RemoveFromVisibleList;
    }

    private void OnDestroy() =>
      unitDeath.OnDeath -= RemoveFromVisibleList;

    private void OnBecameVisible() =>
      _playerSquad.AddVisibleUnit(unitAI);

    private void OnBecameInvisible() =>
      RemoveFromVisibleList();
    
    private void RemoveFromVisibleList() =>
      _playerSquad.RemoveVisibleUnit(unitAI);
  }
}