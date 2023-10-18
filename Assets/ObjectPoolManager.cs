using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static Dictionary<string, Pool> Pools = new();
    public static GameObject Spawn(GameObject type, Vector3 position, Quaternion rotation)
    {
        AddIfNecessary(type);
        Pool pool = Pools[type.name];
        return pool.GetEntity(position, rotation);
    }

    public static void Return(GameObject obj)
    {
        AddIfNecessary(obj);
        Pool pool = Pools[obj.name];
        pool.ReturnEntity(obj);
    }

    public static bool isPooled(GameObject type)
    {
        return Pools.ContainsKey(type.name);
    }

    private static void AddIfNecessary(GameObject type)
    {
        if (!Pools.ContainsKey(type.name))
        {
            Pools.Add(type.name, new Pool(type));
        }
    }
}

public class Pool
{
    private Stack<GameObject> entities;
    private GameObject prototype;

    public Pool(GameObject prototype)
    {
        entities = new();
        this.prototype = prototype;
    }

    public GameObject GetEntity(Vector3 position, Quaternion rotation)
    {
        if (entities.Count > 0)
        {
            GameObject entity = entities.Pop();
            entity.transform.position = position;
            entity.transform.rotation = rotation;
            entity.SetActive(true);
            return entity;
        }
        else
        {
            return Object.Instantiate(prototype, position, rotation);
        }
    }

    public void ReturnEntity(GameObject entity)
    {
        entity.SetActive(false);
        entities.Push(entity);
    }
}
