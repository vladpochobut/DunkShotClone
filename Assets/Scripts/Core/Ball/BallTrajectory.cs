using DunkShot.Core.Basket;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DunkShot.Core.Ball
{
    [RequireComponent(
        typeof(Rigidbody2D), 
        typeof(Collider2D),
        typeof(BallController))]
    public class BallTrajectory : MonoBehaviour
    {
        /// <summary>
        /// event on start shot when ball collide with basket  
        /// </summary>
        public static event Action<Vector3> onStartShot;
        /// <summary>
        /// Event on shot
        /// </summary>
        public static event Action onShot;
        /// <summary>
        /// event on aim when ball collide with basket  
        /// </summary>
        public static event Action<Vector3,Vector3> onAiming;
        /// <summary>
        /// Event on aim when ball collide with anything. First gameObject is a ball, the second is an obstacle the ball has collided with
        /// </summary>
        public static event Action<GameObject, GameObject> onCollision;

        [Header(" Elements ")]
        [SerializeField]
        private GameObject _dotPrefab;
        [SerializeField]
        private Transform _dotsParentTransform;
        private BallController _ballController;
        [Header(" Settings ")]
        [SerializeField]
        private float _sensitivity;
        [SerializeField]
        private int _dotsCount;

        private bool _aiming = false;
        private bool _isShot = false;
        private List<GameObject> _dotsList = new List<GameObject>();
        private Vector3 _currentAbsPosition = new Vector3();
        private const string NetTag = "Net";

        void Start()
        {
            _ballController = GetComponent<BallController>();
            CreateDots();
        }

        private void CreateDots()
        {
            for (int i = 0; i < _dotsCount; i++)
            {
                GameObject dot = Instantiate(_dotPrefab);
                dot.transform.parent = _dotsParentTransform;
                _dotsList.Add(dot);
            }
            _dotsParentTransform.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!_isShot)
            {
                Aim();
            }
        }

        private void Aim()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Touchscreen.current.primaryTouch.isInProgress)
                {
                    if (!_aiming)
                    {
                        _aiming = true;
                        _ballController.SetStartBallPosition(Touchscreen.current.primaryTouch.position.ReadValue());
                        CalculatePath();
                        ShowPath();
                    }
                    else
                    {
                        CalculatePath();
                    }
                }
                else if (_aiming)
                {
                    onStartShot?.Invoke(Touchscreen.current.primaryTouch.position.ReadValue());
                    _isShot = true;
                    _aiming = false;
                    HidePath();
                    onShot?.Invoke();
                }
            }
        }

        private void HidePath()
        {
            _dotsParentTransform.gameObject.SetActive(false);
        }

        private void CalculatePath()
        {
            _dotsParentTransform.gameObject.SetActive(true);
            var velosity = _ballController.GetVelosity(Touchscreen.current.primaryTouch.position.ReadValue());
            _currentAbsPosition.y = Mathf.Abs(transform.position.y);
            for (int i = 0; i < _dotsCount; i++)
            {
                float t = i / 30f;
                var point = (Vector3)PathPoint(transform.position, velosity, t);
                point.z = -1.0f;
                _dotsList[i].transform.position = point;
            }

            onAiming(_dotsList[_dotsCount - 1].gameObject.transform.position,_currentAbsPosition);
        }

        private Vector2 PathPoint(Vector2 startP, Vector2 startVel, float t)
        {
            return startP + startVel * t + 0.5f * Physics2D.gravity * t * t;
        }

        private void ShowPath()
        {
            _dotsParentTransform.gameObject.SetActive(true);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == NetTag)
            {
                _isShot = false;
                _ballController.ConfigureStartBallSettings();
            }
            onCollision(_ballController.gameObject, collision.gameObject);
        }
    }
}
