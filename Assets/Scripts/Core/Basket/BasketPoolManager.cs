using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DunkShot.Core.Basket
{
    public class BasketPoolManager : MonoBehaviour
    {
        public static BasketPoolManager Instance { get; private set; }

        [Header(" Elements ")]
        [SerializeField]
        private List<GameObject> _pooledObjects;
        [SerializeField]
        private GameObject _objectToPool;
        [SerializeField]
        private Transform _basketsParentTransform;
        [Header(" Settings ")]
        [SerializeField]
        private int _countPoolObjects;

        private GameObject _currentBasket;
        public GameObject CurrentBasket => _currentBasket;

        void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
        }

        private void Start()
        {
            GeneratePoolElements();
            GetPooledObject().SetActive(true);
        }

        private void GeneratePoolElements()
        {
            _pooledObjects = new List<GameObject>();
            for (int i = 0; i < _countPoolObjects; i++)
            {
                GameObject obj = Instantiate(_objectToPool);
                obj.GetComponentInChildren<BasketController>().BasketID = i;
                obj.transform.parent = _basketsParentTransform;
                obj.SetActive(false);
                _pooledObjects.Add(obj);
            }
        }

        public GameObject GetPooledObject()
        {
            var nextCandidate = _pooledObjects.Where(x => !x.activeInHierarchy).FirstOrDefault();
            if (nextCandidate != null)
            {
                _currentBasket = nextCandidate;
                return _currentBasket;
            }
            else
            {
                _currentBasket = _pooledObjects.Where(x => x != _currentBasket).FirstOrDefault();
                return _currentBasket;   
            }
        }
    }
}