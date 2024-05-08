using SpaceInvadersMob.Game.Projectiles;
using UnityEngine;

namespace SpaceInvadersMob.Infrastructure.Pools
{
    public class ProjectileSpherePool : BasePool<ProjectileSphere>
    {

        protected override ProjectileSphere CreateObj()
        {
            var prefab = IGameResourceService.LoadProjectile(ProjectileType.Sphere);

            var obj = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<ProjectileSphere>();
            DiContainer.Inject(obj);
            obj.OnRelease(Release);
            return obj;
        }

        protected override void OnGet(ProjectileSphere obj)
        {
            obj.gameObject.SetActive(true);
            obj.OnFire();
        }

        protected override void OnRelease(ProjectileSphere obj)
        {
            obj.gameObject.SetActive(false);
        }

        public ProjectileSpherePool(int capacity, int size) : base(capacity, size)
        {
            InitPool();
        }
    }
}