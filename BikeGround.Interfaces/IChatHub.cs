using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeGround.Interfaces
{
    /// <summary>
    /// Interface koji propisuje metode koje mora sadržavati chat hub. Chat klasa mora biti jednaka na .API i .WEB sloju, a ovaj interface to osigurava
    /// </summary>
    public interface IChatHub
    {
        void Subscribe(string group);

        void Unsubscribe(string group);

        void Send(string grupa, string name, string message);
    }
}
