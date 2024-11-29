namespace Services.EventService
{
    public class PlayerHealthUpdateViewEvent : IViewEvent
    {
        public float FillPercentage { get; private set; }
        public string HealthText { get; private set; }

        public ViewEventType ViewEventType => ViewEventType.PlayerHealth;

        public PlayerHealthUpdateViewEvent(float fillPercentage,string healthText )
        {
            FillPercentage = fillPercentage;
            HealthText = healthText;
        }

        public PlayerHealthUpdateViewEvent GetPlayerHealthUpdateViewEvent()
        {
            return this;
        }

        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }
    
    public partial interface IViewEvent : IEvent
    {
        virtual PlayerHealthUpdateViewEvent GetPlayerHealthUpdateViewEvent()
        {
            return null;
        }
    }
}