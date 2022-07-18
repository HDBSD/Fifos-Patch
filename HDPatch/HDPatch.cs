using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;

namespace HDMods
{
    public class HDPatch : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();

            // create conf dir in game folder
            System.IO.Directory.CreateDirectory(Environment.CurrentDirectory + @"\Conf");
        }
    }

}