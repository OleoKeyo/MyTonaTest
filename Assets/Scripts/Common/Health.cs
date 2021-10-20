using UnityEngine;

namespace MyTonaTest.Common
{
  public class Health : MonoBehaviour
  {
    [SerializeField] private float maxHealth = 2;
    public readonly ObservableVariable<float> Current = new ObservableVariable<float>();
    public float Max { get; private set; }

    private void Awake()
    {
      Max = maxHealth;
      Current.Value = Max;
    }

    public void TakeDamage(float damage)
    {
      if(Current.Value <= 0)
        return;
      
      Current.Value -= damage;
    }
  }
}