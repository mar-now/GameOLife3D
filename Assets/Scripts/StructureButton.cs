using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StructureButton : MonoBehaviour
{
    [SerializeField] string _structureName;
    [SerializeField] StructureBlueprint _structureBlueprint;
    [SerializeField] int tmp;


    public void InstantiateBlueprint()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10;
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos.z = 0;
        pos = pos.Round();

        UserInteractionManager.Instance._currentlyChosenBlueprint = Instantiate(_structureBlueprint, pos, Quaternion.identity);
    }
}
