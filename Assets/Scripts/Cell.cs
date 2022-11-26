using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Cell : MonoBehaviour
{
    /*
     * объявляем переменные, значение которых
     * будут показывать расположение плитки в массиве
     */

    public int X { get; private set; }

    public int Y { get; private set; }

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI pointsText;
    /*
     * в переменной value мы будем хранить номинал плитки в степени двойки,
     * т.к. при объединении двух одинаковых плиток значение финальной плитки удваивается
     */
    // int value;

    public int Value { get; private set; }
    public int Points => isEmpty ? 0 : (int)Mathf.Pow(2, Value); //делаем исключение, если value = 0, т.к. 2^0 = 1

    public bool isEmpty => Value == 0; //пустая ли плитка
    
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

    private void UpdateCell() //метод, в котором отобразим номинал ячейки, а также изменим цвет в зависимости от значения
    {
        pointsText.text = isEmpty ? string.Empty : Points.ToString(); //если плитка пустая, то текст равен пустой строке, иначе отображаем кол-во очков
        pointsText.color = Value <= 2 ? ColorManager.Instance.PointsDarkColor : ColorManager.Instance.PointsLightColor; //ТУТ МОЖНО УСТАНОВИТЬ value < 2, ЧТОБЫ КЛЕТКА С НОМИНАЛОМ 2 НЕ БЫЛА ТЁМНОЙ
        image.color = ColorManager.Instance.CellColors[Value];
    }
}
