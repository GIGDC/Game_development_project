using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Collections;

// 활용하는 클래스 이름을 설정
[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    // Scene 뷰에서 GUI 표시하는 함수
    private void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.m_viewRadius);

        Vector3 vec = fow.DirFormAngle(fow.m_viewAngle);
        Handles.DrawLine(fow.transform.position, fow.transform.position + vec * fow.m_viewRadius);
    }
}
