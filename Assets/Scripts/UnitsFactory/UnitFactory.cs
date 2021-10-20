using MyTonaTest.Common;
using MyTonaTest.Selection;
using MyTonaTest.Unit;
using UnityEngine;

namespace MyTonaTest.UnitsFactory
{
  public class UnitFactory : MonoBehaviour
  {
    [SerializeField] private int maxUnitsCount;
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float unitProductionTimeInSeconds;
    [SerializeField] private PlayerSquad playerSquad;

    public readonly ObservableVariable<int> AvailableUnitsCount = new ObservableVariable<int>();
    public readonly ObservableVariable<float> TimeRemaining = new ObservableVariable<float>();
    public float UnitProductionTime => unitProductionTimeInSeconds;

    private int _unitsInProduction;
    private bool _productionIsActive;
    
    private void Awake()
    {
      AvailableUnitsCount.Value = maxUnitsCount;
      StartProduction();
    }
    
    private void Update()
    {
      if (_productionIsActive)
        Produce(Time.deltaTime);
    }

    private void OnDestroy() =>
      PauseProduction();
    
    public void AddUnit()
    {
      AvailableUnitsCount.Value--;
      
      if (_unitsInProduction == 0 && _productionIsActive)
        TimeRemaining.Value = unitProductionTimeInSeconds;

      _unitsInProduction++;
    }

    private void CreateUnit()
    {
      GameObject unitGo = Instantiate(unitPrefab, transform.position, Quaternion.identity, playerSquad.transform);
      unitGo.GetComponent<UnitAI>().Construct(spawnPoint.position);
      unitGo.GetComponentInChildren<UnitClick>().Construct(playerSquad);
      unitGo.GetComponentInChildren<UnitVisibleTrigger>().Construct(playerSquad);
      unitGo.GetComponent<UnitDeath>().OnDeath += IncreaseAvailableUnits;
    }

    private void IncreaseAvailableUnits() => 
      AvailableUnitsCount.Value++;
    
    private void StartProduction() =>
      _productionIsActive = true;

    private void PauseProduction() =>
      _productionIsActive = false;

    private void Produce(float deltaTime)
    {
      if (TimeRemaining.Value > 0 && _productionIsActive)
      {
        TimeRemaining.Value -= deltaTime;
        if (TimeRemaining.Value <= 0)
        {
          CreateUnit();
          _unitsInProduction--;
          
          if (_unitsInProduction > 0)
            TimeRemaining.Value = unitProductionTimeInSeconds;
        }
      }
    }
  }
}