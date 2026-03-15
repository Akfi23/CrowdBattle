using UnityEngine;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code.ECS.Components;
using Sirenix.OdinInspector;
using UnityEditor;

namespace AKFramework.Generated
{
    [DisallowMultipleComponent, AddComponentMenu("AKFramework/ECS/Components/Spawn Area"), HideMonoScript]
    public sealed class _SpawnArea : AKComponent<SpawnArea>
    {
        [Space(20)]
        [BoxGroup("Gizmo settings")]
        [SerializeField] private Color gizmoColor = new Color(0f, 1f, 0f, 0.5f);
        [BoxGroup("Gizmo settings")]
        [SerializeField] private float lineThickness=2f;
        
        private void OnDrawGizmos()
        {
            gizmoColor.a = 0.5f;
            DrawArea(gizmoColor,lineThickness/2);
        }

        private void OnDrawGizmosSelected()
        {
            gizmoColor.a = 1;
            DrawArea(gizmoColor,lineThickness);
        }

        private void DrawArea(Color color,float lineThickness)
        {
#if UNITY_EDITOR
            var originalColor = Handles.color;
            var originalMatrix = Handles.matrix;

            var yRotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
            Handles.matrix = Matrix4x4.TRS(transform.position, yRotation, Vector3.one);
            Handles.color = color;

            var half = Value.size * 0.5f;
            Vector3[] corners = new Vector3[8];
            corners[0] = new Vector3(-half.x, -half.y, -half.z);
            corners[1] = new Vector3(half.x, -half.y, -half.z);
            corners[2] = new Vector3(half.x, -half.y, half.z);
            corners[3] = new Vector3(-half.x, -half.y, half.z);
            corners[4] = new Vector3(-half.x, half.y, -half.z);
            corners[5] = new Vector3(half.x, half.y, -half.z);
            corners[6] = new Vector3(half.x, half.y, half.z);
            corners[7] = new Vector3(-half.x, half.y, half.z);

            DrawThickLine(corners[0], corners[1], color, lineThickness);
            DrawThickLine(corners[1], corners[2], color, lineThickness);
            DrawThickLine(corners[2], corners[3], color, lineThickness);
            DrawThickLine(corners[3], corners[0], color, lineThickness);

            DrawThickLine(corners[4], corners[5], color, lineThickness);
            DrawThickLine(corners[5], corners[6], color, lineThickness);
            DrawThickLine(corners[6], corners[7], color, lineThickness);
            DrawThickLine(corners[7], corners[4], color, lineThickness);

            DrawThickLine(corners[0], corners[4], color, lineThickness);
            DrawThickLine(corners[1], corners[5], color, lineThickness);
            DrawThickLine(corners[2], corners[6], color, lineThickness);
            DrawThickLine(corners[3], corners[7], color, lineThickness);

            Handles.matrix = originalMatrix;
            Handles.color = originalColor;
#endif
        }

#if UNITY_EDITOR
        private void DrawThickLine(Vector3 p1, Vector3 p2, Color color, float thickness)
        {
            var prevColor = Handles.color;
            Handles.color = color;

            Handles.DrawAAPolyLine(EditorGUIUtility.whiteTexture, thickness, p1, p2);

            Handles.color = prevColor;
        }
#endif
    }
}
