using UnityEditor.Animations;
using UnityEngine;

namespace DunkShot.Core.Basket
{
    [CreateAssetMenu(fileName = "Basket", menuName = "Scriptable Objects/Basket", order = 0)]
    public class BasketSO : ScriptableObject
    {
        [SerializeField]
        private AnimatorController _movementAnimation;
        [SerializeField]
        private int _moveBonus;
        [SerializeField]
        private MOVE_TYPE _movementType;

        public AnimatorController GetCurrentAnimatorController()
        {
            return _movementAnimation;
        }

        public MOVE_TYPE GetMoveType()
        {
            return _movementType;
        }
    }
    public enum MOVE_TYPE
    {
        HORIZONTAL_LEFT,
        HORIZONTAL_RIGHT,
        VERTIVAL
    }
}
