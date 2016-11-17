using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectionController: OpenLevel 
{
    [SerializeField] private int _stageId;
    [SerializeField] private GameObject _lockIcon;
    [SerializeField] private BoxCollider2D _collider;

    private void Start()
    {
        int lastUnlockedStageId = PlayerRepository.GetLastUnlockedStage();
        this.LockStage(this._stageId > (lastUnlockedStageId + 1));
    }

    private void LockStage(bool locked)
    {
        this._lockIcon.SetActive(locked);
        this._collider.enabled = !locked;
    }

    protected override void OnClick()
	{
        PlayerRepository.SetCurrentStage(this._stageId);
        base.OnClick();
	}
}
