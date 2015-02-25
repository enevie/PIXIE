using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatkaGame
{
    interface ICollidable
    {
        //Respond to collision
        void RespondToCollision(Batka batka);       
    }
}
