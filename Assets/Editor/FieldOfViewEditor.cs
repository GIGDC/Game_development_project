using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Collections;

// Ȱ���ϴ� Ŭ���� �̸��� ����
[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    // Scene �信�� GUI ǥ���ϴ� �Լ�
    private void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.m_viewRadius);

        Vector3 vec = fow.DirFormAngle(fow.m_viewAngle);
        Handles.DrawLine(fow.transform.position, fow.transform.position + vec * fow.m_viewRadius);
    }
}
