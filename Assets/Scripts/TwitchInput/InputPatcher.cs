using Harmony;
using Harmony.ILCopying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Patches <see cref="Input"/>
/// </summary>
public class InputPatcher
{
    public static void Patch()
    {
        var harmony = HarmonyInstance.Create("com.bengreenier.unity-twitch-input");
        
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
    
    [HarmonyPatch(typeof(Input))]
    [HarmonyPatch("GetKeyDown")]
    [HarmonyPatch(new Type[] { typeof(string) })]
    public class PatchImpl
    {
        static bool Prefix(ref bool __result, string name)
        {
            Debug.Log("prefix");

            // set original result
            __result = true;

            // don't execute original
            return false;
        }
    }
}