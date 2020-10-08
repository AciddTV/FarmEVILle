using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryEngine.UI;
using CryEngine.UI.Components;
using CryEngine.Rendering;
using CryEngine.FileSystem;
using CryEngine.Resources;

namespace CryEngine.Projects.Game
{
    [EntityComponent(Guid= "AE126E56-8DA4-40D9-9F4C-8AD3D0952E7E")]
    public class FarmhouseA : EntityComponent
    {
        private Canvas _canvas;
        private Text text;
        private Text clickableText;
        private FarmhouseA _farmhouse;
        private cowpen _cowpen;
        private ViewCamera _cam;

        private float _mass = 90f;

        [EntityProperty]
        public float _productionRate { get; set; }

        [EntityProperty]
        public float _buildingCost { get; set; }

        public float _amount { get; set; }

        [EntityProperty]
        public float _prodTemp { get; set; }
        public float _i { get; set; }
        public int mult { get; set; }

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

        [EntityProperty]
        public string _level { get; set; }

        [EntityProperty]
        public bool _boosted { get; set; }

        public enum _Levels { Level1, Level2, Level3 };     //Stuff like this is just giving the farmhouse entity it's own properties to work with

        private Overseer _overseer;
        private TimeKeeper _timeKeeper;                     //This right here is how we keep the program from crashing, because we need to initialize everything first
        //void start()
        protected override void OnGameplayStart()
        {
            base.OnGameplayStart();
            createUI();
            Mouse.ShowCursor();
            _prodTemp = 150;
            _i = 0;
            _boosted = true;
            mult = 1;
        }                                               //essentially our version of void Start()
        

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Initialize();
        }

        private void Initialize()
        {
            _overseer = new Overseer(this);
            _timeKeeper = new TimeKeeper();
            _timeKeeper.initTimeKeeper();
            _cowpen = new cowpen();
            _cam = new ViewCamera();
        }                                               //this here is the important thing that keeps us from crashing, as everything is initialized from the second we start the game

        //automated production. WIP - integrating upgrades and boosting systems.Amount is currently controlled in the editor.

        private void production(float rate, float prodtemp)
        {
            switch(_level) {
                case "Level1":
                    mult = 1;
                    break;
                case "Level2":
                    mult = 3;
                    break;
                case "Level3":
                    mult = 9;
                    break;
            }                                           //The upgrade system that's still a work in progress
            
            if(_i == prodtemp)
            {
                _amount += rate * mult;
                Log.Info(_amount.ToString());
                _i = 0;
            }
            else if(_i < prodtemp)
            {
                _i++;
            }
        }
        private void createOBJ()                    //This is basically kinda what I want Mitch to do, it's a bit of like adding components but what I want him to do is specifically add Mesh on runtime
        {
            _farmhouse.Entity.AddComponent<FarmhouseA>();
        }
        //UI Creation
        private void createUI()                     //Right here is the UI, obviously you'll see that in the full game, The UI isn't fully finished, right now we've got a button and I'm working through placements but it's coming along nicely
        {
            _canvas = SceneObject.Instantiate<Canvas>(null);

            text = _canvas.AddComponent<Text>();
            text.Alignment = Alignment.Center;
            text.Height = 24;
            //text.Content = "Basic UI example";
            text.Offset = new Point(0f, 120f);

            var pnl = SceneObject.Instantiate<Panel>(_canvas);

            clickableText = _canvas.AddComponent<Text>();
            clickableText.Height = 15;
            

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

            var btnAddCow = SceneObject.Instantiate<Button>(_canvas);
            btnAddCow.RectTransform.Alignment = Alignment.Center;
            btnAddCow.RectTransform.Padding = new Padding(200f, 150f);
            btnAddCow.RectTransform.Size = new Point(238.5f, 57.5f);
            btnAddCow.Background.Source = ResourceManager.ImageFromFile("Assets/UI/cowfactory.png", false);
            btnAddCow.Background.SliceType = SliceType.None;

            btnAddCow.Ctrl.OnPressed += addpen; 

           
        }
        private void addpen()
        {
            try
            {
                Vector3 nvec = new Vector3(521f, 518f, 32f);
                Entity.SpawnWithComponent<cowpen>("added", nvec, Quaternion.Identity, 1.0f);
                _cowpen.Entity.LoadGeometry(0, Primitives.Sphere);
                //Log.Info("object should be at " + _cowpen.Entity.Position.X.ToString() + " " + _cowpen.Entity.Position.Y.ToString() + " " + _cowpen.Entity.Position.Z.ToString());
            } catch
            {
                Log.Info("cfg not found by cryengine");
            }
        }

        private void mouselocale()
        {
            
        }

        private void OnCowAdd()                         //This here is very important, this spawns a new farmentity, and I'll walk you through what's happening when we get there in the actual engine
        {
            Log.Info("Cowhouse has been added");
            Entity.SpawnWithComponent<FarmhouseA>("FarmhouseA", Vector3.Zero, Quaternion.Identity, 1);

        }
        private void OnUIbtnPressed()
        {
            Log.Info("FarmhouseA current produce is " + _amount.ToString());
            text.Content = "FarmhouseA current produce is " + _amount.ToString();
            _boosted = false;
        }

        //WIP
        private void OnLeftBtnPressed()
        {
            

            Vector3 t = Vector3.Left;
            Log.Info("Camera to the left");
        }

        //Gives the farmhouseA object an actual shape, and also assigns physics to that object
        private void setBuilding()                              //This here is the thing that gives the farmhouse building that pyramid shape.
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

        //void Update() 
        protected override void OnUpdate(float frameTime)           //essentially our version of void Update()
        {
            production(_productionRate, _prodTemp);

            _overseer.UpdateView(frameTime);
            _timeKeeper.startTimeKeeper(frameTime);

            if (Mouse.LeftDown)
            {
                Log.Info(Mouse.CursorPosition.X.ToString() + " " + Mouse.CursorPosition.Y.ToString());
            }
            if(Mouse.HitEntity != null)
            {
                Log.Info(Mouse.HitEntityId.ToString());
            }
        }

        private void checkforassets()
        {

        }

    }

    [EntityComponent(Category = "Camera", Guid = "53D8FD0A-BA7D-4D1C-810F-4D5738C05736")]
    public class ViewCamera : EntityComponent
    {
        [SerializeValue]
        public static ViewCamera ActiveCamera { get; private set; }

        [SerializeValue]
        private View _view;
        [SerializeValue]
        private bool _active;

        [EntityProperty(EntityPropertyType.Primitive, "Defines if this is the currently active camera")]
        public bool Active {
            get { return _active; }
            set
            {
                if (value)
                {
                    if(ActiveCamera != null)
                    {
                        ActiveCamera._active = false;
                        ActiveCamera._view.SetActive(false);
                    }
                    _active = true;
                    if(_view == null)
                    {
                        _view = View.Create();
                    }
                    _view.SetActive(true);
                    ActiveCamera = this;
                }
                else
                {
                    if(ActiveCamera == this)
                    {
                        ActiveCamera = null;
                    }
                    _active = false;
                    if(_view != null)
                    {
                        _view.SetActive(false);
                    }
                }
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            if(_view == null)
            {
                _view = View.Create();
            }
            _view.LinkTo(Entity);

            if(_active || ActiveCamera == null)
            {
                Active = true;
            }
        }

        protected override void OnUpdate(float frameTime)
        {
            base.OnUpdate(frameTime);

            if (_active)
            {
                _view.Update(frameTime, _active);
            }
        }
    }

}
