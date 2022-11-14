using System;
using System.Collections;
using UnityEngine;

namespace DunkShot.Core.Ball
{
    public class BallController : MonoBehaviour
    {
        [Header(" Settings ")]
        [SerializeField]
        private float _power;

        private Vector3 _startPosition;
        private Rigidbody2D _currentRigidbody2D;
        private Collider2D _currentCollider2D;
        public Vector3 StartPosition => _startPosition;
        public float Power => _power;

        private void OnEnable()
        {
            BallTrajectory.onStartShot += Shot;
        }

        void Start()
        {
            _currentCollider2D = GetComponent<Collider2D>();
            _currentRigidbody2D = GetComponent<Rigidbody2D>();
            ConfigureStartBallSettings();
        }

        public void SetStartBallPosition(Vector3 position)
        {
            _startPosition = position;
        }

        public Vector2 GetVelosity(Vector3 position)
        {
            return GetForce(position) * Time.fixedDeltaTime / _currentRigidbody2D.mass;
        }
    
        private void Shot(Vector3 position)
        {
            _currentRigidbody2D.isKinematic = false;
            _currentCollider2D.enabled = true;
            _currentRigidbody2D.AddForce(GetForce(position));
        }

        public void ConfigureStartBallSettings()
        {
            _currentRigidbody2D.isKinematic = true;
            _currentRigidbody2D.velocity = Vector3.zero;
            _startPosition = transform.position;
        }

        private Vector2 GetForce(Vector3 mouse)
        {
            return (new Vector2(_startPosition.x, _startPosition.y) - new Vector2(mouse.x, mouse.y)) * _power;
        }

        private void OnBecameInvisible()
        {
            GameManager.Instance.SetGameState(GameManager.GAME_STATE.GameOver);
        }

        private void OnDisable()
        {
            BallTrajectory.onStartShot -= Shot;
        }
    }
}