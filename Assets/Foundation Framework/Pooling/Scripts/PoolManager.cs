
using UnityEngine;

namespace FoundationFramework.Pools
{
    public class PoolManager : Singleton<PoolManager>
    {
        private Pool _pool;

        protected override void Awake()
        {
            _pool = gameObject.AddComponent<Pool>();

            base.Awake();
        }

        public static void PopulatePool(GameObject prefab, int size)
        {
            Instance._pool.PopulatePool(prefab, size);
        }

        public static GameObject Spawn(GameObject prefab)
        {
            return Instance == null ? null : Instance._pool.SpawnObject(prefab);
        }

        public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return Instance == null ? null : Instance._pool.SpawnObject(prefab, position, rotation);
        }
        
        public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation,Transform parent)
        {
            if (Instance == null) return null;
            return  Instance._pool.SpawnObject(prefab, position, rotation, parent);
        }

        public static void Despawn(GameObject clone)
        {
            if (Instance == null) return;
            Instance._pool.DespawnObject(clone);
        }

        public static void Despawn(GameObject clone,float time)
        {
            if (Instance == null) return;
            Timer.Register(time, () => Instance._pool.DespawnObject(clone));
        }
    }
}

