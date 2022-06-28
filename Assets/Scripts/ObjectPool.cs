using System.Collections.Generic;

using UnityEngine;

namespace FarmerSim
{
    public abstract class ObjectPool<T> where T : PoolableObject
    {
        protected Stack<T> reusableInstances = new Stack<T>();

        protected T prefabAsset;

        protected Transform objectsParent;

        protected int objectsCount = 30;

        public ObjectPool(Transform objectsParent, int objectsCount)
        {
            this.objectsParent = objectsParent;
            this.objectsCount = objectsCount;

            SetPrefabAsset();

            for (int i = 0; i < objectsCount; i++)
            {
                T objectInstance = Object.Instantiate(prefabAsset, objectsParent);
                objectInstance.Realized += (t) => { ReturnToPool((T) t); };
                objectInstance.gameObject.SetActive(false);
                reusableInstances.Push(objectInstance);
            }
        }

        protected abstract void SetPrefabAsset();

        public void ReturnToPool(T instance)
        {
            instance.gameObject.SetActive(false);

            reusableInstances.Push(instance);
        }

        public T GetObjectFromPool()
        {
            T retComp;
            if (reusableInstances.Count > 0)
            {
                retComp = reusableInstances.Pop();
                retComp.gameObject.SetActive(true);
            }
            else
                retComp = Object.Instantiate(prefabAsset);

            return retComp;
        }
    }
}