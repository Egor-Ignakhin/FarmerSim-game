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

        private readonly List<MoneyPoolable> movedMoneys = new List<MoneyPoolable>();

        [SerializeField] private RectTransform moneyView;

        private RectTransform moneyInstance;

        private ObjectPool<MoneyPoolable> moneyPool;

        [SerializeField] private int moneysCount = 30;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            defaultPlayerInventaryController = defaultPlayerInventaryControllerGM.GetComponent<IPlayerInventoryController>();

            defaultPlayerInventaryController.OnItemSold += OnItemSold;

            moneyPool = new MoneyPool(transform, moneysCount);

            moneyInstance = Resources.Load<RectTransform>("Money");
        }

        private void OnItemSold(int _)
        {
            MoneyPoolable moneyPoolable = moneyPool.GetObjectFromPool();
            RectTransform moneyRT = moneyPoolable.transform as RectTransform;
            moneyRT.sizeDelta *= 0.5f;

            moneyRT.position = Camera.main.WorldToScreenPoint(target.position);
            movedMoneys.Add(moneyPoolable);
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
                RectTransform moneyObj = movedMoneys[i].GetRectTransform();

                moneyObj.position = Vector2.MoveTowards(moneyObj.position,
                    moneyView.position, Time.deltaTime * 2000);

                moneyObj.sizeDelta = Vector3.MoveTowards(moneyObj.sizeDelta,
                    moneyInstance.sizeDelta, Time.deltaTime * 100);

                if (moneyObj.position == moneyView.position)
                {
                    moneyPool.ReturnToPool(movedMoneys[i]);
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