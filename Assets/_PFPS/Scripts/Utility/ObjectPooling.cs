using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using MEC;

namespace HeavenFalls
{
    public class ObjectPooling<T> where T : MonoBehaviour
    {
        private readonly T _object;
        private readonly IObjectPool<T> _pool;

        public ObjectPooling(T prefab, int defaultSize = 30, int maxSize = 100)
        {
            _object = prefab;

            _pool = new ObjectPool<T>(
                CreateObjectPool,
                OnGetObjectPool,
                OnReturnObjectPool,
                OnDestroyObjectPool,
                true,
                defaultSize,
                maxSize
            );
        }
        
        public T GetObject
        {
            get
            {
                var obj = _pool.Get();
                return obj;
            }
        }

        public void ReleaseObject(T objPooled)
        {
            _pool.Release(objPooled);
        }

        private T CreateObjectPool()
        {
            var newObjPool = Object.Instantiate(_object);
            return newObjPool;
        }

        private void OnGetObjectPool(T poolObj)
        {
            poolObj.gameObject.SetActive(true);
        }

        private void OnReturnObjectPool(T poolObj)
        {
            poolObj.gameObject.SetActive(false);
        }

        private void OnDestroyObjectPool(T poolObj)
        {
            Object.Destroy(poolObj);
        }
    }
}
