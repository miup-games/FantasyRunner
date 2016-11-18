using UnityEngine;

public class ResetDataController : ButtonController 
{
    protected override void OnClick()
    {
        base.OnClick();
        PlayerRepository.DeleteAll();
    }
}
