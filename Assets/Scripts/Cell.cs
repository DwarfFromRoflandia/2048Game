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

    [SerializeField] private int x;

    public int X { get => x;}

    [SerializeField] private int y;

    public int Y { get => y;}

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI pointsText;
    /*
     * в переменной value мы будем хранить номинал плитки в степени двойки,
     * т.к. при объединении двух одинаковых плиток значение финальной плитки удваивается
     */
    [SerializeField] private int value;
    private int points => value == 0 ? 0 : (int)Mathf.Pow(2, value); //делаем исключение, если value = 0, т.к. 2^0 = 1

    public bool isEmpty => value == 0; //пустая ли плитка

    private const int MaxValue = 11; //2^11 = 2048

    public void SetValue(int x, int y, int value)
    {
        this.x = x;
        this.y = y;
        this.value = value;

        UpdateCell();
    }

    private void UpdateCell() //метод, в котором отобразим номинал ячейки, а также изменим цвет в зависимости от значения
    {
        pointsText.text = isEmpty ? string.Empty : points.ToString(); //если плитка пустая, то текст равен пустой строке, иначе отображаем кол-во очков
        pointsText.color = value <= 2 ? ColorManager.Instance.PointsDarkColor : ColorManager.Instance.PointsLightColor; //ТУТ МОЖНО УСТАНОВИТЬ value < 2, ЧТОБЫ КЛЕТКА С НОМИНАЛОМ 2 НЕ БЫЛА ТЁМНОЙ
        image.color = ColorManager.Instance.CellColors[value];
    }
}
