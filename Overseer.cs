using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryEngine.Projects.Game
{
    public class Overseer                                   //This is also another thing that stops the game from crashing immediately, because we need to initialize and keep tabs on it
    {
        private readonly FarmhouseA _fhA;

        public Overseer(FarmhouseA fha)
        {
            if(fha == null)
            {
                throw new ArgumentNullException(nameof(fha));
            }
            _fhA = fha;
        }

        public void UpdateView(float frametime)
        {
            //Log.Info("farmhouse A current level is " + _fhA._level.ToString());
        }
    }    

    public class TimeKeeper
    {
        private int _days;
        private int _i = 0;
        private int _hours = 0;
        private int _minutes = 0;

        public void initTimeKeeper()
        {
            _days = 0;
            _hours = 0;
            _minutes = 0;
            _i = 0;
        }
        public void startTimeKeeper(float frametime)            //in the overseer, here's our timekeeping class that allows us to actually tell what time of the day, which day, which hour and how many days we have done since the level was started.
        {
            _i++;

            if(_i == 60)
            {
                _minutes += 10;
                _i = 0;
                Log.Info(_minutes.ToString() + " minutes have passed");
            }

            if(_minutes == 60)
            {
                _hours++;
                _minutes = 0;
                Log.Info(_hours.ToString() + " hours has passed");
            }
            if(_hours == 24)
            {
                _days++;
                _hours = 0;
                Log.Info(_days.ToString() + " days have passed");
            }
        }

    }

    public class Money                                  //And here's Dewald's money class that will act as a monetary system once the cow systems are introduced, but currently a steady flow of currency is introduced
    {
        private float _totmoney;
        int _animAmount;
        private float _incomeRate = 2.5f;

        private readonly FarmhouseA _fhA;

        public Money(FarmhouseA fha)
        {
            if(fha == null)
            {
                throw new ArgumentNullException(nameof(fha));
            }
            _fhA = fha;
        }

        public void incomeUp(float frametime)
        {
            _totmoney += (_incomeRate * _animAmount);
        }
    }

}
