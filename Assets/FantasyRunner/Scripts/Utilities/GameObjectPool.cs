using System.Collections.Generic;
using MiupGames.Common.Singleton;
using UnityEngine;

public class GameObjectPool : Singleton<GameObjectPool>
{
    Dictionary<GameObject, Stack<GameObject>> _prefabObjectPools = new Dictionary<GameObject, Stack<GameObject>>();
    Dictionary<string, Stack<GameObject>> _nameObjectPools = new Dictionary<string, Stack<GameObject>>();
    Dictionary<GameObject, GameObject> _objectsFromPrefab = new Dictionary<GameObject, GameObject>();
    Dictionary<GameObject, string> _objectsFromName = new Dictionary<GameObject, string>();

    private GameObject PositionAndReturnObject(GameObject newObject, Vector3 position, Quaternion rotation, Transform parent)
    {
        newObject.SetActive(true);
        Transform objectTransform = newObject.transform;
        objectTransform.position = position;
        objectTransform.rotation = rotation;
        objectTransform.parent = parent;

        return newObject;
    }

    private GameObject InstantiateFromPrefab(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject newObject = Instantiate(prefab, position, Quaternion.identity, parent) as GameObject;
        this._objectsFromPrefab[newObject] = prefab;
        return newObject;
    }

    private GameObject InstantiateFromName(string name, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject newObject = Instantiate(Resources.Load(name), position, Quaternion.identity, parent) as GameObject;
        this._objectsFromName[newObject] = name;
        return newObject;
    }

    private GameObject GetObject<T>(
        T source, 
        Vector3 position, 
        Transform parent, 
        Dictionary<T, Stack<GameObject>> poolDictionary, 
        System.Func<T, Vector3, Quaternion, Transform, GameObject> instantiateMethod) where T : class
    {
        Stack<GameObject> pool;

        if (poolDictionary.TryGetValue(source, out pool))
        {
            if (pool.Count > 0)
            {
                return this.PositionAndReturnObject(pool.Pop(), position, Quaternion.identity, parent);
            }
        }
        else
        {
            poolDictionary[source] = new Stack<GameObject>();
        }

        return instantiateMethod(source, position, Quaternion.identity, parent);
    }

    public GameObject GetObject(GameObject prefab, Vector3 position, Transform parent)
    {
        return this.GetObject<GameObject>(prefab, position, parent, this._prefabObjectPools, this.InstantiateFromPrefab);
    }

    public T GetObject<T>(GameObject prefab, Vector3 position, Transform parent) where T : MonoBehaviour
    {
        return this.GetObject(prefab, position, parent).GetComponent<T>();
    }

    public GameObject GetObject(string prefabName, Vector3 position, Transform parent)
    {
        return this.GetObject<string>(prefabName, position, parent, this._nameObjectPools, this.InstantiateFromName);
    }

    public T GetObject<T>(string prefabName, Vector3 position, Transform parent) where T : MonoBehaviour
    {
        return this.GetObject(prefabName, position, parent).GetComponent<T>();
    }

    public void ReturnObject(GameObject returnedObject)
    {
        GameObject prefab;
        if (this._objectsFromPrefab.TryGetValue(returnedObject, out prefab))
        {
            this._prefabObjectPools[prefab].Push(returnedObject);
        }
        else
        {
            string name;
            if (this._objectsFromName.TryGetValue(returnedObject, out name))
            {
                this._nameObjectPools[name].Push(returnedObject);
            }
        }

        this.HideReturnedObject(returnedObject);
    }

    private void HideReturnedObject(GameObject returnedObject)
    {
        returnedObject.SetActive(false);
        returnedObject.transform.parent = this.transform;
    }
}