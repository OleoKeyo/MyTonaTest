using MyTonaTest.Common;
using UnityEngine;
using UnityEngine.AI;

namespace MyTonaTest.UnitsFactory
{
  public class ChangeColorTrigger : MonoBehaviour
  {
    [SerializeField] private TriggerObserver triggerObserver;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private NavMeshObstacle meshObstacle;
    [SerializeField] private Color obstacleColor = Color.red;
    private Color _defaultColor;

    public int TriggersCount { get; private set; }
    private bool _isActive;
    
    private void Start()
    {
      _defaultColor = meshRenderer.material.color;
      triggerObserver.TriggerExit += TriggerExit;
      triggerObserver.TriggerEnter += TriggerEnter;
    }

    private void OnDestroy()
    {
      triggerObserver.TriggerExit -= TriggerExit;
      triggerObserver.TriggerStay -= TriggerEnter;
    }

    private void Update()
    {
      if(_isActive)
        UpdateColor();
    }

    public void Enable()
    {
      _isActive = true;
      meshObstacle.enabled = false;
    }

    public void Disable()
    {
      _isActive = false;
      meshObstacle.enabled = true;
      SetMeshRendererColor(_defaultColor);
    }

    private void TriggerExit(Collider obj) => 
      TriggersCount--;

    private void TriggerEnter(Collider obj) => 
      TriggersCount++;

    private void UpdateColor() =>
      SetMeshRendererColor(TriggersCount > 0 ? obstacleColor : _defaultColor);

    private void SetMeshRendererColor(Color newColor)
    {
      if (meshRenderer.material.color != newColor)
        meshRenderer.material.color = newColor;
    }
  }
}