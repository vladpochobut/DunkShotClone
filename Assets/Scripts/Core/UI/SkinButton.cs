using System;
using UnityEngine;
using UnityEngine.UI;

namespace DunkShot.Core.UI
{
    public class SkinButton : MonoBehaviour
    {
        /// <summary>
        /// event when new skin selected
        /// </summary>
        public static event Action<Sprite> onNewSkinSelect;

        [Header(" Elements ")]
        [SerializeField]
        private Button _thisButton;
        [SerializeField]
        private Image _skinImage;
        [SerializeField]
        private GameObject _lockImage;
        [SerializeField]
        private GameObject _selector;


        private bool _unlocked;

        public void Configure(Sprite skinSprite, bool unlocked)
        {
            _skinImage.sprite = skinSprite;
            _unlocked = unlocked;
            if (unlocked)
                Unlock();
            else
                Lock();
        }

        private void Lock()
        {
            _thisButton.interactable = false;
            _skinImage.gameObject.SetActive(false);
            _lockImage.SetActive(true);
        }

        public void Unlock()
        {
            _thisButton.interactable = true;
            _skinImage.gameObject.SetActive(true);
            _lockImage.SetActive(false);

            _unlocked = true;
        }

        public void Select()
        {
            _selector.SetActive(true);
            onNewSkinSelect?.Invoke(_skinImage.sprite);
        }

        public Button GetButton()
        {
            return _thisButton;
        }

        public void Deselect()
        {
            _selector.SetActive(false);
        }

        public bool IsUnlocked()
        {
            return _unlocked;
        }
    }
}