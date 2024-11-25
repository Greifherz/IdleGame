using ServiceLocator;
using Services.EventService;
using Services.TickService;
using UnityEngine;

//Mono context classes are classes that don't do much. They hold references to the scene gameObjects and other view-related scripts and helpers.
//They are the touching point between logic and view
public class GameplayStateMonoContext : MonoBehaviour
{
    private IEventService _eventService;

    [SerializeField] private IdleItem[] IdleItems;
    
    void Start()
    {
        _eventService = Locator.Current.Get<IEventService>();
    }

    void TransitionBack()
    {
        _eventService.Raise(new TransitionEvent(TransitionTarget.Back));
    }
}
