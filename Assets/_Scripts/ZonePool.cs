using UnityEngine;
using UnityEngine.Pool;


namespace _Scripts
{
    public class ZonePool : MonoBehaviour
    {
        [SerializeField] private GameObject zonePrefab;
        public IObjectPool<GameObject> pool;

        private void Awake()
        {
            pool = new ObjectPool<GameObject>(CreateZoneItem, OnTakeZoneFromPool, OnReturnZoneFromPool);
        }

        private GameObject CreateZoneItem()
        {
            var zoneItem = Instantiate(zonePrefab);
            SetPool(pool);
            return zoneItem;
        }

        private void OnTakeZoneFromPool(GameObject zoneObject)
        {
            print($"{gameObject.name} true)");
            zoneObject.SetActive(true);
        }

        private void OnReturnZoneFromPool(GameObject zoneObject)
        {
            print($"{gameObject.name} false)");
            zoneObject.SetActive(false);
        }

        private void SetPool(IObjectPool<GameObject> pool) => this.pool = pool;
    }
}