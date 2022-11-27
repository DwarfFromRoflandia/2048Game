using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public static Field Instance;

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

    private bool anyCellMoved;

    private void Start()
    {
        
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A))
            OnInput(Vector2.left);
        if (Input.GetKeyDown(KeyCode.D))
            OnInput(Vector2.right);
        if (Input.GetKeyDown(KeyCode.W))
            OnInput(Vector2.up);
        if (Input.GetKeyDown(KeyCode.S))
            OnInput(Vector2.down);
#endif
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnInput(Vector2 direction)
    {
        if (!GameController.GameStarted)
        {
            return;
        }

        anyCellMoved = false;
        ResetCellsFlags();

        Move(direction);

        if (anyCellMoved)
        {
            GenerateRandomCell();
            CheckGameResult();
        }
    }

    private void Move(Vector2 direction)
    {
        int startXY = direction.x > 0 || direction.y < 0 ? FieldSize - 1 : 0;
        int dir = direction.x != 0 ? (int)direction.x : -(int)direction.y;

        for (int i = 0; i < FieldSize; i++)
        {
            for (int k = startXY; k >= 0 && k < FieldSize; k -= dir)
            {
                var cell = direction.x != 0 ? field[k, i] : field[i, k];

                if (cell.isEmpty)
                {
                    continue;
                }

                var cellToMerge = FindCellToMerge(cell, direction);

                if (cellToMerge != null)
                {
                    cell.MergeWithCell(cellToMerge);
                    anyCellMoved = true;

                    continue;
                }

                var emtyCell = FindEmptyCell(cell, direction);

                if (emtyCell != null)
                {
                    cell.MoveToCell(emtyCell);
                    anyCellMoved = true;  
                }
            }
        }
    }

    private Cell FindCellToMerge(Cell cell, Vector2 direction)
    {
        int startX = cell.X + (int)direction.x;
        int startY = cell.Y - (int)direction.y;

        for (int x = startX, y = startY; x >= 0 && x < FieldSize && y >= 0 && y < FieldSize; x += (int)direction.x, y -= (int)direction.y)
        {
            if (field[x, y].isEmpty)
            {
                continue;
            }

            if (field[x, y].Value == cell.Value && !field[x, y].HasMerged)
            {
                return field[x, y];
            }

            break;
        }

        return null;
    }

    private Cell FindEmptyCell(Cell cell, Vector2 direction)
    {
        Cell emtyCell = null;

        int startX = cell.X + (int)direction.x;
        int startY = cell.Y - (int)direction.y;

        for (int x = startX, y = startY; x >= 0 && x < FieldSize && y >= 0 && y < FieldSize; x += (int)direction.x, y -= (int)direction.y)
        {
            if (field[x, y].isEmpty)
            {
                emtyCell = field[x, y];
            }
            else
            {
                break;
            }
        }

        return emtyCell;
    }

    private void CheckGameResult()
    {
        bool lose = true;

        for (int x = 0; x < FieldSize; x++)
        {
            for (int y = 0; y < FieldSize; y++)
            {
                if (field[x, y].Value == Cell.MaxValue)
                {
                    GameController.Instance.Win();
                    return;
                }

                if (lose && field[x, y].isEmpty || FindCellToMerge(field[x, y], Vector2.left) || FindCellToMerge(field[x, y], Vector2.right) || FindCellToMerge(field[x, y], Vector2.up) || FindCellToMerge(field[x, y], Vector2.down))
                {
                    lose = false;
                }
            }
        }

        if (lose)
        {
            GameController.Instance.Lose();
        }
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

    private void ResetCellsFlags()
    {
        for (int x = 0; x < FieldSize; x++)
        {
            for (int y = 0; y < FieldSize; y++)
            {
                field[x, y].ResetFlags();
            }
        }
    }
}
