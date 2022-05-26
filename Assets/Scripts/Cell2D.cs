using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell2D : CellBase, IPointerDownHandler
{
    /*private void Awake()
    {
        if(transform.parent.gameObject != CellManager)
    }*/
    private void OnEnable()
    {
        this.name = "Cell " + transform.position.x + " " + transform.position.y;
        CellManager.Instance.AddCell(this);
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

    // Function putting the cell in the next stage's list, if conditions are met
    public override void Check()
    {
        CountNeighboursAlive();

        // If there is at least one neigbour alive, we're addin the cell to the next stage's list
        // Thanks to this, we'll avoid removing cell's which would be respawned in nearest Sprout() call
        if (_neigboursAlive > 0)
        {
            if (IsAliveNow == true && (_neigboursAlive == 2 || _neigboursAlive == 3))
                _isAliveLater = true;
            else if (IsAliveNow == false && _neigboursAlive == 3)
                _isAliveLater = true;
            else
                _isAliveLater = false;

            CellManager.Instance.AddCell(this);
        }
        else
        {
            _isAliveLater = false;
            CellManager.Instance.SchedulePuttinInPool(this);
        }
    }

    // Function spawning new cells around living cell, so the new cells cn come alive
    // in future steps of evolution
    public override void Sprout()
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

                if (CellManager.Instance.GetCellAtPosition(neigbourPos) == null)
                {
                    CellManager.Instance.SpawnCell(neigbourPos);
                    //Debug.Log(transform.name + " sprout");
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + " click");
    }
}


