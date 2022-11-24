using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Cell : MonoBehaviour
{
    /*
     * ��������� ����������, �������� �������
     * ����� ���������� ������������ ������ � �������
     */

    [SerializeField] private int x;

    public int X { get => x;}

    [SerializeField] private int y;

    public int Y { get => y;}

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI pointsText;
    /*
     * � ���������� value �� ����� ������� ������� ������ � ������� ������,
     * �.�. ��� ����������� ���� ���������� ������ �������� ��������� ������ �����������
     */
    [SerializeField] private int value;
    private int points => value == 0 ? 0 : (int)Mathf.Pow(2, value); //������ ����������, ���� value = 0, �.�. 2^0 = 1

    public bool isEmpty => value == 0; //������ �� ������

    private const int MaxValue = 11; //2^11 = 2048

    public void SetValue(int x, int y, int value)
    {
        this.x = x;
        this.y = y;
        this.value = value;

        UpdateCell();
    }

    private void UpdateCell() //�����, � ������� ��������� ������� ������, � ����� ������� ���� � ����������� �� ��������
    {
        pointsText.text = isEmpty ? string.Empty : points.ToString(); //���� ������ ������, �� ����� ����� ������ ������, ����� ���������� ���-�� �����
        pointsText.color = value <= 2 ? ColorManager.Instance.PointsDarkColor : ColorManager.Instance.PointsLightColor; //��� ����� ���������� value < 2, ����� ������ � ��������� 2 �� ���� Ҩ����
        image.color = ColorManager.Instance.CellColors[value];
    }
}
