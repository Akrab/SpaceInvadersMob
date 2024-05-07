using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvadersMob.Infrastructure.Containers
{

    public interface IConfigContainer
    {
        T Get<T>() where T : ScriptableObject;
    }
    
    public class ConfigContainer: IConfigContainer
    {

        private readonly Dictionary<Type, ScriptableObject> containers = new Dictionary<Type, ScriptableObject>();

        public void Add(ScriptableObject container)
        {
            if (containers.ContainsKey(container.GetType()) == false) containers[container.GetType()] = container;
        }

        public T Get<T>() where T : ScriptableObject
        {
            containers.TryGetValue(typeof(T), out var form);
            return (T)form;
        }

        public void Remove<T>()
        {
            containers.Remove(typeof(T));
        }
    }
}
