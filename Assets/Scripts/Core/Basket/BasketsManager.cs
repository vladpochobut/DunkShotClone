using DunkShot.Core.Ball;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DunkShot.Core.Basket
{
    public class BasketsManager : MonoBehaviour
    {
        private Camera _mainCamera;
        private int _basketCounter = 0;
        private void OnEnable()
        {
            _mainCamera = Camera.main;
            BasketController.onGetIntoBasketPosition += CreateNewBasket;
        }

        private void CreateNewBasket(Vector2 position)
        {
            var basket = BasketPoolManager.Instance.GetPooledObject();

            if (basket != null)
            {
                switch (_basketCounter)
                {
                    case 0:
                            basket.transform.position = new Vector2(Random.Range(
                            _mainCamera.ViewportToWorldPoint(new Vector3(0.1f, 0)).x,
                            _mainCamera.ViewportToWorldPoint(new Vector3(0.4f, 0)).x),
                                position.y + Mathf.Abs(Random.Range(
                                    _mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.2f)).y - _mainCamera.ViewportToWorldPoint(Vector3.zero).y,
                                    _mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.3f)).y - _mainCamera.ViewportToWorldPoint(Vector3.zero).y)
                                    ));
                        basket.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-10f,-30f));
                        basket.SetActive(true);
                        _basketCounter++;
                        break;
                    case 1:
                        basket.transform.position = new Vector2(Random.Range(
                            _mainCamera.ViewportToWorldPoint(new Vector3(0.9f, 0)).x,
                            _mainCamera.ViewportToWorldPoint(new Vector3(0.6f, 0)).x),
                                position.y + Mathf.Abs(Random.Range(
                                    _mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.2f)).y - _mainCamera.ViewportToWorldPoint(Vector3.zero).y,
                                    _mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.3f)).y - _mainCamera.ViewportToWorldPoint(Vector3.zero).y)
                                    ));
                        basket.transform.rotation = Quaternion.Euler(0, 0, Random.Range(10f, 30f));
                        basket.SetActive(true);
                        _basketCounter = 0;
                        break;
                }
            }
        }

        private void OnDisable()
        {
            BasketController.onGetIntoBasketPosition -= CreateNewBasket;
        }
    }
}