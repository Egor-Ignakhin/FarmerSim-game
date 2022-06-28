using UnityEngine;
namespace FarmerSim.Player
{
    public class MoneyPool : ObjectPool<MoneyPoolable>
    {
        public MoneyPool(Transform objectsParent, int objectsCount) :
            base(objectsParent, objectsCount)
        {
        }

        protected override void SetPrefabAsset()
        {
            prefabAsset = Resources.Load<MoneyPoolable>("Money");
        }
    }
}