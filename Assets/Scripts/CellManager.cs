using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : Singleton<CellManager>
{
    private static Cell2D _cellPrefab;
    private static List<CellBase> _cellList = new List<CellBase>();
    private static List<CellBase> _nextStageCellList = new List<CellBase>();
    private static Queue<CellBase> _cellPoolQueue = new Queue<CellBase>();

    private void Update()
    {
        Debug.Log(_cellList.Count);
    }
    protected override void Awake()
    {
        base.Awake();
        _cellPrefab = Resources.Load<Cell2D>("Cell2D");
    }

    private void Start()
    {
        InvokeRepeating("EvolveCells", 1f, 0.125f);
    }

    private void EvolveCells()
    {
        foreach (Cell2D cell in _cellList)
            if (_nextStageCellList.Contains(cell) == false)
                PutCellInPool(cell);

        _cellList = new List<CellBase>(_nextStageCellList);

        foreach (Cell2D cell in _cellList)
            if (cell.IsAliveNow == true)
                cell.Sprout();

        foreach (Cell2D cell in _cellList)
            if (_nextStageCellList.Contains(cell) == false)
                PutCellInPool(cell);
        _cellList = new List<CellBase>(_nextStageCellList);
        _nextStageCellList.Clear();

        foreach (CellBase cell in _cellList)
            cell.Check();

        foreach (CellBase cell in _cellList)
            cell.UpdateState();
    }

    public static void AddCell(CellBase cell)
    {
        _nextStageCellList.Add(cell);
    }
    public static void SpawnCell(Vector2 position)
    {
        CellBase cell;

        if (_cellPoolQueue.Count > 0)
        {
            cell = _cellPoolQueue.Dequeue();
            cell.transform.position = position;
            cell.GetFromPoolAction();
        }
        else
            cell = Instantiate(_cellPrefab, position, Quaternion.identity);
        
    }


    public void PutCellInPool(CellBase cell)
    {
        cell.PutInPoolAction();
        _cellPoolQueue.Enqueue(cell);
    }

}
