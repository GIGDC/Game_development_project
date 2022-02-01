using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float m_viewRadius;  // �ü� �Ѱ�
    [Range(0, 360)]
    public float m_viewAngle;   // �ü� ����

    public void Start()
    {
        m_viewRadius = 5;
    }
    public Vector3 DirFormAngle(float angleInDegree)
    {
        angleInDegree += transform.eulerAngles.y;  // �÷��̾��� ����
        return new Vector3(Mathf.Cos(angleInDegree * Mathf.Deg2Rad), 0, Mathf.Sin(angleInDegree * Mathf.Deg2Rad));
    }
}
