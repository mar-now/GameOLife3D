using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CellBase : MonoBehaviour
{

    [SerializeField] public GameObject _cellPrefab;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] protected bool _isAliveNow = false;
    [SerializeField] protected bool _isAliveLater = false;
    public int _neigboursAlive;

    public bool IsAliveNow
    {
        get => _isAliveNow;
        private set => _isAliveNow = value;
    }

    private void Awake()
    {
        transform.SetParent(CellManager.Instance.transform);
    }
    private void Update()
    {
        Color tmp = _spriteRenderer.color;

        if (_isAliveNow == true)
            tmp.a = 255;
        else
            tmp.a = 0;

        _spriteRenderer.color = tmp;
            
    }

    // Function putting the cell in the next stage's list, if conditions are met
    public abstract void Check();

    // Function spawning new cells around living cell, so the new cells cn come alive
    // in future steps of evolution
    public abstract void Sprout();

    public void PutInPoolAction()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void UpdateState()
    {
        IsAliveNow = _isAliveLater;
    }
    public void GetFromPoolAction()
    {
        this.gameObject.SetActive(true);
    }
}
