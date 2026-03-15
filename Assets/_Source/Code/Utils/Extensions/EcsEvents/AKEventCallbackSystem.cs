using System.Collections.Generic;
using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code._AKFramework.AKEvents.Runtime;
using Leopotam.EcsLite;

namespace _Source.Code.Utils.Extensions.EcsEvents
{
    public class AKEventCallbackSystem : AKEcsSystem
    {
        private EcsWorld _world;

        private EcsPool<AKEventCallback> _pool;

        private IAKEventsService _eventsService;

        private readonly Queue<AKEventCallback> _eventToCallbackQueue;
        private readonly AKEvent _akEvent;

        public AKEventCallbackSystem(AKEvent akEvent)
        {
            _akEvent = akEvent;
            _eventToCallbackQueue = new Queue<AKEventCallback>();
        }

        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _pool = _world.GetPool<AKEventCallback>();

            _eventsService = container.Resolve<IAKEventsService>();
            
            _eventsService.AddListener(_akEvent, OnEventCallback);
        }

        public override void Tick(ref IEcsSystems systems)
        {
            while (_eventToCallbackQueue.Count > 0)
            {
                _pool.Add(_world.NewEntity()) = _eventToCallbackQueue.Dequeue();
            }
        }

        private void OnEventCallback(AKEvent akEvent)
        {
            _eventToCallbackQueue.Enqueue(new AKEventCallback(akEvent));
        }
    }
    
    public class AKEventCallbackSystem<T1> : AKEcsSystem
    {
        private EcsWorld _world;

        private EcsPool<AKEventCallback<T1>> _pool;

        private IAKEventsService _eventsService;

        private readonly Queue<AKEventCallback<T1>> _eventToCallbackQueue;
        private readonly AKEvent _akEvent;

        public AKEventCallbackSystem(AKEvent akEvent)
        {
            _akEvent = akEvent;
            _eventToCallbackQueue = new Queue<AKEventCallback<T1>>();
        }

        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _pool = _world.GetPool<AKEventCallback<T1>>();

            _eventsService = container.Resolve<IAKEventsService>();
            
            _eventsService.AddListener<T1>(_akEvent, OnEventCallback);
        }

        public override void Tick(ref IEcsSystems systems)
        {
            while (_eventToCallbackQueue.Count > 0)
            {
                _pool.Add(_world.NewEntity()) = _eventToCallbackQueue.Dequeue();
            }
        }

        private void OnEventCallback(AKEvent akEvent, T1 arg1)
        {
            _eventToCallbackQueue.Enqueue(new AKEventCallback<T1>(akEvent, arg1));
        }
    }
    
    public class AKEventCallbackSystem<T1, T2> : AKEcsSystem
    {
        private EcsWorld _world;

        private EcsPool<AKEventCallback<T1, T2>> _pool;

        private IAKEventsService _eventsService;

        private readonly Queue<AKEventCallback<T1, T2>> _eventToCallbackQueue;
        private readonly AKEvent _akEvent;

        public AKEventCallbackSystem(AKEvent akEvent)
        {
            _akEvent = akEvent;
            _eventToCallbackQueue = new Queue<AKEventCallback<T1, T2>>();
        }

        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _pool = _world.GetPool<AKEventCallback<T1, T2>>();

            _eventsService = container.Resolve<IAKEventsService>();
            
            _eventsService.AddListener<T1, T2>(_akEvent, OnEventCallback);
        }
        
        public override void Tick(ref IEcsSystems systems)
        {
            while (_eventToCallbackQueue.Count > 0)
            {
                _pool.Add(_world.NewEntity()) = _eventToCallbackQueue.Dequeue();
            }
        }

        private void OnEventCallback(AKEvent akEvent, T1 arg1, T2 arg2)
        {
            _eventToCallbackQueue.Enqueue(new AKEventCallback<T1, T2>(akEvent, arg1, arg2));
        }
    }
    
    public class AKEventCallbackSystem<T1, T2, T3> : AKEcsSystem
    {
        private EcsWorld _world;

        private EcsPool<AKEventCallback<T1, T2, T3>> _pool;

        private IAKEventsService _eventsService;

        private readonly Queue<AKEventCallback<T1, T2, T3>> _eventToCallbackQueue;
        private readonly AKEvent _akEvent;

        public AKEventCallbackSystem(AKEvent akEvent)
        {
            _akEvent = akEvent;
            _eventToCallbackQueue = new Queue<AKEventCallback<T1, T2, T3>>();
        }

        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _pool = _world.GetPool<AKEventCallback<T1, T2, T3>>();

            _eventsService = container.Resolve<IAKEventsService>();
            
            _eventsService.AddListener<T1, T2, T3>(_akEvent, OnEventCallback);
        }
        
        public override void Tick(ref IEcsSystems systems)
        {
            while (_eventToCallbackQueue.Count > 0)
            {
                _pool.Add(_world.NewEntity()) = _eventToCallbackQueue.Dequeue();
            }
        }

        private void OnEventCallback(AKEvent akEvent, T1 arg1, T2 arg2, T3 arg3)
        {
            _eventToCallbackQueue.Enqueue(new AKEventCallback<T1, T2, T3>(akEvent, arg1, arg2, arg3));
        }
    }
    
    public class AKEventCallbackSystem<T1, T2, T3, T4> : AKEcsSystem
    {
        private EcsWorld _world;

        private EcsPool<AKEventCallback<T1, T2, T3, T4>> _pool;

        private IAKEventsService _eventsService;

        private readonly Queue<AKEventCallback<T1, T2, T3, T4>> _eventToCallbackQueue;
        private readonly AKEvent _akEvent;

        public AKEventCallbackSystem(AKEvent akEvent)
        {
            _akEvent = akEvent;
            _eventToCallbackQueue = new Queue<AKEventCallback<T1, T2, T3, T4>>();
        }

        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _pool = _world.GetPool<AKEventCallback<T1, T2, T3, T4>>();

            _eventsService = container.Resolve<IAKEventsService>();
            
            _eventsService.AddListener<T1, T2, T3, T4>(_akEvent, OnEventCallback);
        }
        
        public override void Tick(ref IEcsSystems systems)
        {
            while (_eventToCallbackQueue.Count > 0)
            {
                _pool.Add(_world.NewEntity()) = _eventToCallbackQueue.Dequeue();
            }
        }

        private void OnEventCallback(AKEvent akEvent, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            _eventToCallbackQueue.Enqueue(new AKEventCallback<T1, T2, T3, T4>(akEvent, arg1, arg2, arg3, arg4));
        }
    }
}
