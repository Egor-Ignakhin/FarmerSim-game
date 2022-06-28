
using UnityEngine;

namespace FarmerSim.Mud
{
    public class SliceVFXPool : ObjectPool<VFXPoolable>
    {
        public SliceVFXPool(Transform objectsParent, int objectsCount) : base(objectsParent, objectsCount)
        {
        }

        protected override void SetPrefabAsset()
        {
            prefabAsset = Resources.Load<VFXPoolable>("mudSliceVFX");
        }
    }
}