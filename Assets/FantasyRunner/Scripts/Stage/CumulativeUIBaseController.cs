using UnityEngine;
using System.Collections;

public class CumulativeUIBaseController : MonoBehaviour 
{
    [SerializeField] private Color _offColor;
    [SerializeField] private Color _onColor;
    [SerializeField] SpriteRenderer[] _items;
	
    public int Current { get; private set;}

    public int Max
    {
        get
        {
            return this._items.Length;
        }
    }

    protected virtual void Awake()
    {
        this.Reset();
    }

    public virtual bool Add()
    {
        if(this.Current >= this.Max)
        {
            return false;
        }

        this.Current++;
        this._items[this.Current - 1].color = this._onColor;

        return this.Current == this.Max;
    }

    public virtual void Reset()
    {
        this.Current = 0;
        for(int i = 0; i < this._items.Length; i++)
        {
            this._items[i].color = this._offColor;
        }
    }
}