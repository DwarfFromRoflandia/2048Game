using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    /*
     * объявляем переменные, значение которых
     * будут показывать расположение плитки в массиве
     */

    [SerializeField] private int x;
    [SerializeField] private int y;

    /*
     * в переменной value мы будем хранить номинал плитки в степени двойки,
     * т.к. при объединении двух одинаковых плиток значение финальной плитки удваивается
     */
    private int value;
    private int points => value == 0 ? 0 : (int)Mathf.Pow(2, value); //делаем исключение, если value = 0, т.к. 2^0 = 1

    private bool isEmpty; //пустая ли плитка
}
