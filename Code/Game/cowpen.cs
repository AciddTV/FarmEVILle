using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryEngine;

namespace CryEngine.Projects.Game
{
    [EntityComponent(Guid= "0D6611D0-A7D6-4460-ACC5-700119864DA2")] //in cowpen.cs, we have essentially the cowpen, the shape, the building, the entity and loadfunctions, everything that will give us a space to put the cows in.
    class cowpen : EntityComponent
    {
        [EntityProperty]
        public float _cost { get; set; }

        public const string cowpengeom = "Assets/objects/CowPen.cgf";
        private float _mass = 90.0f;
        public float Mass
        {
            get
            {
                return _mass;
            }
            set
            {
                _mass = value;
                setBuilding();
            }
        }
        private void setBuilding()
        {
            var entity = Entity;
            //entity.LoadCharacter(0, cowpengeom);
            //entity.LoadGeometry(0, Primitives.Sphere);
            entity.LoadGeometry(0, cowpengeom);

            var physicsEntity = Entity.Physics;
            if(physicsEntity == null)
            {
                return;
            }
            physicsEntity.Physicalize(Mass, PhysicalizationType.Rigid);
        }

        protected override void OnGameplayStart()
        {
            base.OnGameplayStart();

            setBuilding();
        }

        protected override void OnUpdate(float frameTime)
        {
            base.OnUpdate(frameTime);
        }

        public void spawnCowPen()
        {
            Entity entity = Entity.Find("cowpenA");
            Vector3 nvec = new Vector3(521f, 518f, 32f);
            Entity.SpawnWithComponent<cowpen>("added", nvec, Quaternion.Identity, 1.0f);
            setBuilding();
        }

        private float getmouselocalex()
        {
            return Mouse.CursorPosition.X;
        }
        private float getmouselocaley()
        {
            return Mouse.CursorPosition.Y;
        }

    }
}
