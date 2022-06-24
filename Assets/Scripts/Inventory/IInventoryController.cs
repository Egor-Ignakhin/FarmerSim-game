namespace FarmerSim.Invnentory
{
    internal interface IInventoryController
    {
        bool IsNotFilled();
        void PushItem(IInventoryItem item);
    }
}