using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class CellManager : Singleton<CellManager>
{
    private Cell2D _cellPrefab;
    private List<CellBase> _cellList = new List<CellBase>();
    private List<CellBase> _nextStageCellList = new List<CellBase>();
    private List<CellBase> _cellsToBePooled = new List<CellBase>();
    private Queue<CellBase> _cellPoolQueue = new Queue<CellBase>();

    private static float timeSinceLastEvolve = 0;

    [SerializeField] private int _simulationFieldWidth = 10000;
    [SerializeField] private int _simulationFieldHeight = 10000;
    [SerializeField] private int _simulationFieldDepth = 10000;

    private void Update()
    {
       // Debug.Log(_cellList.Count);

        timeSinceLastEvolve += Time.deltaTime;
        if (GameManager.Instance.IsSimulationPaused == false && timeSinceLastEvolve >=  1f / GameManager.Instance.SimulationSpeed)
        {
            EvolveCells();
            timeSinceLastEvolve = 0;
        }       
    }

    protected override void Awake()
    {
        base.Awake();
        _cellPrefab = Resources.Load<Cell2D>("Prefabs/2D/Cell2D");

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(_simulationFieldWidth, _simulationFieldHeight));
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

    public bool CheckIfCellIsOutOfBounds(CellBase cell)
    {
        Vector3 position = cell.transform.position;
        if (position.x < -(_simulationFieldWidth / 2) || position.x > (_simulationFieldWidth / 2) ||
            position.y < -(_simulationFieldHeight / 2) || position.y > (_simulationFieldHeight / 2) ||
            position.z < -(_simulationFieldDepth / 2) || position.z > (_simulationFieldDepth / 2))
            return true;
        else
            return false;
    }

    public void AddCell(CellBase cell)
    {
        _nextStageCellList.Add(cell);
    }
    public void SpawnCell(Vector2 position, bool isAliveNow = false)
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

        cell.IsAliveNow = isAliveNow;
    }

    public void SchedulePuttinInPool(CellBase cell)
    {
        _cellsToBePooled.Add(cell);
    }
    public void PutCellInPool(CellBase cell)
    {
        cell.PutInPoolAction();
        _cellPoolQueue.Enqueue(cell);
    }

    public Transform GetTransform()
    {
        return Instance.transform;
    }

    public Cell2D GetCellAtPosition(Vector2 position)
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
