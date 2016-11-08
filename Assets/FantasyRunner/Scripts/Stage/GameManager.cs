using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public Character playerCharacter;
    public StageScroll stage;
    public StageInfoLabelController infoLabelController;

	public AudioSource audioSource;
    public AudioClip itemSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip nextWaveSound;
    public AudioClip addPowerSound;

    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private CumulativeUIBaseController _specialPowerController;
    [SerializeField] private PowerItemUsageController _powerItemUsageController;
    [SerializeField] private LootAnimationController _powerLootAnimationController;

    private int _waveIndex = 0;
    private int _enemyIndex = 0;
    private List<List<StageEnemy>> _enemies;

    private bool _finished = false;

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

    void Awake()
    {
        Input.multiTouchEnabled = false;
        GameObjectPool.Instantiate();
    }

	void Start() 
	{
        this._powerItemUsageController.Initialize(playerCharacter);
        this._enemies = DataConstants.GetEnemies();

        StartCoroutine(GoToNextEnemy());

        this.playerCharacter.SetSpeed(-stage.GroundSpeed);
        this.playerCharacter.OnDie += HandlePlayerDie;
	}

    void HandlePlayerDie(Character character)
	{
        //FINISH LOSE
        this.Finish();
        this.PlaySound(loseSound);
        character.OnDie -= this.HandlePlayerDie;
        StartCoroutine(this.infoLabelController.ShowLoseText());
	}

    void HandleEnemyKill(Character character)
    {
        character.OnDie -= this.HandleEnemyKill;
        character.OnRemove -= this.HandleEnemyRemove;
        this._scoreController.AddScore(character.DiePoints);

        StartCoroutine(GoToNextEnemy());
    }

    void HandleEnemyRemove(Character character)
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
            this.PlaySound(nextWaveSound);

            if (this.InFirstWave)
            {
                yield return StartCoroutine(infoLabelController.ShowStartText());
            }
            else if(this.InLastWave)
            {
                this._powerItemUsageController.StopPower();
                yield return StartCoroutine(this.infoLabelController.ShowLastWaveText());
            }
            else
            {
                yield return StartCoroutine(this.infoLabelController.ShowWaveText());
            }
        }

        StartCoroutine(CreateEnemy(_enemies[this._waveIndex][_enemyIndex]));

        this._enemyIndex++;
    }

    void Win()
    {
        this.Finish();
        StartCoroutine(infoLabelController.ShowWinText());
        this.playerCharacter.Win();

        this.PlaySound(winSound);
    }

    void Finish()
    {
        this._finished = true;
        this._powerItemUsageController.StopPower();
        this.stage.StopMovement();
    }

    IEnumerator CreateEnemy(StageEnemy stageEnemy)
	{
        //yield break;
        yield return new WaitForSeconds(stageEnemy.Delay);

        Vector3 position = new Vector3(StageConstants.GROUND_ENEMY_POSITION_X, StageConstants.GROUND_POSITION_Y, 0);

        Character enemyCharacter =
            (Instantiate(Resources.Load(stageEnemy.EnemyAsset), position, Quaternion.identity) as GameObject).
            GetComponent<Character>();
        
        enemyCharacter.OnDie += HandleEnemyKill;
        enemyCharacter.OnRemove += HandleEnemyRemove;
	}

    private void PlaySound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public ItemUsageController PlaceItem(string itemName, Vector3 position)
    {
        GameObject itemObject = Instantiate (Resources.Load (itemName)) as GameObject;
        ItemUsageController itemController = itemObject.GetComponent<ItemUsageController>();

        Transform itemTransform = itemController.transform;
        itemTransform.position = new Vector3(position.x, StageConstants.GROUND_POSITION_Y, 0);

        itemController.NormalizeSpeed(stage);

        this.PlaySound(itemSound);
        return itemController;
    }

    public void AddSpecialPower(Vector3 position)
    {
        this._powerLootAnimationController.AddLoot(position);
        this.AddSpecialPower();
    }

    public void AddSpecialPower()
    {
        this.PlaySound(this.addPowerSound);
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
