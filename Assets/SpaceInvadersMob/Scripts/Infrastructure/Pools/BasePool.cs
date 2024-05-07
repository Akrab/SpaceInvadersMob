﻿using SpaceInvadersMob.Services;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.Pools
{
    public abstract class BasePool<T> where T : Object
    {
        [Inject] private IGameResourceService _gameResourceService;
        private ObjectPool<T> _pool;
        private int _capacity;
        private int _size;

        protected IGameResourceService IGameResourceService => _gameResourceService;
        
        protected void InitPool()
        {

            _pool = new ObjectPool<T>(
                CreateObj,
                OnGet,
                OnRelease,
                obj => Object.Destroy(obj, 0),
                false,
                _capacity, _size
            );
        }
        
        protected abstract T CreateObj();
        protected abstract void OnGet(T obj);
        protected abstract void OnRelease(T obj);
        protected void Return(T obj)
        {
            _pool.Release(obj);
        }
        protected BasePool(int capacity, int size)
        {
            _capacity = capacity;
            _size = size;
        }

        public T Get()
        {
            return _pool.Get();
        }
        
    }
}