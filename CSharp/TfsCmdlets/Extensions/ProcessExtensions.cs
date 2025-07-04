using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TfsCmdlets.Extensions
{
    public static class ProcessExtensions
    {
        private const uint TH32CS_SNAPPROCESS = 0x00000002;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESSENTRY32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }

        /// <summary>
        /// Retorna o PID do processo pai, ou 0 se não encontrado.
        /// </summary>
        public static int GetParentProcessId(this Process process)
        {
            if (process == null)
                throw new ArgumentNullException(nameof(process));

            var snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
            if (snapshot == IntPtr.Zero)
                return 0;

            try
            {
                var entry = new PROCESSENTRY32();
                entry.dwSize = (uint)Marshal.SizeOf(entry);

                if (Process32First(snapshot, ref entry))
                {
                    do
                    {
                        if (entry.th32ProcessID == (uint)process.Id)
                            return (int)entry.th32ParentProcessID;
                    }
                    while (Process32Next(snapshot, ref entry));
                }
            }
            finally
            {
                CloseHandle(snapshot);
            }

            return 0;
        }

        /// <summary>
        /// Retorna o processo pai, ou null se não encontrado.
        /// </summary>
        public static Process ParentProcess(this Process process)
        {
            if (process == null) throw new ArgumentNullException(nameof(process));

            int parentPid = process.GetParentProcessId();

            if (parentPid == 0) return null;

            try
            {
                return Process.GetProcessById(parentPid);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Retorna o handle da janela principal do processo ou de seu ancestral.
        /// </summary>
        public static IntPtr WindowHandleRecursive(this Process process)
        {
            if (process == null)
                throw new ArgumentNullException(nameof(process));

            var current = process;

            while (current is { Id: not 0 })
            {
                var hwnd = current.MainWindowHandle;
                if (hwnd != IntPtr.Zero) return hwnd;

                current = current.ParentProcess();
                if (current == null) break;
            }

            return IntPtr.Zero;
        }
    }
}
