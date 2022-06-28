
using UnityEngine;

namespace FarmerSim.Mud
{
    public class MudWheatPackPool : ObjectPool<WheatPackPoolable>
    {
        public MudWheatPackPool(Transform objectsParent, int objectsCount) : base(objectsParent, objectsCount)
        {
        }

        protected override void SetPrefabAsset()
        {
            prefabAsset = Resources.Load<WheatPackPoolable>("InteractableWheatPack");
        }
    }
}