using System.Collections;
using UnityEngine;

namespace DunkShot.Core.ViewMangers
{
    public class BorderManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _leftButtonBorder;
        [SerializeField]
        private GameObject _rightTopBorder;

        private void Start()
        {
            SetupBorders();
        }

        private void SetupBorders()
        {
            var point = new Vector3();

            point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,Camera.main.nearClipPlane));
            _rightTopBorder.transform.position = point;

            point = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            _leftButtonBorder.transform.position = point;
        }
    }
}