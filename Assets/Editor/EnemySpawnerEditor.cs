using MyTonaTest.Enemy;
using UnityEditor;
using UnityEngine;

namespace MyTonaTest.Editor
{
  [CustomEditor(typeof(EnemyZone))]
  public class EnemySpawnerEditor : UnityEditor.Editor
  {
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderGizmo(EnemyZone zone, GizmoType gizmo)
    {
      Gizmos.color = Color.red;
      Gizmos.DrawSphere(zone.transform.position, 0.5f);
    }
  }
}