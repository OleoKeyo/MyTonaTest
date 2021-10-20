using MyTonaTest.Selection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyTonaTest.Enemy
{
  public class EnemyClick : MonoBehaviour, IPointerClickHandler
  {
    [SerializeField] private UnitTarget target;
    private PlayerSquad _playerSquad;
    
    public void Construct(PlayerSquad playerSquad) =>
      _playerSquad = playerSquad;

    public void OnPointerClick(PointerEventData eventData) =>
      _playerSquad.ChoiceEnemy(target);
  }
}