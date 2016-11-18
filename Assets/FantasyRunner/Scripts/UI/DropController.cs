using UnityEngine;
using System;
using System.Collections;

public class DropController : MonoBehaviour
{
    [SerializeField] private GameObject highlightObject;

    private bool _dropEnabled = true;

    public bool DropEnabled 
    { 
        get
        {
            return this._dropEnabled;
        }
    }

    void Awake()
    {
        this.Unselect();
    }

    public void EnableDrop(bool dropEnabled)
    {
        this._dropEnabled = dropEnabled;
    }

    public void Select()
    {
        if (this.highlightObject != null)
        {
            this.highlightObject.SetActive(true);  
        }
    }

    public void Unselect()
    {
        if (this.highlightObject != null)
        {
            this.highlightObject.SetActive(false);
        }
    }
}
