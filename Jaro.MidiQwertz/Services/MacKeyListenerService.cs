using System.Runtime.InteropServices;

namespace Jaro.MidiQwertz.Services;


static class MacKeyListenerService
{
    private const string ApplicationServices =
        "/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices";

    private const string CoreFoundation =
        "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";

    private static readonly EventCallback Callback = OnKey;

    public static void Start()
    {
        var eventTap = CGEventTapCreate(
            1, 0, 1,
            1UL << 10,
            Callback,
            IntPtr.Zero);

        var source = CFMachPortCreateRunLoopSource(IntPtr.Zero, eventTap, 0);
        var mode = CFStringCreateWithCString(
            IntPtr.Zero,
            "kCFRunLoopDefaultMode",
            0x08000100);

        CFRunLoopAddSource(CFRunLoopGetCurrent(), source, mode);
        CFRunLoopRun();
    }

    private static IntPtr OnKey(
        IntPtr proxy,
        int type,
        IntPtr keyboardEvent,
        IntPtr userInfo)
    {
        if (type == 10)
        {
            Console.WriteLine(CGEventGetIntegerValueField(keyboardEvent, 9));
        }

        return keyboardEvent;
    }

    private delegate IntPtr EventCallback(
        IntPtr proxy,
        int type,
        IntPtr keyboardEvent,
        IntPtr userInfo);

    [DllImport(ApplicationServices)]
    private static extern IntPtr CGEventTapCreate(
        uint tap,
        uint place,
        uint options,
        ulong eventMask,
        EventCallback callback,
        IntPtr userInfo);

    [DllImport(ApplicationServices)]
    private static extern long CGEventGetIntegerValueField(
        IntPtr keyboardEvent,
        int field);

    [DllImport(CoreFoundation)]
    private static extern IntPtr CFMachPortCreateRunLoopSource(
        IntPtr allocator,
        IntPtr port,
        int order);

    [DllImport(CoreFoundation)]
    private static extern IntPtr CFRunLoopGetCurrent();

    [DllImport(CoreFoundation)]
    private static extern void CFRunLoopAddSource(
        IntPtr runLoop,
        IntPtr source,
        IntPtr mode);

    [DllImport(CoreFoundation)]
    private static extern void CFRunLoopRun();

    [DllImport(CoreFoundation)]
    private static extern IntPtr CFStringCreateWithCString(
        IntPtr allocator,
        string value,
        uint encoding);
}