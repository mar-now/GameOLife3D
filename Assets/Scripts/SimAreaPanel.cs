using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimAreaPanel : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10;
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos = pos.Round();
        Debug.Log(pos);
        CellBase cell = CellManager.GetCellAtPosition(pos);
        if (cell == null)
        {
            CellManager.SpawnCell(pos);
            CellManager.GetCellAtPosition(pos).IsAliveNow = true;
            //Debug.Log("spawn");
        }
        else if (cell.IsAliveNow == false)
        {
            cell.IsAliveNow = true;
            //cell.IsAliveLater = true;
        }
        else if (cell.IsAliveNow == true)
            cell.IsAliveNow = false;
    }
}
