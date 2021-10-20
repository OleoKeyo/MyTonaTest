using UnityEngine;
using UnityEngine.UI;

namespace MyTonaTest.UI.Elements
{
  public class Bar : MonoBehaviour
  {
    [SerializeField] private Image image;

    public void SetValue(float current, float max) =>
      image.fillAmount = current / max;
  }
}