using UnityEngine;

namespace FarmerSim.Barn
{
    public class BarnWheatPool : ObjectPool<WheatPackPoolable>
    {
        public BarnWheatPool(Transform objectsParent, int objectsCount) : base(objectsParent, objectsCount)
        {
        }

        protected override void SetPrefabAsset()
        {
            prefabAsset = Resources.Load<WheatPackPoolable>("WheatPack");
        }
    }
}