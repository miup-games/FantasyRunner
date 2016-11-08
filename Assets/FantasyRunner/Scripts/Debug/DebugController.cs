using System.Collections;
using UnityEngine;

public class DebugController : MonoBehaviour 
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private ItemsBaseController _itemsBaseController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < 3; i++)
            {
                this._gameManager.AddSpecialPower();
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            this._itemsBaseController.AddCoins(100);
        }
    }
}
