using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : Singleton<CellManager>
{
    [SerializeField] public static List<Cell2D> _cells = new List<Cell2D>();
    public static List<Cell2D> _nextStageCells = new List<Cell2D>();

    private void Update()
    {
        Debug.Log(_cells.Count);
    }
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        InvokeRepeating("EvolveCells", 1f, 0.125f);
    }

    private void EvolveCells()
    {
        foreach (Cell2D cell in _cells)
            if (_nextStageCells.Contains(cell) == false)
                Destroy(cell.gameObject);
        _cells = new List<Cell2D>(_nextStageCells);

        foreach (Cell2D cell in _cells)
            if (cell.IsAliveNow == true)
                cell.Sprout();

        foreach (Cell2D cell in _cells)
            if (_nextStageCells.Contains(cell) == false)
                Destroy(cell.gameObject);
        _cells = new List<Cell2D>(_nextStageCells);
        _nextStageCells.Clear();

        foreach (Cell2D cell in _cells)
            cell.Check();

        foreach (Cell2D cell in _cells)
            cell.UpdateState();
    }


    public static void AddCell(Cell2D cell)
    {
        _nextStageCells.Add(cell);
    }

    public static void RemoveCell(Cell2D cell)
    {
        _cells.Remove(cell);
    }
}
