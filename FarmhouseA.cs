using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryEngine.UI;
using CryEngine.UI.Components;
using CryEngine.Rendering;

namespace CryEngine.Projects.Game
{
    [EntityComponent(Guid= "AE126E56-8DA4-40D9-9F4C-8AD3D0952E7E")]
    class FarmhouseA : EntityComponent
    {
        private Canvas _canvas;
        private Text text;

        private float _mass = 90f;

        [EntityProperty]
        public float _productionRate { get; set; }

        [EntityProperty]
        public float _buildingCost { get; set; }

        public float _amount { get; set; }

        [EntityProperty]
        public float _prodTemp { get; set; }
        public float _i { get; set; }

        [EntityProperty]
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
        //void start()
        protected override void OnGameplayStart()
        {
            base.OnGameplayStart();
            createUI();
            Mouse.ShowCursor();
            _prodTemp = 150;
            _i = 0;
        }
        //void Update()
        protected override void OnUpdate(float frameTime)
        {
            production(_productionRate, _prodTemp);

        }

        //automated production. WIP - integrating upgrades and boosting systems.Amount is currently controlled in the editor.

        private void production(float rate, float prodtemp)
        {
            
            if(_i == prodtemp)
            {
                _amount += rate;
                Log.Info(_amount.ToString());
                _i = 0;
            }
            else if(_i < prodtemp)
            {
                _i++;
            }
        }

        //UI Creation
        private void createUI()
        {
            _canvas = SceneObject.Instantiate<Canvas>(null);

            text = _canvas.AddComponent<Text>();
            text.Alignment = Alignment.Center;
            text.Height = 24;
            //text.Content = "Basic UI example";
            text.Offset = new Point(0f, 120f);

            //Create a button that shows the total amount of produce that's been gained so far. Gaining produce is automatic.

            var btn = SceneObject.Instantiate<Button>(_canvas);
            btn.RectTransform.Alignment = Alignment.BottomLeft;
            btn.RectTransform.Padding = new Padding(0f, -160.0f);
            btn.RectTransform.Size = new Point(300f, 30f);
            btn.Ctrl.Text.Content = "FarmhouseA turnover";
            btn.Ctrl.Text.Height = 18;
            btn.Ctrl.Text.DropsShadow = true;
            btn.Ctrl.Text.Alignment = Alignment.Center;

            //When the btn thats been created is pressed, method OnUIbtnPressed is called

            btn.Ctrl.OnPressed += OnUIbtnPressed;
            
            //WIP - Button that changes position of camera so that the player can move the camera around
            var btnLeft = SceneObject.Instantiate<Button>(_canvas);
            btnLeft.RectTransform.Alignment = Alignment.BottomRight;
            btnLeft.RectTransform.Padding = new Padding(0f, -160f);
            btnLeft.RectTransform.Size = new Point(100f, 20f);
            btnLeft.Ctrl.Text.Content = "<";
            btnLeft.Ctrl.Text.Height = 10;
            btnLeft.Ctrl.Text.DropsShadow = true;
            btnLeft.Ctrl.Text.Alignment = Alignment.Center;

            btnLeft.Ctrl.OnPressed += OnLeftBtnPressed;
        }

        private void OnUIbtnPressed()
        {
            Log.Info("FarmhouseA current produce is " + _amount.ToString());
            text.Content = "FarmhouseA current produce is " + _amount.ToString();
        }

        //WIP
        private void OnLeftBtnPressed()
        {
            Vector3 t = Vector3.Left;
            Log.Info("Camera to the left");
        }

        //Gives the farmhouseA object an actual shape, and also assigns physics to that object
        private void setBuilding()
        {
            var entity = Entity;
            entity.LoadGeometry(0, Primitives.Pyramid);

            var PhysicsEntity = Entity.Physics;
            if(PhysicsEntity == null)
            {
                return;
            }
            PhysicsEntity.Physicalize(Mass, PhysicalizationType.Rigid);
        }

    }

    

    [EntityComponent(Guid= "AC8BB444-B598-42AD-9188-68385E3663DD")]
    class FarmhouseB : EntityComponent {
        [EntityProperty]
        public float _productionRate { get; set; }

        [EntityProperty]
        public float _buildingCost { get; set; }

        protected override void OnGameplayStart()
        {
            base.OnGameplayStart();
        }

        protected override void OnUpdate(float frameTime)
        {
            base.OnUpdate(frameTime);
        }
    }
}
