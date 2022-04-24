using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;


public class Cell2D : CellBase
{
    private void OnEnable()
    {
        CellManager.AddCell(this);
        this.name = "Cell " + transform.position.x + " " + transform.position.y;
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

    public override void Check()
    {
        CountNeighboursAlive();

        if(_neigboursAlive > 0)
        { 
        if (IsAliveNow == true && (_neigboursAlive == 2 || _neigboursAlive == 3))
        {
            _isAliveLater = true;
            CellManager.AddCell(this);
        }
        else if (IsAliveNow == false && _neigboursAlive == 3)
        {
            _isAliveLater = true;
            CellManager.AddCell(this);
        }
        else
            _isAliveLater = false;
        }

    }

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
                RaycastHit2D hit = Physics2D.Raycast((neigbourPos), neigbourPos, 0.1f);

                if (hit.collider == null)
                {
                    CellManager.SpawnCell(neigbourPos);
                    Debug.Log(transform.name + " sprout");
                }
            }
        }

        //Debug.Break();
    }
}
