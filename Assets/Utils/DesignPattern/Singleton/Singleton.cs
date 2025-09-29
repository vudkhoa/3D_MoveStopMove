using UnityEngine;
namespace Utils.DesignPattern.Singleton
{
    public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(gameObject); // Optional: Keep the instance across scenes
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}