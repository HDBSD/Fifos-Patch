using System;
using System.Reflection;
using UnityEngine;

using Fifo.Core;
using Fifo.Core.Saving;
using Fifo.Characters;


namespace HDMods.Dev
{
    public class DebugUI 
    {

        static Rect windowRect = new Rect(20, 20, 200, 300);
        static float lvlslider = 0;
        static bool showUI = false;
        static bool pendingUIStateChanged = false;

        public static void OnGUI()
        {
            GUI.Label(new Rect(Screen.width - 130, Screen.height - 20, 130, 20), "HDPatch 0.0.1 - <color=red><b>DEV</b></color>");

            if (showUI)
            {
                Cursor.lockState = CursorLockMode.Confined;
                windowRect = GUI.Window(0, windowRect, MainUI, "HDPatch - Dev Menu");
            }

            if (Input.GetKeyDown(KeyCode.F8) && pendingUIStateChanged == false)
            {
                pendingUIStateChanged = true;
                return;
            }

            if (pendingUIStateChanged && Input.GetKeyDown(KeyCode.F8) == false)
            {
                UnityEngine.Object.FindObjectOfType<MenuGenericManager>().enabled = showUI;
                pendingUIStateChanged = false;
                showUI = !showUI;
                Cursor.visible = showUI;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        static void MainUI(int windowID)
        {

            

            lvlslider = LabelSlider(new Rect(15, 20, 100, 20), lvlslider, 10.0f, "Level ID");


            if (GUI.Button(new Rect(15, 40, 170, 20), "Load Level"))
            {
                GameManager gm = UnityEngine.Object.FindObjectOfType<GameManager>();
                SaveData save = (SaveData)typeof(GameManager).GetField("saveData", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(gm);

                if (save == null)
                {
                    typeof(GameManager).GetField("saveData", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(gm, new SaveData());
                }

                SceneLoader.LoadScene((int)lvlslider);
            }

            if (GUI.Button(new Rect(15, 60, 170, 20), "Toggle Jump"))
            {
                Player Ply = UnityEngine.Object.FindObjectOfType<Player>();
                PlayerSaveData saveData = (PlayerSaveData)typeof(Player).GetField("saveData", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Ply);

                foreach (SkillSaveData skill in saveData.SkillsData.Skills)
                {
                    if (skill.Action == "Jump")
                        skill.Unlocked = !skill.Unlocked;
                }
            }

            if (GUI.Button(new Rect(15, 80, 170, 20), "Toggle Double Jump"))
            {
                Player Ply = UnityEngine.Object.FindObjectOfType<Player>();
                PlayerSaveData saveData = (PlayerSaveData)typeof(Player).GetField("saveData", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Ply);

                foreach (SkillSaveData skill in saveData.SkillsData.Skills)
                {
                    if (skill.Action == "DoubleJump")
                        skill.Unlocked = !skill.Unlocked;
                }
            }

            if (GUI.Button(new Rect(15, 100, 170, 20), "Toggle Whirlwind"))
            {
                Player Ply = UnityEngine.Object.FindObjectOfType<Player>();
                PlayerSaveData saveData = (PlayerSaveData)typeof(Player).GetField("saveData", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Ply);

                foreach (SkillSaveData skill in saveData.SkillsData.Skills)
                {
                    if (skill.Action == "Whirlwind")
                        skill.Unlocked = !skill.Unlocked;
                }
            }

            if (GUI.Button(new Rect(15, 120, 170, 20), "Toggle Stomp"))
            {
                Player Ply = UnityEngine.Object.FindObjectOfType<Player>();
                PlayerSaveData saveData = (PlayerSaveData)typeof(Player).GetField("saveData", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Ply);

                foreach (SkillSaveData skill in saveData.SkillsData.Skills)
                {
                    if (skill.Action == "Stomp")
                        skill.Unlocked = !skill.Unlocked;
                }
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
