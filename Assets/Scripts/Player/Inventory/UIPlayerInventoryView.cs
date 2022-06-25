
using FarmerSim.Invnentory;

using System.Collections;

using UnityEngine;
using UnityEngine.UI;

namespace FarmerSim.Player
{
    public class UIPlayerInventoryView : MonoBehaviour, IInventoryView
    {
        private IInventoryModel playerInventoryModel;

        [SerializeField] private TMPro.TextMeshProUGUI packsCountText;
        [SerializeField] private TMPro.TextMeshProUGUI moneyCountText;
        private int lastMoneyCount;
        [SerializeField] private Image moneyImg;
        private bool moneyRoutineRunned;

        public void Initialize(IInventoryModel inventoryModel)
        {
            this.playerInventoryModel = inventoryModel;
            playerInventoryModel.InventoryModelChanged += OnInventoryModelChanged;
            OnInventoryModelChanged();
        }

        private void OnInventoryModelChanged()
        {
            var packsCount = playerInventoryModel.GetItemsCountByType<WheatPackItem>();
            var moneyCount = playerInventoryModel.GetMoneyCount();

            if (moneyCount > lastMoneyCount)
            {
                if (!moneyRoutineRunned)
                    StartCoroutine(nameof(AnimateMoneySprite));
            }

            packsCountText.SetText($"{packsCount}/{playerInventoryModel.GetMaxItemsCount()}");
            moneyCountText.SetText($"{moneyCount}");

            lastMoneyCount = moneyCount;
        }

        private IEnumerator AnimateMoneySprite()
        {
            moneyRoutineRunned = true;
            float time = 0.5f;
            var startSizeDelta = moneyImg.rectTransform.sizeDelta;
            while (time > 0.25f)
            {
                time -= Time.deltaTime;

                moneyImg.rectTransform.sizeDelta = Vector2.MoveTowards(moneyImg.rectTransform.sizeDelta,
                    startSizeDelta * 1.5f, 0.25f);

                yield return null;
            }
            while (time > 0)
            {
                time -= Time.deltaTime;
                moneyImg.rectTransform.sizeDelta = Vector2.MoveTowards(moneyImg.rectTransform.sizeDelta,
                   startSizeDelta, 0.25f);

                yield return null;
            }
            moneyRoutineRunned = false;
        }

        private void OnDestroy()
        {
            playerInventoryModel.InventoryModelChanged -= OnInventoryModelChanged;
        }
    }
}