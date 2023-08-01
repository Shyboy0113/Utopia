using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class BorderlessWindowController : MonoBehaviour
{
#if UNITY_STANDALONE_WIN
    private const int GWL_STYLE = -16;
    private const uint WS_POPUP = 0x80000000;
    private const uint SWP_FRAMECHANGED = 0x0020;
    private const uint SWP_SHOWWINDOW = 0x0040;
    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int width, int height, uint uFlags);
#endif

#if UNITY_STANDALONE_OSX
    [UnityEditor.MenuItem("Window/Borderless Window")]
    private static void SetBorderlessWindow()
    {
        UnityEditorInternal.InternalEditorUtility.SetCustomLightmapSettings(0);
    }
#endif
    public void SetBorderlessWindow()
    {
#if UNITY_STANDALONE_WIN
        IntPtr windowHandle = GetWindowHandle();
        SetWindowLong(windowHandle, GWL_STYLE, WS_POPUP);
        SetWindowPos(windowHandle, HWND_TOPMOST, 0, 0, Screen.currentResolution.width, Screen.currentResolution.height, SWP_FRAMECHANGED | SWP_SHOWWINDOW);
#endif
    }

#if UNITY_STANDALONE_WIN
    private IntPtr GetWindowHandle()
    {
        IntPtr windowHandle = IntPtr.Zero;
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            windowHandle = GetActiveWindow();
        }
        return windowHandle;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();
#endif
}
