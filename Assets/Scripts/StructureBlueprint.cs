using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

using UnityEngine;
using UnityEngine.EventSystems;

public class StructureBlueprint : MonoBehaviour
{
    [SerializeField] private GameObject _structurePrefab;

    public void Update()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10;
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos.z = 0;
        pos = pos.Round();

        transform.position = pos;

/*        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(_structurePrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }*/
    }

    public bool AreThereLivingCellsUnderTheBlueprint()
    {
        foreach(Transform child in transform)
            if (CellManager.GetCellAtPosition(transform.position) != null)
                return true;

        return false;
    }

    public void InstantiateStructureFromBlueprint()
    {
        Instantiate(_structurePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
