using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    /*
     * �����, �������� � ���� ������ ���� ��������� ������ ������.
     * ��� ����� ��� ��������� ����� ������ � ����������� �� � ��������
     */

    // ��������� ������� Singleton

    public static ColorManager Instance;

    public Color[] CellColors; // 0 ������� ������� - ��� ���� ������ ������

    [Space(5)]
    public Color PointsDarkColor;
    public Color PointsLightColor;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

}
