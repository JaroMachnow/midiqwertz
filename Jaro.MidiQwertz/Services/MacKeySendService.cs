using System.Runtime.InteropServices;
using Jaro.MidiQwertz.Enums;

namespace Jaro.MidiQwertz.Services;

public static class MacKeySendService
{
    private const string ApplicationServices =
           "/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices";
   
       private const uint HidEventTap = 0;
       private const ulong Command = 1UL << 20;
   
       [DllImport(ApplicationServices)]
       public static extern long CGEventGetIntegerValueField(
           IntPtr keyboardEvent,
           int field);

       [DllImport(ApplicationServices)]
       private static extern IntPtr CGEventCreateKeyboardEvent(
           IntPtr source,
           ushort virtualKey,
           [MarshalAs(UnmanagedType.I1)] bool keyDown);
   
       [DllImport(ApplicationServices)]
       private static extern void CGEventSetFlags(IntPtr keyboardEvent, ulong flags);
   
       [DllImport(ApplicationServices)]
       private static extern void CGEventPost(uint tap, IntPtr keyboardEvent);
   
       [DllImport(ApplicationServices)]
       private static extern void CFRelease(IntPtr reference);

       public static void SendKeyUpDown(MacKey key, MacModifier modifiers = MacModifier.None)
       {
           SendKeyUpDown((ushort)key, (ulong)modifiers);
       }

       public static void SendKeyUp(MacKey key, MacModifier modifiers = MacModifier.None)
       {
           SendKeyUp((ushort)key, (ulong)modifiers);
       }

       public static void SendKeyDown(MacKey key, MacModifier modifiers = MacModifier.None)
       {
           SendKeyDown((ushort)key, (ulong)modifiers);
       }
   
       public static void SendKeyUpDown(ushort keyCode, ulong modifiers = 0)
       {
           var keyDown = CGEventCreateKeyboardEvent(IntPtr.Zero, keyCode, true);
           var keyUp = CGEventCreateKeyboardEvent(IntPtr.Zero, keyCode, false);
   
           if (keyDown == IntPtr.Zero || keyUp == IntPtr.Zero)
           {
               throw new InvalidOperationException("Could not create a macOS keyboard event.");
           }
   
           try
           {
               CGEventSetFlags(keyDown, modifiers);
               CGEventSetFlags(keyUp, modifiers);
   
               CGEventPost(HidEventTap, keyDown);
               CGEventPost(HidEventTap, keyUp);
           }
           finally
           {
               CFRelease(keyDown);
               CFRelease(keyUp);
           }
       }

       public static void SendKeyUp(ushort keyCode, ulong modifiers = 0)
       {
           var keyUp = CGEventCreateKeyboardEvent(IntPtr.Zero, keyCode, false);
   
           if (keyUp == IntPtr.Zero)
               throw new InvalidOperationException("Could not create a macOS keyboard event.");
   
           try
           {
               CGEventSetFlags(keyUp, modifiers);
   
               CGEventPost(HidEventTap, keyUp);
           }
           finally
           {
               CFRelease(keyUp);
           }
       }

       public static void SendKeyDown(ushort keyCode, ulong modifiers = 0)
       {
           var keyDown = CGEventCreateKeyboardEvent(IntPtr.Zero, keyCode, true);
   
           if (keyDown == IntPtr.Zero)
               throw new InvalidOperationException("Could not create a macOS keyboard event.");
   
           try
           {
               CGEventSetFlags(keyDown, modifiers);
   
               CGEventPost(HidEventTap, keyDown);
           }
           finally
           {
               CFRelease(keyDown);
           }
       }
}