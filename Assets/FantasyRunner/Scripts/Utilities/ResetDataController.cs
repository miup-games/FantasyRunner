using UnityEngine;

public class ResetDataController : ButtonController 
{
    protected override void OnClick()
    {
        PlayerRepository.DeleteAll();
    }
}
