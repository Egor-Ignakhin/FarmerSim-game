namespace FarmerSim.Invnentory
{
    public interface IInventoryController
    {
        bool IsNotFilled();
        
        void PushItem(IInventoryItem item);
        
        bool HaveItems<T>();

        void SellItem<T>(int moneyCount);
    }
}