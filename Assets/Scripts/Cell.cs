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

    public int X { get; private set; }

    public int Y { get; private set; }

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI pointsText;
    /*
     * � ���������� value �� ����� ������� ������� ������ � ������� ������,
     * �.�. ��� ����������� ���� ���������� ������ �������� ��������� ������ �����������
     */
    // int value;

    public int Value { get; private set; }
    public int Points => isEmpty ? 0 : (int)Mathf.Pow(2, Value); //������ ����������, ���� value = 0, �.�. 2^0 = 1

    public bool isEmpty => Value == 0; //������ �� ������
    
    public bool HasMerged { get; private set; }

    public const int MaxValue = 11; //2^11 = 2048

    public void SetValue(int x, int y, int value)
    {
        X = x;
        Y = y;
        Value = value;

        UpdateCell();
    }

    public void IncreaseValue()
    {
        Value++;
        HasMerged = true;

        GameController.Instance.AddPoints(Points);

        UpdateCell();
    }

    public void ResetFlags()
    {
        HasMerged = false;
    }

    public void MergeWithCell(Cell otherCell)
    {
        otherCell.IncreaseValue();

        SetValue(X, Y, 0);

        UpdateCell();
    }

    public void MoveToCell(Cell target)
    {
        target.SetValue(target.X, target.Y, Value);
        SetValue(X, Y, 0);

        UpdateCell();
    }

    private void UpdateCell() //�����, � ������� ��������� ������� ������, � ����� ������� ���� � ����������� �� ��������
    {
        pointsText.text = isEmpty ? string.Empty : Points.ToString(); //���� ������ ������, �� ����� ����� ������ ������, ����� ���������� ���-�� �����
        pointsText.color = Value <= 2 ? ColorManager.Instance.PointsDarkColor : ColorManager.Instance.PointsLightColor; //��� ����� ���������� value < 2, ����� ������ � ��������� 2 �� ���� Ҩ����
        image.color = ColorManager.Instance.CellColors[Value];
    }
}
