using System;
using UnityEngine;

namespace MyTonaTest.Common
{
  [RequireComponent(typeof(SphereCollider))]
  public class TriggerObserver : MonoBehaviour
  {
    public event Action<Collider> TriggerEnter;
    public event Action<Collider> TriggerExit; 
    public event Action<Collider> TriggerStay;
    
    private void OnTriggerEnter(Collider other) => 
      TriggerEnter?.Invoke(other);

    private void OnTriggerExit(Collider other) => 
      TriggerExit?.Invoke(other);

    private void OnTriggerStay(Collider other) =>
      TriggerStay?.Invoke(other);

    public void ChangeRadiusZone(float radius)
    {
      GetComponent<SphereCollider>().radius = radius;
    }
  }
}