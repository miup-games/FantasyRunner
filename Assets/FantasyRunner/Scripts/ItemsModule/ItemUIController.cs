using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIController : MonoBehaviour 
{
    [SerializeField] private DragController _dragController;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ProgressBarController _delayBar;
    [SerializeField] private TextMesh _costText;
    [SerializeField] private GameObject _disabledObject;

    public Transform Slot { get; private set; }
    public Item Item { get; private set; }
    public bool CanUse { get; private set;}

    private List<PowerItemController> _activatedPowerItems = new List<PowerItemController>();

    public bool CanAffort(int coins)
    {
        return coins >= this.Item.Cost;
    }

    private void Awake()
    {
        this._disabledObject.SetActive(false);
        this.CanUse = false;
        this._dragController.OnDrop += OnDrop;
    }

    private void Start()
    {
        StartCoroutine(DelayCoroutine());
    }

    private IEnumerator DelayCoroutine()
    {
        this.CanUse = false;

        float currentTime = 0;
        while (true)
        {
            currentTime += Time.deltaTime;
            this._delayBar.SetValue(((this.Item.Delay - currentTime) / this.Item.Delay));

            if (currentTime >= this.Item.Delay)
            {
                break;
            }

            yield return 0;
        }

        this.CanUse = true;
    }

    private void OnDrop(DragController dragObject, DropController dropObject)
    {
        ItemDropController itemDropController = dropObject.GetComponent<ItemDropController>();

        if (itemDropController != null)
        {
            itemDropController.ProccesItem(this);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject otherObject = col.gameObject;
        PowerItemController powerItem = otherObject.GetComponent<PowerItemController>();

        if (powerItem != null && powerItem.Item.Id == Item.Id)
        {
            this._activatedPowerItems.Add(powerItem);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        GameObject otherObject = col.gameObject;
        PowerItemController powerItem = otherObject.GetComponent<PowerItemController>();

        if (powerItem != null && powerItem.Item.Id == Item.Id)
        {
            this._activatedPowerItems.Remove(powerItem);
        }
    }

    public void Return()
    {
        this._dragController.Return();
    }

    public void SetItem(Item item, Transform slot)
    {
        this.Slot = slot;
        this.Item = item;
        this._costText.text = "" + item.Cost;
        this._spriteRenderer.sprite = Resources.Load<Sprite>(Item.IconName);
    }

    public void UpdateCoins(int coins)
    {
        bool canAfford = this.CanAffort(coins);
        this._disabledObject.SetActive(!canAfford);
    }

    public void Use(ItemsBaseController itemsController, GameManager gameManager, bool discard = true)
    {
        ItemUsageController itemUsageController = gameManager.PlaceItem(this.Item, this.transform.position);
        itemUsageController.SetController(itemsController);

        if (this._activatedPowerItems.Count > 0)
        {
            this._activatedPowerItems[0].Use();
            gameManager.AddSpecialPower(this._activatedPowerItems[0].transform.position);
        }

        this._activatedPowerItems.Clear();

        if (discard)
        {
            this.Discard();
        }
        else
        {
            this._dragController.Return();
            StartCoroutine(DelayCoroutine());
        }
    }

    public void Discard()
    {
        Destroy(this.gameObject);
    }
}
