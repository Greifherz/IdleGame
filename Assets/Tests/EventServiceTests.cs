using NUnit.Framework;
using ServiceLocator;
using Services.EventService;
using Services.TickService;

namespace Tests
{
    public class EventServiceTests
    {
        private void InitializeEssentials()
        {
            Locator.Initialize();
            var TickService = new AsyncTickService();
            Locator.Current.Register<ITickService>(TickService);
        }

        [Test]
        public void VisitorPipelineEventService_CommonEvents_CommonPipeline_Functionality()
        {
            InitializeEssentials();

            var EventService = new VisitorPipelinesEventService();
            EventService.Initialize();

            var ReceivedEvent = false;

            var CommonEventHandler = new CommonEventHandler((commonEvent) => { ReceivedEvent = true; });
            EventService.RegisterListener(CommonEventHandler);

            EventService.Raise(new CommonEvent());
            Locator.Current.Get<ITickService>().ManualClock();

            Assert.IsTrue(ReceivedEvent);
        }

        [Test]
        public void VisitorPipelineEventService_CommonEvents_ViewPipeline_Functionality()
        {
            InitializeEssentials();

            var EventService = new VisitorPipelinesEventService();
            EventService.Initialize();

            var ReceivedEvent = false;

            var CommonEventHandler = new CommonEventHandler((commonEvent) => { ReceivedEvent = true; });
            EventService.RegisterListener(CommonEventHandler, EventPipelineType.ViewPipeline);

            EventService.Raise(new CommonEvent(), EventPipelineType.ViewPipeline);
            Locator.Current.Get<ITickService>().ManualClock();

            Assert.IsTrue(ReceivedEvent);
        }

        [Test]
        public void VisitorPipelineEventService_CommonEvents_GameplayPipeline_Functionality()
        {
            InitializeEssentials();

            var EventService = new VisitorPipelinesEventService();
            EventService.Initialize();

            var ReceivedEvent = false;

            var CommonEventHandler = new CommonEventHandler((commonEvent) => { ReceivedEvent = true; });
            EventService.RegisterListener(CommonEventHandler, EventPipelineType.GameplayPipeline);

            EventService.Raise(new CommonEvent(), EventPipelineType.GameplayPipeline);
            Locator.Current.Get<ITickService>().ManualClock();

            Assert.IsTrue(ReceivedEvent);
        }

        [Test]
        public void VisitorPipelineEventService_CommonEvents_ServicePipeline_Functionality()
        {
            InitializeEssentials();

            var EventService = new VisitorPipelinesEventService();
            EventService.Initialize();

            var ReceivedEvent = false;

            var CommonEventHandler = new CommonEventHandler((commonEvent) => { ReceivedEvent = true; });
            EventService.RegisterListener(CommonEventHandler, EventPipelineType.ServicesPipeline);

            EventService.Raise(new CommonEvent(), EventPipelineType.ServicesPipeline);
            Locator.Current.Get<ITickService>().ManualClock();

            Assert.IsTrue(ReceivedEvent);
        }

        [Test]
        public void VisitorPipelineEventService_CommonEvents_CommonPipeline_WrongPipeline_Functionality()
        {
            InitializeEssentials();

            var EventService = new VisitorPipelinesEventService();
            EventService.Initialize();

            var ReceivedEvent = false;

            var CommonEventHandler = new CommonEventHandler((commonEvent) => { ReceivedEvent = true; });
            EventService.RegisterListener(CommonEventHandler);

            EventService.Raise(new CommonEvent(), EventPipelineType.GameplayPipeline);
            Locator.Current.Get<ITickService>().ManualClock();

            Assert.IsFalse(ReceivedEvent);
        }

        [Test]
        public void VisitorPipelineEventService_Only_Listens_One_Pipeline()
        {
            InitializeEssentials();

            var EventService = new VisitorPipelinesEventService();
            EventService.Initialize();

            var ReceivedEventCount = 0;

            var CommonEventHandler = new CommonEventHandler((commonEvent) => { ReceivedEventCount++; });
            EventService.RegisterListener(CommonEventHandler, EventPipelineType.CommonPipeline);
            EventService.RegisterListener(CommonEventHandler, EventPipelineType.GameplayPipeline);
            EventService.RegisterListener(CommonEventHandler, EventPipelineType.ServicesPipeline);
            EventService.RegisterListener(CommonEventHandler, EventPipelineType.ViewPipeline);

            EventService.Raise(new CommonEvent(), EventPipelineType.CommonPipeline);
            Locator.Current.Get<ITickService>().ManualClock();

            Assert.AreEqual(ReceivedEventCount, 1);
        }

        [Test]
        public void VisitorPipelineEventService_Can_Listen_Several_Times_Same_Pipeline()
        {
            InitializeEssentials();

            var EventService = new VisitorPipelinesEventService();
            EventService.Initialize();

            var ReceivedEventCount = 0;

            var CommonEventHandler = new CommonEventHandler((commonEvent) => { ReceivedEventCount++; });
            EventService.RegisterListener(CommonEventHandler);
            EventService.RegisterListener(CommonEventHandler);
            EventService.RegisterListener(CommonEventHandler);

            EventService.Raise(new CommonEvent());
            Locator.Current.Get<ITickService>().ManualClock();

            Assert.AreEqual(ReceivedEventCount, 3);
        }

        [Test]
        public void GeneralPipeline_Functionality()
        {
            InitializeEssentials();

            var EventService = new GeneralEventService();
            EventService.Initialize();

            var ReceivedEvent = false;

            //General should work with just any class
            EventService.RegisterGeneralListener<EventServiceTests>((tests) => { ReceivedEvent = true; });

            EventService.RaiseGeneralEvent(this);

            Assert.IsTrue(ReceivedEvent);
        }

        [Test]
        public void GeneralPipeline_Only_Notified_Of_Listened_Class_Functionality()
        {
            InitializeEssentials();

            var EventService = new GeneralEventService();
            EventService.Initialize();

            var ReceivedEvent = false;

            //General should work with just any class
            EventService.RegisterGeneralListener<EventServiceTests>((tests) => { ReceivedEvent = true; });

            EventService.RaiseGeneralEvent(EventService);

            Assert.IsFalse(ReceivedEvent);
        }
    }
}