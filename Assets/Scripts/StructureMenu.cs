using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructureMenu : MonoBehaviour
{
    public Button menuToggleButton;
    private bool _isExpanded = false;
    private RectTransform _rectTransform;

    public void Awake()
    {
        menuToggleButton.onClick.AddListener(ToggleMenu);
        _rectTransform = gameObject.GetComponent<RectTransform>();
    }

    private void OnDestroy()
    {
        menuToggleButton.onClick.RemoveListener(ToggleMenu);
    }

    private void ToggleMenu()
    {
        if (_isExpanded == true)
            _rectTransform.anchoredPosition = new Vector2(0, 0);
        else
            _rectTransform.anchoredPosition = new Vector2(-200, 0);

        _isExpanded = !_isExpanded;
    }
}
