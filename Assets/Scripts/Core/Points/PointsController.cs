using DunkShot.Core.Basket;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Core.Points
{
    public class PointsController : MonoBehaviour
    {
        public static PointsController Instance;
        private const string StarKey = "Star";
        [Header(" Elements ")]
        [SerializeField]
        private Text _starsCountText;
        [SerializeField]
        private Text _pointsCountText;

        private int _currentPoints= 0;
        private void OnEnable()
        {
            StarHandler.onStarCollect += AddStar;
            BasketController.onGetIntoBasket += AddPoint;
        }

        private void Start()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
        }

        private void AddPoint()
        {
            _currentPoints += 1;
            _pointsCountText.text = _currentPoints.ToString();
        }

        private void AddStar()
        {
            PlayerPrefs.SetInt(StarKey, PlayerPrefs.GetInt(StarKey, 0) + 1);
            _starsCountText.text = GetStarsCount().ToString();
        }

        public int GetStarsCount()
        {
            return PlayerPrefs.GetInt(StarKey, 0);
        }

        private void OnDisable()
        {
            StarHandler.onStarCollect -= AddStar;
            BasketController.onGetIntoBasket -= AddPoint;
        }
    }
}