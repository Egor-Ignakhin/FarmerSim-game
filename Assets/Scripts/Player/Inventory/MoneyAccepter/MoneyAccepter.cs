
using UnityEngine;

namespace FarmerSim.Player
{
    public class MoneyAccepter : MonoBehaviour
    {
        [SerializeField] private BoxPlayerInventoryView boxPlayerInventoryView;
        [SerializeField] private Transform target;

        private void Awake()
        {
            boxPlayerInventoryView.OnWheatPackSelled += OnGetMoney;
        }

        private void OnGetMoney(int _)
        {
            var money = CreateMoney();

            money.anchoredPosition = Camera.main.WorldToViewportPoint(target.position);
        }

        private RectTransform CreateMoney()
        {
            return Instantiate(Resources.Load<RectTransform>("Money"), transform.parent);
        }

        private void OnDestroy()
        {
            boxPlayerInventoryView.OnWheatPackSelled -= OnGetMoney;
        }
    }
}