using MRO.ROI.Automation.Selenium;
using System;
using WindowsInput;
using WindowsInput.Native;

namespace MRO.ROI.Automation.Utility
{
    public static class ScannerUtil
    {
        /// <summary>
        /// Scan and accepts test pages.
        /// </summary>
        public static void ScanDocuments(int nPages = 2)
        {
			DebugUtil.DebugMessage("ScanDocuments: " + nPages);
			Driver.Wait(TimeSpan.FromSeconds(3));
			InputSimulator simulator = new InputSimulator();
			for (int i = 0; i < nPages; i++)
			{
				DebugUtil.DebugMessage("ScanDocuments page: " + i);
				Driver.Wait(TimeSpan.FromMilliseconds(500));
				simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
			}
            Driver.Wait(TimeSpan.FromSeconds(1));
			DebugUtil.DebugMessage("ScanDocuments Accept");
			simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A);
            Driver.Wait(TimeSpan.FromSeconds(1 * nPages));
			DebugUtil.DebugMessage("ScanDocuments Completed");
		}
	}

    public static class DebugUtil
    {
        public static void DebugMessage(string message)
        {
            System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString()} : {message}");
        }
    }
}
