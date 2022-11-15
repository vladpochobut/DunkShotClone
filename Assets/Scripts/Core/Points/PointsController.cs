using DunkShot.Core.Basket;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DunkShot.Core.Points
{
    public class PointsController : MonoBehaviour
    {
        public static PointsController Instance;
        private const string StarKey = "Star";
        [Header(" Elements ")]
        [SerializeField]
        private List<Text> _starsCountText;
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

            UpdateStarsScore();
        }

        public void UpdateStarsScore()
        {
            _starsCountText.ForEach(x => x.text = GetStarsCount().ToString());
        }

        private void AddPoint()
        {
            _currentPoints += 1;
            _pointsCountText.text = _currentPoints.ToString();
        }

        private void AddStar()
        {
            PlayerPrefs.SetInt(StarKey, PlayerPrefs.GetInt(StarKey, 0) + 1);
            _starsCountText.ForEach(x=>x.text = GetStarsCount().ToString());
        }

        public void UseStars(int price) 
        {
            PlayerPrefs.SetInt(StarKey, PlayerPrefs.GetInt(StarKey, 0) - 1);
            _starsCountText.ForEach(x => x.text = GetStarsCount().ToString());
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