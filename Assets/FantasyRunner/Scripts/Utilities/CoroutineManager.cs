using System.Collections;
using MiupGames.Common.Singleton;
using UnityEngine;

public class CoroutineManager : Singleton<GameObjectPool>
{
    public new Coroutine StartCoroutine(IEnumerator couroutine)
    {
        return base.StartCoroutine(couroutine);
    }

    public new void StopCoroutine(IEnumerator couroutine)
    {
        base.StopCoroutine(couroutine);
    }

    public new void StopCoroutine(Coroutine couroutine)
    {
        base.StopCoroutine(couroutine);
    }
}