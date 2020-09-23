using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryEngine.Projects.Game
{
    public class Overseer
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
            Log.Info("farmhouse A current level is " + _fhA._level.ToString());
        }
    }
    
}
