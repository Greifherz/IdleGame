# IdleGame - Name to be decided
 
This is a boilerplate game project. The game itself isn't important but the underlying code architecture is as it's meant to be reusable with other projects.

It could have some improvements on that at the time of writing as not all project-specific code is whithin the Game folder.

--------- Architecture ----------

The idea started off simple, making it most adherent to SOLID and have everything decoupled so it can easily be ported to other projects and/or extended. There's another project in a private repo with only the ported part but there's only so much I can see in terms of improvement without putting it to use, so the game project is needed so I can see these points of improvement, apply them then port it back to the boilerplate project.

With use and trying to approach a real project the EventService became the main service, providing decoupled communication to and from services to game logic and view. Services can have reference between themselves but must do so through the Service Locator so get all benefits from a DI and help on integration and unit tests with mocked classes in the future if need be. 

So the project's architecture is simple and akin to data-driven architectures. Services are bound to the Service Locator as a DI, including the EventService. Game/project related code uses the services for communications and kept from holding references between themselves and only communicating through events. It's possible to have more than one listener for given event and that's the whole point. To do so you would need to create new events and I've made a Code Generator that automates the creation of new events, since they have a boilerplate. It creates:
- IEvent interface
- Event class, implementing the interface and visitor pattern
- EventHandler, implementing the listening usage of the Event class.
And also updates EventType enum.

When registering the listeners or raising the event, you need to specify in which pipeline, if none is provided the default is the CommonEventPipeline. 

- Event Service
    There are two event services implemented and being used. VisitorPipelineEventService and a GeneralEventService. They are abstracted by their own interface and served into a unified facade ( https://refactoring.guru/design-patterns/facade ). 
    The VisitorPipelineEventService, as in the name, uses the Visitor Design Pattern ( https://refactoring.guru/design-patterns/visitor ) so we don't cast the class in an expensive way, while supporting a lot of flexibility in the events(I've made the Events have flags of type, so aggregated events can exist or decorated events can exist as well). The downside of it is that it's very boilerplatey, every events needs a bit of code for itself and it's handler. The usage of the event flexibility is yet to be seen. It's separated in pipelines so, as the project grows, we don't have a single huge list of events being raised and a lot of listeners that will end up doing nothing with these events. With multiple pipelines we can ensure we keep listeners lean and the events in each of them in check. Future recommended use is to have one pipeline per feature or something, this way communication is also addressed in a way it's easier to follow as it's not generic.
    The GeneralEventService is a lot more simple, but it doesn't support event layering, it's a single pipeline and it's easy to misuse. You can listen for any class as event and raise any object as event. Using it a lot overtime may bloat and cause unexpected collisions on the dictionary and since it's very generic it would be harder to follow. As project grows and more events are fired it would be a bottleneck. Still, having this kind of implementation is worth having, especially if dealing with external libraries (none implemented so far) or modules.
