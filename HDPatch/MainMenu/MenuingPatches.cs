using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDMods
{

    // fixes for wrongwarp

    [HarmonyPatch(typeof(Fifo.Core.GameManager), "LoadGame", new[] { typeof(int), typeof(bool) })]
    class LoadGame_Patch
    {
        [HarmonyPrefix]
        internal static bool Prefix()
        {
            // if in loading screen abort regular code path
            return !Fifo.Core.SceneLoader.IsloadingScene;
        }
    }

    [HarmonyPatch(typeof(Fifo.Core.GameManager), "NewGame", new[] { typeof(int) })]
    class NewGame_Patch
    {
        [HarmonyPrefix]
        internal static bool Prefix()
        {
            // if in loading screen abort regular code path
            return !Fifo.Core.SceneLoader.IsloadingScene;
        }
    }

    // fixes navigation when loading into the game

    [HarmonyPatch(typeof(MenuGenericManager), "GOBackActionSelected")]
    class GOBackActionSelected_Patch
    {
        [HarmonyPrefix]
        internal static bool Prefix()
        {
            // if in loading screen abort regular code path
            return !Fifo.Core.SceneLoader.IsloadingScene;
        }
    }

}
