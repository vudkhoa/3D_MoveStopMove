using UnityEngine;
namespace Utils.DesignPattern.ObjectPooling
{
    public class PooledObject : MonoBehaviour
    {
        private ObjectPool pool;

        public ObjectPool Pool { get => pool; set => pool = value; }

        public void Release()
        {
            pool.ReturnToPool(this);
        }
    }
}