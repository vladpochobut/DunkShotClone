using DunkShot.Core.Ball;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DunkShot.Core.Basket
{
    [RequireComponent(typeof(Animator))]
    public class BasketController : MonoBehaviour
    {
        /// <summary>
        /// event when hit in a basket
        /// </summary>
        /// <param name="Vector2"></param>current ball position
        public static event Action<Vector2> onGetIntoBasketPosition;
        /// <summary>
        ///  event when hit in a basket
        /// </summary>
        public static event Action onGetIntoBasket;
        [Header(" Elements ")]
        [SerializeField]
        private Collider2D _triggerCollider;
        [SerializeField]
        private GameObject _star;
        [SerializeField]
        private BasketSO _basketSOType;
        [SerializeField]
        private List<BoxCollider2D> _hoopColliders;
        [Header(" Settings ")]
        [SerializeField]
        private int _starSpawnChance = 20;
        public Collider2D TriggerCollider => _triggerCollider;

        private int _basketID;
        private Animator _animator;
        private Camera _mainCamera;
        private static GameObject _currentBasket;

        private const string NetTag = "Net";
        private const string NetPowerKey = "Power";

        private void OnEnable()
        {
            StarSpawn();
            _animator = GetComponent<Animator>();
            _mainCamera = Camera.main;
            BallTrajectory.onShot += DisableColledersForShot;
            BallTrajectory.onAiming += BasketAiming;
            BallTrajectory.onCollision += OnAccurateShotIDontKnowAboutNameOfThisMethod;
        }

        private void Start()
        {
            _animator.runtimeAnimatorController = _basketSOType?.GetCurrentAnimatorController();
        }

        private void StarSpawn()
        {
            if (_star != null)
            {
                var chanceValue = UnityEngine.Random.Range(1, 100);
                Debug.Log("Chance->" + chanceValue);
                if (chanceValue < _starSpawnChance)
                    _star.SetActive(true);
            }
        }

        public int BasketID
        {
            get{ return _basketID; }
            set { _basketID = value; }
        }

        public void SetColliderActive(bool activeState)
        {
            _hoopColliders.ForEach(x => x.enabled = activeState);
            _triggerCollider.enabled = activeState;
        }

        private void BasketAiming(Vector3 lookAtPosition, Vector3 ballAbsPosition)
        {
            if (_currentBasket == null || _currentBasket != _triggerCollider.gameObject)
                return;
            AnimateBasketRotation(lookAtPosition);
            AnimateNetPower(lookAtPosition,ballAbsPosition);
        }

        private void AnimateBasketRotation(Vector3 lookAtPosition)
        {
            var currentPos = GetComponentInParent<Transform>().position;
            var direction = lookAtPosition - currentPos;
            transform.up = new Vector3(direction.x, direction.y, 0f);
        }

        private void AnimateNetPower(Vector3 targetPoint,Vector3 currentAbsPosition)
        {
            _triggerCollider.GetComponent<Animator>()
                    .SetFloat(NetPowerKey, Mathf.Min(_mainCamera
                        .WorldToViewportPoint(targetPoint - currentAbsPosition).y, 1f));
        }

        private void DisableColledersForShot()
        {
            if (_currentBasket == null || _currentBasket != _triggerCollider.gameObject)
                return;
            StartCoroutine(ShotNetAnimation());
        }

        private IEnumerator ShotNetAnimation()
        {
            _triggerCollider.GetComponent<Animator>().SetFloat(NetPowerKey, 0f);
            //_triggerCollider.GetComponent<Animator>().Update(0f);
            SetColliderActive(false);
            yield return new WaitForSeconds(1f);
            SetColliderActive(true);
        }

        private void OnAccurateShotIDontKnowAboutNameOfThisMethod(GameObject ball, GameObject basket)
        {
            // collisions only with Net are considered as shots on basket
            if (basket.tag != NetTag) return;
            var currentBasketId = BasketPoolManager.Instance.CurrentBasket.GetComponentInChildren<BasketController>().BasketID;
            if (basket != _currentBasket 
                && (_currentBasket == null 
                || _currentBasket.GetComponentInParent<BasketController>().BasketID 
                != currentBasketId))
            {
                _currentBasket = basket;
                onGetIntoBasketPosition?.Invoke(ball.transform.position);
                onGetIntoBasket?.Invoke();
                StarSpawn();
            }
        }

        private void OnDisable()
        {
            BallTrajectory.onShot -= DisableColledersForShot;
            BallTrajectory.onAiming -= BasketAiming;
            BallTrajectory.onCollision -= OnAccurateShotIDontKnowAboutNameOfThisMethod;
        }
    }
}