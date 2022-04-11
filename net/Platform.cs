using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Identity.Client.NativeInterop
{
    internal enum OperatingSystemType
    {
        Windows,
        Unix,
        MacOSX
    }

    internal static class Platform
    {
        public static string ProcessorArchitecture => RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant();

        public static OperatingSystemType OperatingSystem
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return OperatingSystemType.Windows;
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return OperatingSystemType.Unix;
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return OperatingSystemType.MacOSX;
                }

                throw new PlatformNotSupportedException();
            }
        }

        public static string GetNativeLibraryExtension()
        {
            switch (OperatingSystem)
            {
                case OperatingSystemType.MacOSX:
                    return ".dylib";

                case OperatingSystemType.Unix:
                    return ".so";

                case OperatingSystemType.Windows:
                    return ".dll";
            }

            throw new PlatformNotSupportedException();
        }

        /// <summary>
        /// Returns true if the runtime is Mono.
        /// </summary>
        public static bool IsRunningOnMono()
            => Type.GetType("Mono.Runtime") != null;

        /// <summary>
        /// Returns true if the runtime is .NET Framework.
        /// </summary>
        public static bool IsRunningOnNetFramework()
            => typeof(object).Assembly.GetName().Name == "mscorlib" && !IsRunningOnMono();

        /// <summary>
        /// Returns true if the runtime is .NET Core.
        /// </summary>
        public static bool IsRunningOnNetCore()
            => typeof(object).Assembly.GetName().Name != "mscorlib";

        public static string GetExecutingAssemblyDirectory()
        {
            // Assembly.CodeBase is not actually a correctly formatted
            // URI.  It's merely prefixed with `file:///` and has its
            // backslashes flipped.  This is superior to EscapedCodeBase,
            // which does not correctly escape things, and ambiguates a
            // space (%20) with a literal `%20` in the path.  Sigh.
            var managedPath = Assembly.GetExecutingAssembly().CodeBase;
            if (managedPath == null)
            {
                managedPath = Assembly.GetExecutingAssembly().Location;
            }
            else if (managedPath.StartsWith("file:///"))
            {
                managedPath = managedPath.Substring(8).Replace('/', '\\');
            }
            else if (managedPath.StartsWith("file://"))
            {
                managedPath = @"\\" + managedPath.Substring(7).Replace('/', '\\');
            }

            managedPath = Path.GetDirectoryName(managedPath);
            return managedPath;
        }
    }
}
