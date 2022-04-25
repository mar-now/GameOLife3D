using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : Singleton<CellManager>
{
    private static Cell2D _cellPrefab;
    private static List<CellBase> _cellList = new List<CellBase>();
    private static List<CellBase> _nextStageCellList = new List<CellBase>();
    private static Queue<CellBase> _cellPoolQueue = new Queue<CellBase>();

    private static float _simulationSpeed = 0.15f;
    private static bool _isSimulationPaused = false;
    private static float timeSinceLastEvolve = 0;


    private void Update()
    {
        Debug.Log(_cellList.Count);

        timeSinceLastEvolve += Time.deltaTime;
        if (_isSimulationPaused == false && timeSinceLastEvolve >= _simulationSpeed)
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
        foreach (Cell2D cell in _cellList)
            if (_nextStageCellList.Contains(cell) == false)
                PutCellInPool(cell);

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


    public static void PutCellInPool(CellBase cell)
    {
        cell.PutInPoolAction();
        _cellPoolQueue.Enqueue(cell);
    }

    public Transform GetTransform()
    {
        return Instance.transform;
    }
}
