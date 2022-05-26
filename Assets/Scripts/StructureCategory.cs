using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructureCategory : MonoBehaviour
{
    [SerializeField] private Button _headerButton;
    [SerializeField] private RectTransform _content;
    [SerializeField] private bool _isExpanded = false;

    private RectTransform _myRectTransform;

    private float _defaultHeight = 0;
    private float _expandedHeight = 0;

    #region Properties for earlier declared fields
    public Button HeaderButton { get => _headerButton;}
    public float DefaultHeight { get => _defaultHeight;}
    public float ExpandedHeight { get => _expandedHeight;}
    public bool IsExpanded { get => _isExpanded;}
    #endregion

    public void Awake()
    {
        if (_myRectTransform == null)
            _myRectTransform = this.GetComponent<RectTransform>();

        _defaultHeight = _headerButton.GetComponent<RectTransform>().sizeDelta.y;
        _expandedHeight = CalculateExpandedHeight();
    }

    private float CalculateExpandedHeight()
    {
        float calculatedHeight = 0;
        foreach(RectTransform button in _content)
            calculatedHeight += button.sizeDelta.y;

        calculatedHeight += _defaultHeight + 5;

        return calculatedHeight;
    }

    public void Toggle()
    {
        var tmp = _myRectTransform.sizeDelta;

        if (_isExpanded == true)
            tmp.y = _defaultHeight;
        else
            tmp.y = _expandedHeight;

        _myRectTransform.sizeDelta = tmp;

        _isExpanded = !_isExpanded;

        LayoutRebuilder.MarkLayoutForRebuild(this.transform.parent.GetComponent<RectTransform>());
    }
}
