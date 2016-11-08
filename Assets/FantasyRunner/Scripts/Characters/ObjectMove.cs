using UnityEngine;
using System.Collections;

public class ObjectMove : MonoBehaviour 
{
	public float speed;
    public bool useFrontLimit = false;

    private bool movementEnabled = true;
    private float speedMultiplier = 1f;
    private StageScroll stageScroll;
    private Coroutine restoreSpeedFactorCoroutine;

    public void SetSpeed(float speed)
    {
        this.StopResetSpeedFactorCoroutine();
        this.speed = speed;
    }

    public void SetSpeedFactor(float speedFactor, float time = 0)
    {
        this.StopResetSpeedFactorCoroutine();
        this.speedMultiplier = speedFactor;
        if (time > 0)
        {
            this.restoreSpeedFactorCoroutine = StartCoroutine(ResetSpeedFactorWithDelay(time));
        }
    }

    public void EnableMovement(bool movementEnabled)
    {
        this.StopResetSpeedFactorCoroutine();
        this.movementEnabled = movementEnabled;
    }

    private void Start()
    {
        this.stageScroll = FindObjectOfType<StageScroll>();
    }

    private void StopResetSpeedFactorCoroutine()
    {
        if (this.restoreSpeedFactorCoroutine != null)
        {
            StopCoroutine(this.restoreSpeedFactorCoroutine);
        }
        this.restoreSpeedFactorCoroutine = null;
    }

    private IEnumerator ResetSpeedFactorWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.SetSpeedFactor(1f);
    }

	private void Update ()
	{
        if (!movementEnabled)
        {
            return;
        }

        if (Time.timeScale > 0)
        {
            this.Move(((speed * speedMultiplier) + this.stageScroll.GroundSpeed) * Time.timeScale);
        }
        if (transform.localPosition.x <= StageConstants.GROUND_BACK_LIMIT_X)
		{
			Destroy(gameObject);
		}
	}

    private void Move(float currentSpeed)
    {
        float x = transform.localPosition.x + currentSpeed;
        if (this.useFrontLimit && x > StageConstants.GROUND_FRONT_LIMIT_X)
        {
            x = StageConstants.GROUND_FRONT_LIMIT_X;
        }

        if (Time.timeScale > 0)
        {
            transform.localPosition = new Vector3(x, transform.localPosition.y, 0);
        }
    }
}
