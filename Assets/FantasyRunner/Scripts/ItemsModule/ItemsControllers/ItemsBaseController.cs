using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemsBaseController : MonoBehaviour 
{
    [SerializeField] protected GameObject _itemPrefab;
    [SerializeField] protected GameManager _gameManager;
    [SerializeField] protected Transform[] _itemSlots;
    [SerializeField] protected TextMesh _infoText;
    [SerializeField] protected WeaponUIController _weaponUIController;
    [SerializeField] protected GameObject _powerItemPrefab;

    protected List<Item> _allCurrentItems;
    protected int _coins = 0;

    protected virtual void Awake()
    {
        this._allCurrentItems = PlayerRepository.GetCurrentItems();
        this._gameManager.OnFinishStage += OnFinishStage;
    }

    protected virtual void Start()
    {
        StartCoroutine(this.AddPowerItems());
    }

    private void OnFinishStage(bool win)
    {
        if(win)
        {
            PlayerRepository.AddCoins(this._coins);
        }
    }

    private IEnumerator AddPowerItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(ItemConstants.POWER_ITEM_DELAY);
            {
                if (Random.Range(0, 1f) > (1f - ItemConstants.POWER_ITEM_PBB))
                {
                    Item item = null;
                    do 
                        item = this._allCurrentItems[Random.Range(0, this._allCurrentItems.Count)];
                    while(item.Cost == 0);

                    Vector3 position = new Vector3(StageConstants.GROUND_ENEMY_POSITION_X, StageConstants.GROUND_POSITION_Y, 0);

                    PowerItemController powerItem = 
                        (Instantiate(this._powerItemPrefab, position, Quaternion.identity) as GameObject).
                        GetComponent<PowerItemController>();

                    powerItem.Initialize(item);
                }
            }
        }
    }

    public abstract void UseItem(ItemUIController itemController);

    public virtual void RecycleItem(ItemUIController itemController) {}

    public virtual void RemoveItem(ItemUIController itemController) {}

    public virtual void AddCoins(int coins, Vector3 position)
    {
        this.AddCoins(coins);
    }

    public virtual void AddCoins(int coins)
    {
        this._coins = Mathf.Clamp(this._coins + coins, 0, int.MaxValue);
        this._infoText.text = "" + this._coins;
    }

    public void AddWeapon(WeaponItem weaponItem)
    {
        this._weaponUIController.SetWeapon(weaponItem);
    }
}
