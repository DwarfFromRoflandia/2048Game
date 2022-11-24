using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    /*
     * класс, хранящий в себе массив всех возможных цветов плиток.
     * Это нужно для изменения цвета плитки в зависимости от её значения
     */

    // реализуем паттерн Singleton

    public static ColorManager Instance;

    public Color[] CellColors; // 0 элемент массива - это цвет пустой клетки

    [Space(5)]
    public Color PointsDarkColor;
    public Color PointsLightColor;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

}
