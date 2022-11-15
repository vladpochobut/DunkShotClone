using UnityEngine;

namespace DunkShot.Core.ViewMangers
{
    public class GameplayViewManager : MonoBehaviour
    {
        private Camera _mainCamera;

        private void OnEnable()
        {
            _mainCamera = Camera.main;
        }

        private void Start()
        {
            CalculateScreenParams();
        }

        private void CalculateScreenParams()
        {
            transform.position = new Vector3(_mainCamera.transform.position.x, _mainCamera.transform.position.y, 0);
            var bottomLeft = _mainCamera.ViewportToWorldPoint(Vector3.zero) * 100;
            var topRight = _mainCamera.ViewportToWorldPoint(new Vector3(_mainCamera.rect.width, _mainCamera.rect.height)) * 100;
            var screenSize = topRight - bottomLeft;
            var screenRatio = screenSize.x / screenSize.y;
            var desiredRatio = transform.localScale.x / transform.localScale.y;
            if (screenRatio > desiredRatio)
            {
                var height = screenSize.y;
                transform.localScale = new Vector3(height * desiredRatio, height);
            }
            else
            {
                var width = screenSize.x;
                transform.localScale = new Vector3(width, width / desiredRatio);
            }
        }
    }
}