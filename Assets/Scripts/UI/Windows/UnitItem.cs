using MyTonaTest.UI.Elements;
using UnityEngine;

namespace MyTonaTest.UI.Windows
{
  public class UnitItem : MonoBehaviour
  {
    [SerializeField] private Bar bar;
    
    public void UpdateBar(float current, float max)
    {
      bar.SetValue(current, max);
    }
  }
}