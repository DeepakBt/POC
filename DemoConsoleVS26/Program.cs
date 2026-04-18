using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Text;

#if NET7_0_OR_GREATER
// System.Management is available for Windows via the System.Management NuGet package.
// If you target Windows and see compile errors for System.Management, add the NuGet package:
//  - dotnet add package System.Management
using System.Management;
#endif

namespace DemoConsoleVS26
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            WriteHeader("System Summary");
            PrintSystemSummary();

            WriteHeader("CPU");
            PrintCpuInfo();

            WriteHeader("Memory");
            PrintMemoryInfo();

            WriteHeader("Disks");
            PrintDiskInfo();

            WriteHeader("Network Adapters");
            PrintNetworkInfo();

            WriteHeader("GPU / Video Controllers");
            PrintVideoControllers();

            WriteHeader("BIOS / BaseBoard");
            PrintBiosAndBaseboard();

            WriteHeader("Installed Software (Windows)");
            PrintInstalledSoftware();

            Console.WriteLine();
            Console.WriteLine("Done. Press any key to exit...");
            Console.ReadKey();
        }

        static void WriteHeader(string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', 60));
            Console.WriteLine(title);
            Console.WriteLine(new string('-', 60));
        }

        static void PrintSystemSummary()
        {
            Console.WriteLine($"Machine Name: {Environment.MachineName}");
            Console.WriteLine($"User: {Environment.UserName}");
            Console.WriteLine($"OS: {RuntimeInformation.OSDescription}");
            Console.WriteLine($"OS Architecture: {RuntimeInformation.OSArchitecture}");
            Console.WriteLine($".NET Runtime: {RuntimeInformation.FrameworkDescription}");
            Console.WriteLine($"Process Architecture: {RuntimeInformation.ProcessArchitecture}");
            Console.WriteLine($"Logical processors: {Environment.ProcessorCount}");
            Console.WriteLine($"System Up Time (approx): {GetUptime()}");
        }

        static string GetUptime()
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
#if NET7_0_OR_GREATER
                    var mos = new ManagementObjectSearcher("SELECT LastBootUpTime FROM Win32_OperatingSystem");
                    foreach (ManagementObject mo in mos.Get())
                    {
                        var lastBoot = ManagementDateTimeConverter.ToDateTime(mo["LastBootUpTime"].ToString());
                        var up = DateTime.Now - lastBoot;
                        return up.ToString(@"dd\.hh\:mm\:ss");
                    }
#endif
                }
                // fallback approximate using Environment.TickCount64
                var ms = Environment.TickCount64;
                var ts = TimeSpan.FromMilliseconds(ms);
                return ts.ToString(@"dd\.hh\:mm\:ss");
            }
            catch
            {
                return "Unknown";
            }
        }

        static void PrintCpuInfo()
        {
            try
            {
#if NET7_0_OR_GREATER
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var q = new ManagementObjectSearcher("SELECT Name, Manufacturer, NumberOfCores, NumberOfLogicalProcessors, MaxClockSpeed FROM Win32_Processor");
                    foreach (ManagementObject mo in q.Get())
                    {
                        Console.WriteLine($"Name: {mo["Name"]}");
                        Console.WriteLine($"Manufacturer: {mo["Manufacturer"]}");
                        Console.WriteLine($"Cores: {mo["NumberOfCores"]}, Logical Processors: {mo["NumberOfLogicalProcessors"]}");
                        Console.WriteLine($"Max Clock (MHz): {mo["MaxClockSpeed"]}");
                        Console.WriteLine();
                    }
                    return;
                }
#endif
                Console.WriteLine("Detailed CPU information via WMI is only available on Windows with System.Management.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CPU info error: {ex.Message}");
            }
        }

        static void PrintMemoryInfo()
        {
            try
            {
#if NET7_0_OR_GREATER
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var q = new ManagementObjectSearcher("SELECT TotalVisibleMemorySize, FreePhysicalMemory FROM Win32_OperatingSystem");
                    foreach (ManagementObject mo in q.Get())
                    {
                        ulong totalKb = Convert.ToUInt64(mo["TotalVisibleMemorySize"]);
                        ulong freeKb = Convert.ToUInt64(mo["FreePhysicalMemory"]);
                        Console.WriteLine($"Total Physical Memory: {FormatBytes(totalKb * 1024)}");
                        Console.WriteLine($"Free Physical Memory: {FormatBytes(freeKb * 1024)}");
                    }
                    return;
                }
#endif
                // Cross-platform fallback
                Console.WriteLine($"GC Total Memory (approx): {FormatBytes(GC.GetTotalMemory(false))}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Memory info error: {ex.Message}");
            }
        }

        static void PrintDiskInfo()
        {
            try
            {
                foreach (var d in DriveInfo.GetDrives().Where(x => x.IsReady))
                {
                    Console.WriteLine($"Drive: {d.Name} ({d.DriveType})");
                    Console.WriteLine($"  Volume Label: {d.VolumeLabel}");
                    Console.WriteLine($"  Format: {d.DriveFormat}");
                    Console.WriteLine($"  Total Size: {FormatBytes(d.TotalSize)}");
                    Console.WriteLine($"  Free Space: {FormatBytes(d.TotalFreeSpace)}");
                }

#if NET7_0_OR_GREATER
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var q = new ManagementObjectSearcher("SELECT Model, InterfaceType, Size, MediaType FROM Win32_DiskDrive");
                    foreach (ManagementObject mo in q.Get())
                    {
                        Console.WriteLine($"Disk Model: {mo["Model"]}");
                        Console.WriteLine($"  Interface: {mo["InterfaceType"]}");
                        if (mo["Size"] != null)
                        {
                            var size = Convert.ToUInt64(mo["Size"]);
                            Console.WriteLine($"  Size: {FormatBytes(size)}");
                        }
                        Console.WriteLine($"  MediaType: {mo["MediaType"]}");
                    }
                }
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Disk info error: {ex.Message}");
            }
        }

        static void PrintNetworkInfo()
        {
            try
            {
                var ifs = NetworkInterface.GetAllNetworkInterfaces();
                foreach (var ni in ifs)
                {
                    Console.WriteLine($"Name: {ni.Name}");
                    Console.WriteLine($"  Description: {ni.Description}");
                    Console.WriteLine($"  Type: {ni.NetworkInterfaceType}");
                    Console.WriteLine($"  Status: {ni.OperationalStatus}");
                    Console.WriteLine($"  Speed: {ni.Speed / 1_000_000.0} Mbps");
                    var ips = ni.GetIPProperties().UnicastAddresses.Select(a => a.Address.ToString()).ToArray();
                    if (ips.Length > 0)
                        Console.WriteLine($"  IPs: {string.Join(", ", ips)}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Network info error: {ex.Message}");
            }
        }

        static void PrintVideoControllers()
        {
            try
            {
#if NET7_0_OR_GREATER
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var q = new ManagementObjectSearcher("SELECT Name, AdapterRAM, DriverVersion, VideoProcessor FROM Win32_VideoController");
                    foreach (ManagementObject mo in q.Get())
                    {
                        Console.WriteLine($"Name: {mo["Name"]}");
                        if (mo["AdapterRAM"] != null)
                            Console.WriteLine($"  RAM: {FormatBytes(Convert.ToUInt64(mo["AdapterRAM"]))}");
                        Console.WriteLine($"  DriverVersion: {mo["DriverVersion"]}");
                        Console.WriteLine($"  VideoProcessor: {mo["VideoProcessor"]}");
                    }
                    return;
                }
#endif
                Console.WriteLine("Video controller info via WMI is only available on Windows with System.Management.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Video info error: {ex.Message}");
            }
        }

        static void PrintBiosAndBaseboard()
        {
            try
            {
#if NET7_0_OR_GREATER
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var biosQ = new ManagementObjectSearcher("SELECT Manufacturer, Name, SerialNumber, SMBIOSBIOSVersion FROM Win32_BIOS");
                    foreach (ManagementObject mo in biosQ.Get())
                    {
                        Console.WriteLine($"BIOS: {mo["Manufacturer"]} - {mo["Name"]}");
                        Console.WriteLine($"  Serial: {mo["SerialNumber"]}");
                        Console.WriteLine($"  Version: {mo["SMBIOSBIOSVersion"]}");
                    }

                    var baseQ = new ManagementObjectSearcher("SELECT Manufacturer, Product, SerialNumber FROM Win32_BaseBoard");
                    foreach (ManagementObject mo in baseQ.Get())
                    {
                        Console.WriteLine($"BaseBoard Manufacturer: {mo["Manufacturer"]}");
                        Console.WriteLine($"  Product: {mo["Product"]}");
                        Console.WriteLine($"  Serial: {mo["SerialNumber"]}");
                    }
                    return;
                }
#endif
                Console.WriteLine("BIOS/Baseboard info via WMI is only available on Windows with System.Management.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BIOS/Baseboard error: {ex.Message}");
            }
        }

        static void PrintInstalledSoftware()
        {
            try
            {
                if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Console.WriteLine("Installed software listing is implemented only for Windows.");
                    return;
                }

                var programs = GetInstalledPrograms();
                if (programs.Count == 0)
                {
                    Console.WriteLine("No installed programs found (or permissions prevented reading the registry).");
                    return;
                }

                foreach (var p in programs.OrderBy(x => x.Name).Take(200)) // limit output
                {
                    Console.WriteLine($"{p.Name} {p.Version} {(string.IsNullOrEmpty(p.Publisher) ? "" : $" - {p.Publisher}")}");
                }
                if (programs.Count > 200)
                    Console.WriteLine($"... ({programs.Count - 200} more entries truncated)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Installed software error: {ex.Message}");
            }
        }

        static List<InstalledProgram> GetInstalledPrograms()
        {
            var results = new List<InstalledProgram>();
            // Search both 32-bit and 64-bit registry hives
            string[] registryRoots = new[]
            {
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
                @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
            };

            foreach (var root in registryRoots)
            {
                using (var key = Registry.LocalMachine.OpenSubKey(root))
                {
                    if (key == null) continue;
                    foreach (var subName in key.GetSubKeyNames())
                    {
                        using (var sub = key.OpenSubKey(subName))
                        {
                            if (sub == null) continue;
                            var displayName = sub.GetValue("DisplayName") as string;
                            if (string.IsNullOrEmpty(displayName)) continue;
                            var version = sub.GetValue("DisplayVersion") as string ?? "";
                            var publisher = sub.GetValue("Publisher") as string ?? "";
                            results.Add(new InstalledProgram { Name = displayName.Trim(), Version = version.Trim(), Publisher = publisher.Trim() });
                        }
                    }
                }
            }

            // Also check current user uninstall (per-user installs)
            using (var cu = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
            {
                if (cu != null)
                {
                    foreach (var subName in cu.GetSubKeyNames())
                    {
                        using (var sub = cu.OpenSubKey(subName))
                        {
                            if (sub == null) continue;
                            var displayName = sub.GetValue("DisplayName") as string;
                            if (string.IsNullOrEmpty(displayName)) continue;
                            var version = sub.GetValue("DisplayVersion") as string ?? "";
                            var publisher = sub.GetValue("Publisher") as string ?? "";
                            results.Add(new InstalledProgram { Name = displayName.Trim(), Version = version.Trim(), Publisher = publisher.Trim() });
                        }
                    }
                }
            }

            return results;
        }

        static string FormatBytes(long bytes)
        {
            if (bytes < 0) return "Unknown";
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        static string FormatBytes(ulong bytes) => FormatBytes((long)bytes);

        class InstalledProgram
        {
            public string Name { get; set; }
            public string Version { get; set; }
            public string Publisher { get; set; }
        }
    }
}
