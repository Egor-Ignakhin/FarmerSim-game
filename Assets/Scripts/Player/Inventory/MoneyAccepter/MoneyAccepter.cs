using FarmerSim.Player.Inventory;

using System.Collections.Generic;

using UnityEngine;

namespace FarmerSim.Player
{
    public sealed class MoneyAccepter : MonoBehaviour
    {
        [SerializeField] private GameObject defaultPlayerInventaryControllerGM;
        private IPlayerInventoryController defaultPlayerInventaryController;

        [SerializeField] private Transform target;

        private readonly List<(RectTransform obj, int cost)> movedMoneys = new List<(RectTransform obj, int cost)>();

        [SerializeField] private RectTransform moenyView;

        private Vector2 normalMoneySizeDelta;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            defaultPlayerInventaryController = defaultPlayerInventaryControllerGM.GetComponent<IPlayerInventoryController>();

            defaultPlayerInventaryController.OnItemSold += OnItemSold;

            normalMoneySizeDelta = Resources.Load<RectTransform>("Money").sizeDelta;
        }

        private void OnItemSold(int moneyCount)
        {
            var money = CreateMoney();
            money.sizeDelta *= 0.5f;

            money.position = Camera.main.WorldToScreenPoint(target.position);
            movedMoneys.Add((money, moneyCount));
        }

        private RectTransform CreateMoney()
        {
            return Instantiate(Resources.Load<RectTransform>("Money"), transform.parent);
        }

        private void Update()
        {
            if (movedMoneys.Count > 0)
            {
                MoveMoneys();
            }
        }

        private void MoveMoneys()
        {
            for (int i = 0; i < movedMoneys.Count; i++)
            {
                RectTransform moneyObj = movedMoneys[i].obj;

                moneyObj.position = Vector2.MoveTowards(moneyObj.position,
                    moenyView.position, Time.deltaTime * 2000);

                moneyObj.sizeDelta = Vector3.MoveTowards(moneyObj.sizeDelta,
                    normalMoneySizeDelta, Time.deltaTime * 100);

                if (moneyObj.position == moenyView.position)
                {
                    Destroy(moneyObj.gameObject);
                    movedMoneys.RemoveAt(i);
                }
            }
        }

        private void OnDestroy()
        {
            defaultPlayerInventaryController.OnItemSold -= OnItemSold;
        }
    }
}