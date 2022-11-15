using DunkShot.Core.Basket;
using UnityEngine;

namespace DunkShot.Core.CameraStuff
{
    public class CameraFollow : MonoBehaviour
    {
        [Header(" Elements ")]
        [SerializeField]
        private GameObject target;
        [Header(" Settings ")]
        [SerializeField]
        private float _cameraOffsetY;

        private bool _cameraIsMove = false;

        private void OnEnable()
        {
            BasketController.onGetIntoBasket += SetCameraToMove;    
        }

        void Update()
        {
            if (!_cameraIsMove)
                return;
            SmoothCameraMove();
        }

        private void SetCameraToMove() 
        {
            _cameraIsMove = true;
        }

        private void SmoothCameraMove()
        {
            transform.position = new Vector3(transform.position.x,
                                                        Mathf.Lerp(transform.position.y,
                                                        target.transform.position.y + _cameraOffsetY, 0.02f),
                                                        transform.position.z);

            if (transform.position.y >= target.transform.position.y + _cameraOffsetY - 0.5f)
            {
                _cameraIsMove = false;
            }
        }

        private void OnDisable()
        {
            BasketController.onGetIntoBasket -= SetCameraToMove;
        }
    }
}