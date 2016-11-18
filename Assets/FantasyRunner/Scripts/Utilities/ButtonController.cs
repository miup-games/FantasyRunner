using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour 
{
    [SerializeField] private EventTrigger.TriggerEvent _customCallback;

    private Vector3 _regularScale;

    protected virtual void Awake()
    {
        this._regularScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        transform.localScale = this._regularScale * 0.9f;
    }

    private void OnMouseUp()
    {
        transform.localScale = this._regularScale;
        this.OnClick();
    }

    protected virtual void OnClick()
    {
        if (this._customCallback != null)
        {
            this._customCallback.Invoke(new BaseEventData(EventSystem.current));
        }
    }
}
