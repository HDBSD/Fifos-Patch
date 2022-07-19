using System;
using System.Reflection;
using UnityEngine;

namespace HDMods.Dev
{
    public class DebugUI 
    {

        static Rect windowRect = new Rect(20, 20, 200, 300);
        static float lvlslider = 0;


        public static void OnGUI()
        {
            GUI.Label(new Rect(Screen.width - 130, Screen.height - 20, 130, 20), "HDPatch 0.0.1 - <color=red><b>DEV</b></color>");

            windowRect = GUI.Window(0, windowRect, DoMyWindow, "HDPatch - Dev Menu");
            
        }

        static void DoMyWindow(int windowID)
        {
            lvlslider = LabelSlider(new Rect(15, 20, 100, 20), lvlslider, 10.0f, "Level ID");
            if (GUI.Button(new Rect(15, 40, 100, 20), "Load Level"))
            {
                Fifo.Core.GameManager gm = UnityEngine.Object.FindObjectOfType<Fifo.Core.GameManager>();
                Fifo.Core.Saving.SaveData save = (Fifo.Core.Saving.SaveData)typeof(Fifo.Core.GameManager).GetField("saveData", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(gm);

                if (save == null)
                {
                    typeof(Fifo.Core.GameManager).GetField("saveData", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(gm, new Fifo.Core.Saving.SaveData());
                }

                Fifo.Core.SceneLoader.LoadScene((int)lvlslider);
            }

            GUI.DragWindow(new Rect(0, 0, 10000, 20));
        }

        static float LabelSlider(Rect screenRect, float sliderValue, float sliderMaxValue, string labelText)
        {

            sliderValue = GUI.HorizontalSlider(screenRect, sliderValue, 0.0f, sliderMaxValue);
            
            screenRect.x += 3 + screenRect.width;
            screenRect.y -= 6;

            GUI.Label(screenRect, labelText + $": {((int)sliderValue).ToString()}");

            return sliderValue;
        }


    }
}
