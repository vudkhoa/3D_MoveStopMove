using System.Collections.Generic;
using UnityEngine;

namespace Utils.DesignPattern.ObjectPooling
{
    public class ObjectPool : MonoBehaviour
    {
        [Header("Object Pool Setting")]
        [SerializeField] private int initialPoolSize;
        [SerializeField] private PooledObject ObjectToPool;

        private Stack<PooledObject> stack;

        private void Awake()
        {
            this.SetupPool();
        }

        private void SetupPool()
        {
            // Initialize the stack
            if (this.ObjectToPool == null)
            {
                Debug.LogError("ObjectToPool is not assigned.");
                return;
            }
            this.stack = new Stack<PooledObject>();

            for (int i = 0; i < this.initialPoolSize; ++i)
            {
                PooledObject obj = Instantiate(this.ObjectToPool, transform);
                obj.gameObject.SetActive(false);
                obj.Pool = this;

                this.stack.Push(obj);
            }
        }

        public PooledObject GetPooledObject()
        {
            if (this.ObjectToPool == null)
            {
                Debug.LogError("ObjectToPool is not assigned.");
                return null;
            }

            PooledObject obj = null;
            if (this.stack.Count <= 0)
            {
                obj = Instantiate(this.ObjectToPool, transform);
                obj.gameObject.SetActive(true);
                obj.Pool = this;

                return obj;
            }

            obj = this.stack.Pop();
            //obj.gameObject.SetActive(true);
            return obj; 
        }

        public void ReturnToPool(PooledObject obj)
        {
            if (obj == null)
            {
                Debug.LogError("Returned object is null.");
                return;
            }
            obj.gameObject.SetActive(false);
            this.stack.Push(obj);
        }

    }
}