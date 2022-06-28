using UnityEngine;

namespace FarmerSim.Mud
{
    public class SlicedGrassPool : ObjectPool<SlicedGrassPoolable>
    {
        public SlicedGrassPool(Transform objectsParent, int objectsCount) : base(objectsParent, objectsCount)
        {
        }

        protected override void SetPrefabAsset()
        {
            prefabAsset = Resources.Load<SlicedGrassPoolable>("SlicedGrass");
        }
    }
}