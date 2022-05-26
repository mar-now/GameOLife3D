using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimAreaPanel : MonoBehaviour, IPointerDownHandler
{
    public event EventHandler PointerDown;
    public void OnPointerDown(PointerEventData eventData)
    {
        /* if (PointerDown != null)
             PointerDown(this, null);*/

        UserInteractionManager.Instance.OnSimPanelPointerDown();
    }
}
