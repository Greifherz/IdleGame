using System;

namespace Services.ViewProvider.View
{
    /// <summary>
    /// The "View" in MVP.
    /// This is a contract that the UI components must follow.
    /// It knows how to display things, but not why.
    /// </summary>
    public interface IMiningView
    {
        // Events the view can fire
        event Action OnCollectClicked;
        event Action OnHireClicked;

        // Properties the Presenter can set
        void SetVisibility(bool visible);
        void SetAccumulatedGold(string amount);
        void SetMinerCount(string count);
        void SetHireButtonInteractable(bool isInteractable);
    }
}