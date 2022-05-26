using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

// Class handling all clicks and UI logic
// Propably messy as hell but I don't know how to make it better
public class UserInteractionManager : Singleton<UserInteractionManager>
{
    public SimAreaPanel _simAreaPanel;

    public StructureBlueprint _currentlyChosenBlueprint = null;

    private void Awake()
    {
    }

    public void OnSimPanelPointerDown()
    {
        if(_currentlyChosenBlueprint != null)
        {
            if(_currentlyChosenBlueprint.AreThereLivingCellsUnderTheBlueprint() == false)
            {
                _currentlyChosenBlueprint.InstantiateStructureFromBlueprint();
                _currentlyChosenBlueprint = null;
            }
        }
        else
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 10;
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos.z = 0;
            pos = pos.Round();

            CellBase cell = CellManager.GetCellAtPosition(pos);

            if (cell != null)
                cell.IsAliveNow = !cell.IsAliveNow;
            else
                CellManager.SpawnCell(pos, true);
        }
    }
}
