using System;

namespace Services.ViewProvider.View
{
    public interface IMultiArmyView
    {
        event Action<int> OnHireClicked;

        void SetVisibility(bool visible);
        void SetListVisibility(int index,bool visible);
        // Properties the Presenter can set
        void SetUnitAttack(int index,string amount);
        void SetUnitHealth(int index,string amount);
        void SetUnitCost(int index,string amount);
        void SetUnitCount(int index,string amount);
        
        void SetHireButtonInteractable(int index,bool isInteractable);
    }
}