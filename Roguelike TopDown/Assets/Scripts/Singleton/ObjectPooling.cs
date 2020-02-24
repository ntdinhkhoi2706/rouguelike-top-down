using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : SingletonMonoBehaviour<ObjectPooling>
{
    List<int> PooledKeyList = new List<int>();
    Dictionary<int, List<GameObject>> PooledGoDic = new Dictionary<int, List<GameObject>>();

    protected override void Awake()
    {
        base.Awake();
    }

    public GameObject GetGameObject(GameObject prefab,Vector3 position,Quaternion rotation,bool forceInstantiate=false)
    {
        if (!prefab)
            return null;

        int key = prefab.GetInstanceID();

        if(!PooledKeyList.Contains(key) && !PooledGoDic.ContainsKey(key))
        {
            PooledKeyList.Add(key);
            PooledGoDic.Add(key, new List<GameObject>());
        }

        List<GameObject> goList = PooledGoDic[key];
        GameObject go = null;

        if (!forceInstantiate)
        {
            for (int i = goList.Count - 1; i >= 0; i--)
            {
                go = goList[i];
                if (go == null)
                {
                    goList.Remove(go);
                    continue;
                }
                if (go.activeSelf == false)
                {
                    // Found free GameObject in object pool.
                    Transform goTransform = go.transform;
                    goTransform.position = position;
                    goTransform.rotation = rotation;
                    go.SetActive(true);
                    return go;
                }
            }
        }

        // Instantiate because there is no free GameObject in object pool.
        go = (GameObject)Instantiate(prefab, position, rotation);
        go.transform.parent = _Transform;
        goList.Add(go);

        return go;

    }

    public void ReleaseGameObject(GameObject obj,bool destroy=false)
    {
        if(destroy)
        {
            Destroy(obj);
            return;
        }
        obj.SetActive(false);
    }

    public int GetActivePooledObjectCount()
    {
        int cnt = 0;
        for (int i = 0; i < PooledKeyList.Count; i++)
        {
            int key = PooledKeyList[i];
            var goList = PooledGoDic[key];
            for (int j = 0; j < goList.Count; j++)
            {
                var go = goList[j];
                if (go != null && go.activeInHierarchy)
                {
                    cnt++;
                }
            }
        }
        return cnt;
    }
}
