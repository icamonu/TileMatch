using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{ 
    public static class ObjectPool<T> where T : MonoBehaviour
    {
        private static Dictionary<T, Queue<T>> pools = new Dictionary<T, Queue<T>>();
        private static Dictionary<T, T> originalPrefabs = new Dictionary<T, T>();

        public static T Get(T prefab, Vector3 position = default, Quaternion rotation = default)
        {
            if (!pools.ContainsKey(prefab))
                pools[prefab] = new Queue<T>();

            if (pools[prefab].Count > 0)
            {
                T obj = pools[prefab].Dequeue();
                obj.transform.SetPositionAndRotation(position, rotation);
                obj.gameObject.SetActive(true);
                obj.transform.localScale = Vector3.one;
                return obj;
            }
            else
            {
                T newObj = Object.Instantiate(prefab, position, rotation);
                originalPrefabs[newObj] = prefab;
                return newObj;
            }
        }

        public static void Release(T obj)
        {
            obj.gameObject.SetActive(false);

            T prefab = originalPrefabs[obj];
            if (!pools.ContainsKey(prefab))
                pools[prefab] = new Queue<T>();

            pools[prefab].Enqueue(obj);
        }
    }
}