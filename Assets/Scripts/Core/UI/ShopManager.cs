using DunkShot.Core.Points;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DunkShot.Core.UI
{
    public class ShopManager : MonoBehaviour
    {
        [Header(" Elements ")]
        [SerializeField]
        private List<SkinButton> _skinButtons;
        [SerializeField]
        private Button _purchaseButton;
        [Header(" Skins ")]
        [SerializeField]
        private List<Sprite> _skins;
        [Header(" Pricing ")]
        [SerializeField]
        private int _skinPrice;
        [SerializeField]
        private Text _priceText;

        private void Awake()
        {
            _priceText.text = _skinPrice.ToString();
        }

        private void Start()
        {
            ConfigureButtons();
        }

        private void ConfigureButtons()
        {
            for (int i = 0; i < _skinButtons.Capacity; i++)
            {
                var unlocked = PlayerPrefs.GetInt("SkinButton" + i) == 1;
                _skinButtons[i].Configure(_skins[i], unlocked);
                int skinIndex = i;
                _skinButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
            }
        }

        public void UnlockSkin(int skinIndex)
        {
            PlayerPrefs.GetInt("SkinButton" + skinIndex, 1);
            _skinButtons[skinIndex].Unlock();
        }

        private void SelectSkin(int skinIndex)
        {
            for (int i = 0; i < _skinButtons.Count; i++)
            {
                if (skinIndex == i)
                    _skinButtons[i].Select();
                else
                    _skinButtons[i].Deselect();
            }
        }

        public void PurchaseSkin()
        {
            var skinButtons = new List<SkinButton>();
            for (int i = 0; i < _skinButtons.Count; i++)
                if (!_skinButtons[i].IsUnlocked())
                    skinButtons.Add(_skinButtons[i]);
            if (skinButtons.Count <= 0)
                return;
            var randomSkinButton = skinButtons[UnityEngine.Random.Range(0, skinButtons.Count)];
            UnlockSkin(randomSkinButton.transform.GetSiblingIndex());
            SelectSkin(randomSkinButton.transform.GetSiblingIndex());

            PointsController.Instance.UseStars(_skinPrice);

            UpdatePurchaseButton();
        }

        public void UpdatePurchaseButton()
        {
            if (PointsController.Instance.GetStarsCount() < _skinPrice)
                _purchaseButton.interactable = false;
            else
                _purchaseButton.interactable = true;
        }
    }
}