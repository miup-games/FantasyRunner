using UnityEngine;
using System;
using System.Collections;

public class DragController : MonoBehaviour
{
    private Transform _transform;
    private Camera _currentCamera;

    private Vector3 _originalPosition;
    private bool _dragging = false;
    private bool _dragEnabled = true;
    private DropController _currentDropArea = null;

    public Action<DragController, DropController> OnDrop; 

    public bool Dragging
    {
        get
        {
            return this._dragging && this._dragEnabled;
        }
    }

    public void EnableDrag(bool dragEnabled)
    {
        this._dragEnabled = dragEnabled;  
    }

    public void Return()
    {
        this._transform.position = this._originalPosition;
    }

    private void Awake()
    {
        this._transform = transform;
        this._currentCamera = Camera.main;
        this._originalPosition = this._transform.position;
    }

    void OnMouseDown()
    {
        if (this._dragEnabled)
        {
            this._dragging = true;
        }
    }

    void OnMouseUp()
    {
        if (this.Dragging)
        {
            this._dragging = false;

            if (this._currentDropArea != null)
            {
                if (this.OnDrop != null)
                {
                    this.OnDrop(this, this._currentDropArea);
                }
            }
            else
            {
                this.Return();
            }
        }

        if (this._currentDropArea != null)
        {
            this._currentDropArea.Unselect();
            this._currentDropArea = null;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.Dragging)
        {
            DropController dropArea = col.gameObject.GetComponent<DropController>();

            if (dropArea != null && dropArea.DropEnabled)
            {
                if (this._currentDropArea != null)
                {
                    this._currentDropArea.Unselect();
                }

                this._currentDropArea = dropArea;
                this._currentDropArea.Select();
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (this.Dragging)
        {
            if (this._currentDropArea == null)
            {
                return;
            }

            DropController dropArea = col.gameObject.GetComponent<DropController>();

            if (dropArea != null && dropArea == this._currentDropArea)
            {
                this._currentDropArea.Unselect();
                this._currentDropArea = null;
            }
        }
    }

    void Update()
    {
        if (this.Dragging)
        {
            var v3 = Input.mousePosition;
            v3.z = 0f;
            v3 = this._currentCamera.ScreenToWorldPoint(v3);

            v3.z = this._originalPosition.z;
            this._transform.position = v3;
        }
    }
}
