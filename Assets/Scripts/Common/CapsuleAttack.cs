using System.Linq;
using UnityEngine;

namespace MyTonaTest.Common
{
  public class CapsuleAttack : MonoBehaviour
  {
    [SerializeField] private CapsuleAnimator animator;
    [SerializeField] private string layer;
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private float damage = 0.5f;
    [SerializeField] private float cleavage = 1f;
    [SerializeField] private float effectiveDistance = 0.5f;
    [SerializeField] private Transform parent;

    private bool _attackIsActive;
    private float _attackCooldown;
    private bool _isAttack;
    private Transform _targetTransform;
    private Collider[] _hits = new Collider[1];
    private int _layerMask;

    private void Awake() =>
      _layerMask = 1 << LayerMask.NameToLayer(layer);
    
    private void Update()
    {
      UpdateCooldown();
      if(_attackIsActive)
        parent.LookAt(_targetTransform);
      
      if (CanAttack())
        StartAttack();
    }

    private void StartAttack()
    {
      animator.PlayAttack();
      _isAttack = true;
    }
    
    private void UpdateCooldown()
    {
      if (!CooldownIsUp())
        _attackCooldown -= Time.deltaTime;
    }

    public void EnableAttack(Transform target)
    {
      _targetTransform = target;
      _attackIsActive = true;
    }

    public void DisableAttack()
    {
      _attackIsActive = false;
      animator.StopAttack();
    }
    
    private void OnAttackEnded()
    {
      _attackCooldown = attackCooldown;
      if (Hit(out Collider hit))
        hit.transform.GetComponent<Health>().TakeDamage(damage);

      _isAttack = false;
    }
    
    private bool CanAttack() =>
      _attackIsActive && CooldownIsUp() && !_isAttack;

    private bool CooldownIsUp() =>
      _attackCooldown <= 0;
    
    private bool Hit(out Collider hit)
    {
      int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), cleavage, _hits, _layerMask);
      
      hit = _hits.FirstOrDefault();
      
      return hitsCount > 0;
    }

    private Vector3 StartPoint() =>
      new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward*effectiveDistance;
  }
}