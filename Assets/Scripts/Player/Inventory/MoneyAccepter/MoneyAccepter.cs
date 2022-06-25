
using FarmerSim.Invnentory;

using System.Collections.Generic;

using UnityEngine;

namespace FarmerSim.Player
{
    public class MoneyAccepter : MonoBehaviour
    {
        [SerializeField] private BoxPlayerInventoryView boxPlayerInventoryView;
        [SerializeField] private Transform target;
        private List<(RectTransform obj, int cost)> movedMoneys = new List<(RectTransform obj, int cost)>();
        [SerializeField] private RectTransform moenyView;

        [SerializeField] private GameObject pPlayerInventoryControllerGM;
        private IInventoryController pPlayerInventoryController;
        private Vector2 normalMoneySizeDelta;

        private void Awake()
        {
            pPlayerInventoryController = pPlayerInventoryControllerGM.GetComponent<IInventoryController>();

            boxPlayerInventoryView.OnWheatPackSelled += OnWheatPackSelled;

            normalMoneySizeDelta = Resources.Load<RectTransform>("Money").sizeDelta;
        }

        private void OnWheatPackSelled(int moneyCount)
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
                    pPlayerInventoryController.SellItem<WheatPackItem>(movedMoneys[i].cost);
                    Destroy(moneyObj.gameObject);
                    movedMoneys.RemoveAt(i);
                }
            }
        }

        private void OnDestroy()
        {
            boxPlayerInventoryView.OnWheatPackSelled -= OnWheatPackSelled;
        }
    }
}