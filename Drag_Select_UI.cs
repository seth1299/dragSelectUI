using System;
using HarmonyLib;
using UnityEngine;

namespace Drag_Select_UINS
{
    // Inherit from your mod framework’s base Mod class.
    public class Drag_Select_UI : Mod
    {
        // Singleton instance.
        public static Drag_Select_UI? _instance;
        public Harmony? HarmonyInstance { get; private set; }
        
        // Fields to track drag state.
        private bool previousMouseIsDragging = false;
        private bool dragHandled = false;

        public void Awake()
        {
            _instance = this;
            // Create a new Harmony instance with a unique ID.
            HarmonyInstance = new Harmony("com.example.dragselectui");
            try
            {
                // Patch all methods marked in this class.
                HarmonyInstance.PatchAll(typeof(Drag_Select_UI));
            }
            catch (Exception ex)
            {
                Logger.Log("Patching failed: " + ex.Message);
            }
        }

        public override void Ready()
        {
            Logger.Log(this.name + " fully loaded!");
            // Called when the mod is fully loaded.
        }

        private void OnDestroy()
        {
            if ( HarmonyInstance != null )
                HarmonyInstance.UnpatchSelf();
        }

        // Harmony patch to hook into InputController.Update.
        [HarmonyPatch(typeof(InputController), "Update")]
        public static class InputController_Update_Patch
        {
            // Postfix patch executes after InputController.Update.
            public static void Postfix(InputController __instance)
            {
                if (_instance == null)
                    return;
                
                bool currentDragging = __instance.MouseIsDragging;

                // When a new drag starts, reset the handled flag.
                if (!_instance.previousMouseIsDragging && currentDragging)
                {
                    _instance.dragHandled = false;
                }

                // If we were dragging in the previous frame and now we're not,
                // and we haven't handled this gesture yet, then the drag has ended.
                if (_instance.previousMouseIsDragging && !currentDragging && !_instance.dragHandled)
                {
                    _instance.HandleDragEnd(__instance);
                    _instance.dragHandled = true;
                }

                _instance.previousMouseIsDragging = currentDragging;
            }
        }

        // Called when a click-and-drag gesture is determined to have ended.
        private void HandleDragEnd(InputController input)
        {
            if (input.InputCount > 0)
            {
                Vector2 start = input.GetStartPosition(0);
                Vector2 end = input.GetInputPosition(0);
                Logger.Log($"Click-and-drag detected from {start} to {end}");
                // Insert your UI selection logic here (e.g. processing which objects fall within a selection rectangle).
            }
        }
    }
}
