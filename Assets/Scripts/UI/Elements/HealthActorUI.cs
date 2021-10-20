using MyTonaTest.Common;
using UnityEngine;

namespace MyTonaTest.UI.Elements
{
  public class HealthActorUI : MonoBehaviour
  {
    [SerializeField] private Bar bar;

    private Health _health;

    private void Awake()
    {
      _health = GetComponent<Health>();
      _health.Current.OnChanged += UpdateHpBar;
    }
    
    private void UpdateHpBar() => 
      bar.SetValue(_health.Current.Value, _health.Max);
  }
}