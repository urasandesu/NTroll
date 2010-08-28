using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Urasandesu.NTroll.PInvoke;
using System.Diagnostics;
using Urasandesu.NAnonym.Linq;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Urasandesu.NTroll.PInvoke.Structures;
using Urasandesu.NTroll.PInvoke.Enums;
using System.Drawing;
using System.Windows.Interop;
using System.Collections.ObjectModel;

namespace Urasandesu.NTroll.EnumerateWindowsSample
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            IntPtr window1HWnd = ((HwndSource)HwndSource.FromVisual(this)).Handle;
            //var zOrder = new LinkedList<IntPtr>();
            //var zOrder = new List<IntPtr>();

            bool success = 
            User32.EnumWindows(
                (hWnd, lParam) =>
                {
                    if (!User32.IsWindowVisible(hWnd)) return true;

                    // MEMO: ／(^o^)＼ http://stackoverflow.com/questions/295996/is-the-order-in-which-handles-are-returned-by-enumwindows-meaningful
                    string title = GetWindowTitle(hWnd);
                    if (!string.IsNullOrEmpty(title))
                    {
                        Console.WriteLine("Window: {0}", title);
                    }

                    //IntPtr hWndPrev = IntPtr.Zero;
                    //var hWndNode = default(LinkedListNode<IntPtr>);
                    //var hWndPrevNode = default(LinkedListNode<IntPtr>);
                    //if ((hWndPrev = User32.GetWindow(hWnd, GetWindow_Cmd.GW_HWNDPREV)) != IntPtr.Zero)
                    //{
                    //    if (zOrder.Count == 0)
                    //    {
                    //        hWndNode = zOrder.AddFirst(hWnd);
                    //        zOrder.AddAfter(hWndNode, hWndPrev);
                    //    }
                    //    else
                    //    {
                    //        if ((hWndNode = zOrder.Find(hWnd)) != null)
                    //        {
                    //            if ((hWndPrevNode = zOrder.FindLast(hWndPrev)) != null)
                    //            {
                    //                zOrder.Remove(hWndPrevNode);
                    //            }
                    //            zOrder.AddAfter(hWndNode, hWndPrev);
                    //        }
                    //        else if ((hWndPrevNode = zOrder.Find(hWndPrev)) != null)
                    //        {
                    //            if ((hWndNode = zOrder.FindLast(hWnd)) != null)
                    //            {
                    //                zOrder.Remove(hWndNode);
                    //            }
                    //            zOrder.AddBefore(hWndPrevNode, hWnd);
                    //        }
                    //        else
                    //        {
                    //            hWndNode = zOrder.AddLast(hWnd);
                    //            zOrder.AddAfter(hWndNode, hWndPrev);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    Console.WriteLine("Expected error at GetWindow. {0}", new Win32Exception(Marshal.GetLastWin32Error()).Message);
                    //}

                    return true;
                }, 
                IntPtr.Zero);

            if (!success)
            {
                Console.WriteLine("Expected error at EnumThreadWindows.");
            }

            //Console.WriteLine("Z-Order: ");
            //zOrder.ForEach(
            //hWnd =>
            //{
            //    string title = GetWindowTitle(hWnd);
            //    if (!string.IsNullOrEmpty(title))
            //    {
            //        Console.WriteLine("Window: {0}", title);
            //    }
            //});
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            new Window2() { Owner = this, Title = "Hoge" }.Show();
        }

        string GetWindowTitle(IntPtr hWnd)
        {
            int length = User32.GetWindowTextLength(hWnd);
            if (0 < length)
            {
                var lpString = new StringBuilder(length + 1);
                if (0 < User32.GetWindowText(hWnd, lpString, lpString.Capacity))
                {
                    return lpString.ToString();
                }
                else
                {
                    //Console.WriteLine("Expected error at GetWindowText. {0}", new Win32Exception(Marshal.GetLastWin32Error()).Message);
                    return null;
                }
            }
            else
            {
                //Console.WriteLine("Expected error at GetWindowTextLength. {0}", new Win32Exception(Marshal.GetLastWin32Error()).Message);
                return null;
            }
        }
    }
}
