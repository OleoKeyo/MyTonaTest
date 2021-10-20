using MyTonaTest.Common;
using MyTonaTest.Selection;
using UnityEngine;

namespace MyTonaTest.Enemy
{
  public class EnemyZone : MonoBehaviour
  {
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private TriggerObserver aggroTrigger;
    [SerializeField] private float radius;
    [SerializeField] private PlayerSquad playerSquad;

    public void Awake()
    {
      aggroTrigger.ChangeRadiusZone(radius);
      GameObject enemyGo = Instantiate(enemyPrefab, transform.position,  Quaternion.identity, transform);
      enemyGo.GetComponent<AggroList>().Construct(aggroTrigger, radius);
      enemyGo.GetComponent<EnemyAI>().Construct();
      enemyGo.GetComponentInChildren<EnemyClick>().Construct(playerSquad);
    }
  }
}