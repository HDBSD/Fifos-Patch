using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;

namespace HDMods
{
    public class HDPatch : MelonMod
    {

        private bool DevMode = false;

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();

            DevMode = Environment.GetCommandLineArgs().Contains("-HDPatch_Dev");

            // create conf dir in game folder
            System.IO.Directory.CreateDirectory(Environment.CurrentDirectory + @"\Conf");
        }

        public override void OnGUI()
        {
            base.OnGUI();

            // if on main menu show HD Patch info

            if (Fifo.Core.SceneLoader.Current == Fifo.Core.SceneIndex.MainMenu && !DevMode)
                GUI.Label(new Rect(Screen.width - 90, Screen.height - 20, 90, 20), "HDPatch 0.0.1");

            // if dev flag is set 

            if (DevMode)
                Dev.DebugUI.OnGUI();
        }
    }

}
