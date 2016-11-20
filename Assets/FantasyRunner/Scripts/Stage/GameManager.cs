using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    [SerializeField] private CharacterController _playerCharacter;
    [SerializeField] private StageInfoLabelController _infoLabelController;

    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private CumulativeUIBaseController _specialPowerController;
    [SerializeField] private PowerItemUsageController _powerItemUsageController;
    [SerializeField] private LootAnimationController _powerLootAnimationController;
    [SerializeField] private Pause _pauseController;

    private StageScroll _stageScroll;
    private int _waveIndex = 0;
    private int _enemyIndex = 0;
    private List<List<StageEnemy>> _enemies;
    private Stage _stage;

    private bool _finished = false;

    public System.Action<bool> OnFinishStage;

    private bool InFirstWave
    {
        get
        {
            return this._waveIndex == 0;
        }
    }

    private bool InLastWave
    {
        get
        {
            return this._waveIndex == this._enemies.Count - 1;
        }
    }

    private bool IsWaveFinished
    {
        get
        {
            return this._enemyIndex == this._enemies[_waveIndex].Count;
        }
    }

    private bool IsLastWaveFinished
    {
        get
        {
            return this._waveIndex == this._enemies.Count;
        }
    }

    private void Awake()
    {
        Input.multiTouchEnabled = false;
        GameObjectPool.Instantiate();
        this.CreateStage();
    }

    private void CreateStage()
    {
        this._stage = StageRepository.GetStageById(PlayerRepository.GetCurrentStage());

        this._enemies = this._stage.Waves;

        GameObject stagePrefab = Resources.Load(this._stage.PrefabName) as GameObject;
        GameObject newObject = Instantiate(stagePrefab, Vector3.zero, Quaternion.identity, this.transform) as GameObject;
        this._stageScroll = newObject.GetComponent<StageScroll>();

        AudioManager.instance.PlayMusic(this._stage.MusicName);
    }

    private void Start() 
	{
        this.SetPlayer();
        StartCoroutine(this.GoToNextEnemy());
	}

    private void SetPlayer()
    {
        Character character = CharacterRepository.GetPlayer();
        this._playerCharacter.Initialize(character);

        this._powerItemUsageController.Initialize(this._playerCharacter);
        this._playerCharacter.SetSpeed(-this._stageScroll.GroundSpeed);
        this._stageScroll.SetBuffManager(this._playerCharacter.BuffManager);

        this._playerCharacter.OnAttackStart += AttackStart;
        this._playerCharacter.OnRunStart += RunStart;
        this._playerCharacter.OnDie += HandlePlayerDie;
    }

    private void AttackStart()
    {
        this._stageScroll.SetBattle(true);
    }

    private void RunStart()
    {
        this._stageScroll.SetBattle(false);
    }

    private void HandlePlayerDie(CharacterController character)
	{
        //FINISH LOSE
        this.Finish(false);
        AudioManager.instance.PlayFx("Lose");

        character.OnDie -= this.HandlePlayerDie;
        StartCoroutine(this._infoLabelController.ShowLoseText());
	}

    void HandleEnemyKill(CharacterController character)
    {
        character.OnDie -= this.HandleEnemyKill;
        character.OnRemove -= this.HandleEnemyRemove;
        this._scoreController.AddScore(character.DiePoints);

        StartCoroutine(GoToNextEnemy());
    }

    void HandleEnemyRemove(CharacterController character)
    {
        character.OnDie -= HandleEnemyKill;
        character.OnRemove -= HandleEnemyRemove;
        StartCoroutine(GoToNextEnemy());
    }

    IEnumerator GoToNextEnemy()
    {
        if (this._finished)
        {
            yield break;
        }

        if (this.IsWaveFinished)
        {
            this._enemyIndex = 0;
            this._waveIndex++;
        }

        if (this.IsLastWaveFinished)
        {
            //WIN
            yield return 0;
            this.Win();
            yield break;
        }

        if (this._enemyIndex == 0)
        {
            AudioManager.instance.PlayFx("NextWave");

            if (this.InFirstWave)
            {
                yield return StartCoroutine(_infoLabelController.ShowStartText());
            }
            else if(this.InLastWave)
            {
                this._powerItemUsageController.StopPower();
                yield return StartCoroutine(this._infoLabelController.ShowLastWaveText());
            }
            else
            {
                yield return StartCoroutine(this._infoLabelController.ShowWaveText());
            }
        }

        StartCoroutine(CreateEnemy(_enemies[this._waveIndex][_enemyIndex]));

        this._enemyIndex++;
    }

    void Win()
    {
        this.Finish(true);
        StartCoroutine(_infoLabelController.ShowWinText());
        this._playerCharacter.Win();

        PlayerRepository.SetLastUnlockedStage(this._stage.Id);
        AudioManager.instance.PlayFx("Win");
    }

    void Finish(bool win)
    {
        this._finished = true;
        this._powerItemUsageController.StopPower();
        this._stageScroll.StopMovement();

        if (this.OnFinishStage != null)
        {
            this.OnFinishStage(win);
        }

        StartCoroutine(this.FinishCoroutine());
    }

    IEnumerator FinishCoroutine()
    {
        yield return new WaitForSeconds(2f);
        this._pauseController.StartPause(true);
    }

    IEnumerator CreateEnemy(StageEnemy stageEnemy)
	{
        //yield break;
        yield return new WaitForSeconds(stageEnemy.Delay);

        Vector3 position = new Vector3(StageConstants.GROUND_ENEMY_POSITION_X, StageConstants.GROUND_POSITION_Y, 0);

        Character character = CharacterRepository.GetCharacterByName(stageEnemy.EnemyName);

        CharacterController enemyCharacter =
            (Instantiate(Resources.Load(character.PrefabName), position, Quaternion.identity) as GameObject).
            GetComponent<CharacterController>();

        enemyCharacter.Initialize(character);
        enemyCharacter.OnDie += HandleEnemyKill;
        enemyCharacter.OnRemove += HandleEnemyRemove;
	}

    public ItemUsageController PlaceItem(string itemName, Vector3 position)
    {
        GameObject itemObject = Instantiate (Resources.Load (itemName)) as GameObject;
        ItemUsageController itemController = itemObject.GetComponent<ItemUsageController>();

        Transform itemTransform = itemController.transform;
        itemTransform.position = new Vector3(position.x, StageConstants.GROUND_POSITION_Y, 0);

        itemController.StartMovement();

        AudioManager.instance.PlayFx("Item");
        return itemController;
    }

    public void AddSpecialPower(Vector3 position)
    {
        this._powerLootAnimationController.AddLoot(position);
        this.AddSpecialPower();
    }

    public void AddSpecialPower()
    {
        AudioManager.instance.PlayFx("AddPower");
        if (!this.InLastWave && !this.IsLastWaveFinished && this._specialPowerController.Add())
        {
            this._powerItemUsageController.StartPower( () => 
                {
                    this._specialPowerController.Reset();
                }
            );
        }
    }
}
