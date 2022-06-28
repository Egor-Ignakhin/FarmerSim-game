using UnityEngine;

namespace FarmerSim.Player
{
    public class MoneyPoolable : PoolableObject
    {
        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public RectTransform GetRectTransform()
        {
            return rectTransform;
        }
    }
}