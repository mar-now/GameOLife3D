using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class CellManager : Singleton<CellManager>
{
    private static Cell2D _cellPrefab;
    private static List<CellBase> _cellList = new List<CellBase>();
    private static List<CellBase> _nextStageCellList = new List<CellBase>();
    private static List<CellBase> _cellsToBePooled = new List<CellBase>();
    private static Queue<CellBase> _cellPoolQueue = new Queue<CellBase>();

    private static float timeSinceLastEvolve = 0;



    private void Update()
    {
       // Debug.Log(_cellList.Count);

        timeSinceLastEvolve += Time.deltaTime;
        if (GamestateManager.IsSimulationPaused == false && timeSinceLastEvolve >=  1f / GamestateManager.SimulationSpeed)
        {
            EvolveCells();
            timeSinceLastEvolve = 0;
        }       
    }

    protected override void Awake()
    {
        base.Awake();
        _cellPrefab = Resources.Load<Cell2D>("Cell2D");

    }

    private void EvolveCells()
    {
        // Putting the cells which aren't present in the next stage in pool
        foreach (CellBase cell in _cellsToBePooled)
                PutCellInPool(cell);
        _cellsToBePooled.Clear();

        // Moving the cells from the next stage list to the current stage list
        _cellList = new List<CellBase>(_nextStageCellList);

        // Spawning new cells around living ones
        // New cells are dead, but they may come alive in the next stage
        foreach (Cell2D cell in _cellList)
            if (cell.IsAliveNow == true)
                cell.Sprout();

        // After Sprout() _nexStageCellList contains new cells, so I'm moving them
        // once again to the current stage list and clear the next stage list
        _cellList = new List<CellBase>(_nextStageCellList);
        _nextStageCellList.Clear();

        // Checking wheter the cells shoud be present in the next stage list and
        // puttting them there
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

    public static void SchedulePuttinInPool(CellBase cell)
    {
        _cellsToBePooled.Add(cell);
    }
    public static void PutCellInPool(CellBase cell)
    {
        cell.PutInPoolAction();
        _cellPoolQueue.Enqueue(cell);
    }

    public Transform GetTransform()
    {
        return Instance.transform;
    }

    public static Cell2D GetCellAtPosition(Vector2 position)
    {
        // Rounding coordinates, because position of centre of the cell has only integer values
        position.x = Mathf.RoundToInt(position.x);
        position.y = Mathf.RoundToInt(position.y);
        // "position" is always a centre of a cell, so we can raycast at any direction
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.up , 0.05f);

        if (hit.collider == null)
            return null;
        else
            return hit.collider.GetComponent<Cell2D>();
    }
}
