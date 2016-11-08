using System;
using System.Collections;
using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour 
{
    [SerializeField] private Animator animator;
    [SerializeField] private Animation animation;
    [SerializeField] private string jumpKey = "jump";
    [SerializeField] private string attackKey = "attack";
    [SerializeField] private string runKey = "run";
    [SerializeField] private string dieKey = "die";
    [SerializeField] private string idleKey = "idle";
    [SerializeField] private string winKey = "win";

    [SerializeField] private float attackDuration = 0.5f;

    bool died = false;

    private void PlayAnimation(string animKey)
    {
        if (died)
        {
            return;
        }
        if(!string.IsNullOrEmpty(animKey))
        {
            if (animator != null)
            {
                animator.SetTrigger(Animator.StringToHash(animKey));
            }
            if (animation != null)
            {
                animation.Play(animKey);
            }
        }
    }

    public void Jump()
    {
        this.PlayAnimation(jumpKey);
    }

    public IEnumerator Attack()
    {
        this.PlayAnimation(attackKey);
        yield return new WaitForSeconds(attackDuration);
    }

    public void Run()
    {
        this.PlayAnimation(runKey);
    }

    public IEnumerator Win()
    {
        yield return new WaitForSeconds(1f);
        this.PlayAnimation(winKey);
    }

    public void Die()
    {
        this.PlayAnimation(dieKey);
        died = true;
    }

    public void Idle()
    {
        this.PlayAnimation(idleKey);
    }
}
