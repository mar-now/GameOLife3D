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

    private void Update()
    {
        if (IsAliveNow == true)
            _spriteRenderer.color = Color.red;
        else
            _spriteRenderer.color = Color.white;
    }

    public abstract void Check();
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
