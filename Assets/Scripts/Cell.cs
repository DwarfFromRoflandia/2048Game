using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    /*
     * ��������� ����������, �������� �������
     * ����� ���������� ������������ ������ � �������
     */

    [SerializeField] private int x;
    [SerializeField] private int y;

    /*
     * � ���������� value �� ����� ������� ������� ������ � ������� ������,
     * �.�. ��� ����������� ���� ���������� ������ �������� ��������� ������ �����������
     */
    private int value;
    private int points => value == 0 ? 0 : (int)Mathf.Pow(2, value); //������ ����������, ���� value = 0, �.�. 2^0 = 1

    private bool isEmpty; //������ �� ������
}
