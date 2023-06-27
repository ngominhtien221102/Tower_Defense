using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WayPoint))]
public class WayPoinEditor : Editor
{
    WayPoint WayPoint => target as WayPoint;

    //goi moi khi Unity cap nhat scene view
    private void OnSceneGUI()
    {
        Handles.color = Color.red;

        for (int i = 0; i < WayPoint.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            //Create Handle (tao va chinh sua c�c handle trong scene view)
            Vector3 currentWaypointPoint = WayPoint.CurrentPosition + WayPoint.Points[i];
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint,
                Quaternion.identity, 0.7f, new Vector3(0.3f, 0.3f, 0.3f),
                Handles.SphereHandleCap);

            //Create Text cho moi waypoint
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 16;
            textStyle.normal.textColor = Color.yellow;
            Vector3 textAlliment = Vector3.down * 0.35f + Vector3.right * 0.35f;
            Handles.Label(WayPoint.CurrentPosition + WayPoint.Points[i] + textAlliment,
                $"{i + 1}", textStyle);
            EditorGUI.EndChangeCheck();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Move Handle");
                WayPoint.Points[i] = newWaypointPoint - WayPoint.CurrentPosition;
            }
        }
    }

}
