using System.Collections;
using System.Collections.Generic;
using _Source.Code._AKFramework.AKEvents.Runtime;
using _Source.Code.Utils.Extensions;
using _Source.Code.Utils.Extensions.EcsEvents;
using Leopotam.EcsLite;
using UnityEngine;

public static class EventsExtensions
{
    public static IEcsSystems EventCallbackHere(this IEcsSystems systems, AKEvent akEvent)
    {
        return systems.Add(new AKEventCallbackSystem(akEvent));
    }

    public static IEcsSystems EventCallbackHere<T1>(this IEcsSystems systems, AKEvent akEvent)
    {
        return systems.Add(new AKEventCallbackSystem<T1>(akEvent));
    }

    public static IEcsSystems EventCallbackHere<T1, T2>(this IEcsSystems systems, AKEvent akEvent)
    {
        return systems.Add(new AKEventCallbackSystem<T1, T2>(akEvent));
    }

    public static IEcsSystems EventCallbackHere<T1, T2, T3>(this IEcsSystems systems, AKEvent akEvent)
    {
        return systems.Add(new AKEventCallbackSystem<T1, T2, T3>(akEvent));
    }

    public static IEcsSystems EventCallbackHere<T1, T2, T3, T4>(this IEcsSystems systems, AKEvent akEvent)
    {
        return systems.Add(new AKEventCallbackSystem<T1, T2, T3, T4>(akEvent));
    }

    public static bool HasEventCallback<T>(this EcsWorld world, AKEvent akEvent) where T : struct, IAKEventCallback
    {
        var filter = world.Filter<T>().End();
        var pool = world.GetPool<T>();

        foreach (var entity in filter)
        {
            if (pool.Get(entity).AKEvent == akEvent) return true;
        }

        return false;
    }
    
    public static bool HasEventCallback<T>(this EcsWorld world, AKEvent akEvent,
        out EcsPackedEntity eventCallbackPackedEntity) where T : struct, IAKEventCallback
    {
        var filter = world.Filter<T>().End();
        var pool = world.GetPool<T>();

        eventCallbackPackedEntity = new EcsPackedEntity();

        foreach (var entity in filter)
        {
            if (pool.Get(entity).AKEvent != akEvent) continue;
            
            eventCallbackPackedEntity = world.PackEntity(entity);
            
            return true;
        }

        return false;
    }

    public static bool HasEventCallback<T>(this EcsWorld world, AKEvent akEvent,
        out List<EcsPackedEntity> eventCallbackPackedEntities) where T : struct, IAKEventCallback
    {
        var filter = world.Filter<T>().End();
        var pool = world.GetPool<T>();

        eventCallbackPackedEntities = new List<EcsPackedEntity>(10);

        foreach (var entity in filter)
        {
            if (pool.Get(entity).AKEvent != akEvent) continue;

            eventCallbackPackedEntities.Add(world.PackEntity(entity));
        }

        return eventCallbackPackedEntities.Count > 0;
    }
}
