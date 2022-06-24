namespace FarmerSim.Invnentory
{
    public interface IInventoryController
    {
        bool IsNotFilled();
        void PushItem(IInventoryItem item);
        bool HaveItems<T>();
    }
}