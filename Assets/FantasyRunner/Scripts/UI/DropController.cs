using UnityEngine;
using System;
using System.Collections;

public class DropController : MonoBehaviour
{
    [SerializeField] GameObject highlightObject;

    void Awake()
    {
        this.Unselect();
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
