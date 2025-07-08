using System;

namespace Services.ViewProvider.View
{
    /// <summary>
    /// The "View" in MVP.
    /// This is a contract that the UI components must follow.
    /// It knows how to display things, but not why.
    /// </summary>
    public interface IArmyView //This is dummy, only for 1 army unit. TODO - make it for n army units
    {
        event Action OnHireClicked;

        void SetVisibility(bool visible);
        // Properties the Presenter can set
        void SetUnitAttack(string amount);
        void SetUnitHealth(string amount);
        void SetUnitCost(string amount);
        void SetUnitCount(string count);
        
        void SetHireButtonInteractable(bool isInteractable);
    }
}