﻿using SpaceInvadersMob.Game.Projectiles;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SpaceInvadersMob.Infrastructure.Pools
{
    public class ProjectileLinePool: BasePool<ProjectileLine>
    {

        protected override ProjectileLine CreateObj()
        {
            var prefab = IGameResourceService.LoadProjectile(ProjectileType.Line);
            
            var obj =  Object.Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<ProjectileLine>();

            obj.OnReturn(Return);
            return obj;
        }
        
        protected override void OnGet(ProjectileLine obj)
        {
            obj.gameObject.SetActive(true);
        }

        protected override void OnRelease(ProjectileLine obj)
        {
            obj.gameObject.SetActive(false);
        }
        
        public ProjectileLinePool(int capacity, int size) : base(capacity, size)
        {
            InitPool();
        }
    }
}