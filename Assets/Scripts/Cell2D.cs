using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;


public class Cell2D : MonoBehaviour
{
    [SerializeField] public GameObject _cellPrefab;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private bool _isAliveNow = false;
    [SerializeField] private bool _isAliveLater = false;
    public int _neigboursAlive;

    public bool IsAliveNow 
    { 
        get => _isAliveNow; 
        private set => _isAliveNow = value; 
    }

    private void Awake()
    {
        CellManager.AddCell(this);
        this.name = "Cell " + transform.position.x + " " + transform.position.y;
    }

    private void Update()
    {
        if (IsAliveNow == true)
            _spriteRenderer.color = Color.red;
        else
            _spriteRenderer.color = Color.white;
    }

    void CountNeighboursAlive()
    {
        int count = 0;
        Vector2 neigbourPos = new Vector2();

        for(int x = -1; x<=1; x++)
        {
            for(int y = -1; y<=1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                neigbourPos.x = transform.position.x + x;
                neigbourPos.y = transform.position.y + y;
                RaycastHit2D hit = Physics2D.Raycast((neigbourPos), neigbourPos, 0.1f);

                if (hit.collider != null)
                {
                    Cell2D neighbour = hit.collider.gameObject.GetComponent<Cell2D>();
                    if (neighbour.IsAliveNow == true)
                        count++;
                }
            }
        }

        _neigboursAlive = count;
    }

    public void UpdateState()
    {
        IsAliveNow = _isAliveLater;
    }

    public void OnInstantiate()
    {
        IsAliveNow = false;
        //Check();
    }
    public void Check()
    {
        CountNeighboursAlive();

        if (IsAliveNow == true && (_neigboursAlive == 2 || _neigboursAlive == 3))
        {
            _isAliveLater = true;
            //CellManager.AddCell(this);
        }
        else if (IsAliveNow == false && _neigboursAlive == 3)
        {
            _isAliveLater = true;
            //CellManager.AddCell(this);
        }
        else
            _isAliveLater = false;

        CellManager.AddCell(this);
    }

    public void Sprout()
    {
        if (IsAliveNow == false)
            return;

        Vector2 neigbourPos = new Vector2();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                neigbourPos.x = transform.position.x + x;
                neigbourPos.y = transform.position.y + y;
                RaycastHit2D hit = Physics2D.Raycast((neigbourPos), neigbourPos, 0.1f);

                if (hit.collider == null)
                {
                    Cell2D cell = Instantiate(_cellPrefab, neigbourPos, Quaternion.identity).GetComponent<Cell2D>();
                    Debug.Log(transform.name + " sprout");
                    cell.OnInstantiate();
                }
            }
        }

        //Debug.Break();
    }
}
