using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : SingletonMonoBehaviour<ObjectPooling>
{
    List<int> KeyList = new List<int>();
    Dictionary<int, List<GameObject>> GoDic = new Dictionary<int, List<GameObject>>();

    protected override void Awake()
    {
        base.Awake();
    }

    public GameObject GetGameObject(GameObject prefab,Vector3 position,Quaternion rotation,bool forceInstantiate=false)
    {
        if (!prefab)
            return null;

        int key = prefab.GetInstanceID();

        if(!KeyList.Contains(key) && !GoDic.ContainsKey(key))
        {
            KeyList.Add(key);
            GoDic.Add(key, new List<GameObject>());
        }

        List<GameObject> goList = GoDic[key];
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
                    Transform goTransform = go.transform;
                    goTransform.position = position;
                    goTransform.rotation = rotation;
                    go.SetActive(true);
                    return go;
                }
            }
        }
        go =Instantiate(prefab, position, rotation);
        go.transform.parent = Transform;
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
        for (int i = 0; i < KeyList.Count; i++)
        {
            int key = KeyList[i];
            var goList = GoDic[key];
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
