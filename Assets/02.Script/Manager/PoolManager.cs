using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingleTone<PoolManager>
{
    [System.Serializable]
    public class ObjectPool
    {
        public GameObject Prefab;

        public int PoolSize;
    }

    #region Variables

    public List<ObjectPool> PoolList;

    private Dictionary<string, Queue<GameObject>> objectpooldictionary;

    #endregion

    private void Awake()
    {
        if(PoolManager.Instance != null && PoolManager.Instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Init();
            DontDestroyOnLoad(this);
        }
    }

    private void Init()
    {
        objectpooldictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (ObjectPool pool in PoolList)
        {
            Queue<GameObject> poolqueue = new Queue<GameObject>();

            for (int i = 0; i < pool.PoolSize; i++)
            {
                GameObject obj = GameObject.Instantiate(pool.Prefab);

                obj.name = pool.Prefab.name;

                obj.transform.parent = this.transform;

                poolqueue.Enqueue(obj);

                obj.SetActive(false);
            }
            objectpooldictionary.Add(pool.Prefab.name, poolqueue);
        }
    }

    GameObject CreateNewObj(GameObject obj)
    {
        var newobj = GameObject.Instantiate(obj);

        newobj.transform.parent = this.transform;

        return newobj;
    }

    ObjectPool FindObjPool(string poolName)
    {
        return PoolList.Find(x => (x.Prefab != null && string.Equals(x.Prefab.name, poolName)));
    }

    public GameObject Spawn(string poolName, Vector3 position, Quaternion rotation)
    {
        GameObject obj = null;

        if (objectpooldictionary.ContainsKey(poolName))
        {
            if (objectpooldictionary[poolName].Count > 0)
            {
                foreach (GameObject q in objectpooldictionary[poolName])
                {
                    if (q.activeSelf == false)
                    {
                        obj = objectpooldictionary[poolName].Dequeue();

                        obj.transform.position = position;

                        obj.transform.rotation = rotation;

                        obj.SetActive(true);
                        break;
                    }
                }
            }
            else
            {
                ObjectPool pool = FindObjPool(poolName);

                if (pool != null)
                {
                    obj = CreateNewObj(pool.Prefab);

                    obj.name = pool.Prefab.name;

                    obj.transform.position = position;

                    obj.transform.rotation = rotation;

                    obj.SetActive(true);
                }
            }
        }
        else
        {
            Debug.LogError(poolName + " Object Pool Is Not Available");
        }
        return obj;
    }

    private void _despawn(GameObject poolObject)
    {
        if (objectpooldictionary.ContainsKey(poolObject.name))
        {
            poolObject.SetActive(false);

            objectpooldictionary[poolObject.name].Enqueue(poolObject);

        }
        else
        {
            Debug.LogError(poolObject.name + " Object Pool is Not Available");
        }
    }

    public void Despawn(GameObject poolObject)
    {
        _despawn(poolObject);
    }

}
