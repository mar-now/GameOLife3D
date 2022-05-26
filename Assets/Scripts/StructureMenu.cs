using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructureMenu : MonoBehaviour
{
    public Button menuToggleButton;
    public ScrollRect scrollView;
    private bool _isExpanded = false;
    private RectTransform _rectTransform;

    public void Awake()
    {
        menuToggleButton.onClick.AddListener(ToggleMenu);
        _rectTransform = gameObject.GetComponent<RectTransform>();

        var content = scrollView.content.transform;
        foreach (RectTransform category in content)
        {
            StructureCategory tmp = category.GetComponent<StructureCategory>();
            tmp.HeaderButton.onClick.AddListener(delegate{ ResizeContentBoxOnCategoryClick(tmp); });
        }

        var contentSize = content.GetComponent<RectTransform>().sizeDelta;
        contentSize.y = CalculateContentBoxHeight();
        content.GetComponent<RectTransform>().sizeDelta = contentSize;
    }

    public void Update()
    {

        
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
            _rectTransform.anchoredPosition = new Vector2(-_rectTransform.sizeDelta.x, 0);

        _isExpanded = !_isExpanded;
    }

    private void ResizeContentBoxOnCategoryClick(StructureCategory category)
    {
        var content = scrollView.content.transform;
        var contentSize = scrollView.content.GetComponent<RectTransform>().sizeDelta;

        if (category.IsExpanded)
            contentSize.y = contentSize.y - category.DefaultHeight + category.ExpandedHeight;
        else
            contentSize.y = contentSize.y - category.ExpandedHeight + category.DefaultHeight;

        scrollView.content.GetComponent<RectTransform>().sizeDelta = contentSize;
    }

    // Todo: it has to include content box padding and viewport padding, for now the value is hardcoded
    private float CalculateContentBoxHeight()
    {
        float height = 0;

        var content = scrollView.content.transform;
        foreach (RectTransform category in content)
        {
            StructureCategory tmp = category.GetComponent<StructureCategory>();

            if (tmp.IsExpanded == true)
                height += tmp.ExpandedHeight;
            else
                height += tmp.DefaultHeight;
        }

        return height + 20;
    }
}
