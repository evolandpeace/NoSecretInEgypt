using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PoolTool : MonoBehaviour
{
    /* public GameObject objprefab;

     public ObjectPool<GameObject> pool;

     private void Start()
     {
         pool = new ObjectPool<GameObject>(
             createFunc: () => Instantiate(objprefab, transform),
             actionOnGet: (obj) => obj.SetActive(true),
             actionOnRelease: (obj) => obj.SetActive(false),
             collectionCheck: false,
             maxSize: 20
         );
         PreFillPool(10);
     }
     private void PreFillPool(int count)
     {
         var preFillArray = new GameObject[count];
         for (int i = 0; i < count; i++)
         {
             preFillArray[i] = pool.Get();
         }
         foreach (var item in preFillArray)
         {
             pool.Release(item);
         }
     }
     public GameObject GetGameObjectFromPool()
     {

         return pool.Get();

     }
     public void ReturnGameObjectToPool(GameObject obj)
     {
         pool.Release(obj);
     }
 */
    public static List<PooledObjectInfo> objectPools = new List<PooledObjectInfo>();

    public  GameObject SpawnObject(GameObject objectToSpawn)
    {
        PooledObjectInfo pool = objectPools.Find(p => p.LookuoString == objectToSpawn.name);
        
        if(pool == null)
        {
            pool = new PooledObjectInfo() {LookuoString =  objectToSpawn.name};
            objectPools.Add(pool);
        }

        GameObject spawnableObject = pool.InactiveObjects.FirstOrDefault();

        if(spawnableObject==null)
        {
            spawnableObject = Instantiate(objectToSpawn,this.transform);
        }
        else
        {
            pool.InactiveObjects.Remove(spawnableObject);
            spawnableObject.SetActive(true);
        }

        return spawnableObject;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        string name = obj.name.Substring(0, obj.name.Length - 7);
        PooledObjectInfo pool = objectPools.Find(p => p.LookuoString == name);

        if(pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled: "+obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }
}

public class PooledObjectInfo
{
    public string LookuoString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}

