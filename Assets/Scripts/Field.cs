using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [Header("Field Properties")]
    [SerializeField] private float CellSize;
    [SerializeField] private float Spacing; //размер отступа между плитками
    [SerializeField] private int FieldSize;
    [SerializeField] private int InitCellsCount;

    [Space(10)]
    [SerializeField] private Cell cellPref;

    /*
     * RectTransform это аналог Transform, только он используется для объектов, находящихся на Canvas'е. 
     * Через него мы будем устанавливать размер нашего поля.
     */
    [SerializeField] private RectTransform rectTransform;

    private Cell[,] field; // поле мы будем хранить в двухмерном массиве
    void Start()
    {
        GenerateField();
    }


    void Update()
    {
        
    }

    private void CreateField()
    {
        field = new Cell[FieldSize, FieldSize];

        float fieldWidth = FieldSize * (CellSize + Spacing) + Spacing; // второй отступ после скобок (+ Spacing) нужен для того, чтобы добавить отступ слева у самой перовй клетке
        rectTransform.sizeDelta = new Vector2(fieldWidth, fieldWidth); //изменяем размер объекта на Canvas'е

        //находим координаты начальных клеток
        float startX = -(fieldWidth / 2) + (CellSize / 2) + Spacing;
        float startY = (fieldWidth / 2) - (CellSize / 2) - Spacing;

        
        //заполняем поле
        for (int x = 0; x < FieldSize; x++)
        {
            for (int y = 0; y < FieldSize; y++)
            {
                var cell = Instantiate(cellPref, transform, false); //создаём клетку
                var position = new Vector2(startX + (x * (CellSize + Spacing)), startY - (y * (CellSize + Spacing)));
                cell.transform.localPosition = position;

                field[x, y] = cell;

                cell.SetValue(x, y, 0);
            }
        }
    }

    public void GenerateField() //метод, использующийся для очищения поля и подготовки его к новой игре
    {
        if (field == null)
        {
            CreateField();      
        }

        for (int x = 0; x < FieldSize; x++)
        {
            for (int y = 0; y < FieldSize; y++)
            {
                field[x, y].SetValue(x, y, 0);
            }
        }

        for (int i = 0; i < InitCellsCount; i++)
        {
            GenerateRandomCell();
        }
    }

    private void GenerateRandomCell()
    {
        var emptyCells = new List<Cell>();

        for (int x = 0; x < FieldSize; x++)
        {
            for (int y = 0; y < FieldSize; y++)
            {
                if (field[x, y].isEmpty)
                {
                    emptyCells.Add(field[x, y]);
                }
            }
        }

        if (emptyCells.Count == 0)
        {
            throw new System.Exception("There is no any empty cell!");
        }


        int value = Random.Range(0, 10) == 0 ? 2 : 1;

        var cell = emptyCells[Random.Range(0, emptyCells.Count)];

        cell.SetValue(cell.X, cell.Y, value);
    }
}
