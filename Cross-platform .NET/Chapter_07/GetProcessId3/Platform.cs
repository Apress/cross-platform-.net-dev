//Platform.cs
using System;

namespace Crossplatform.NET.Chapter7
{
    public abstract class Platform
    {
        private Platform currentPlatform;

        static Platform()
        {
           if (IsWindows())
                currentPlatform = new WinPlatform();
            else
                currentPlatform = new UnixPlatform();
        }

        protected Platform(){}
        
        public static Platform CurrentPlatform
        {
           get { return currentPlatform; }
        }
        
        public abstract int GetProcessId();
        
        //Provide a cheap way of seeing if we're on windows
        private static bool IsWindows()
        {
            PlatformID platform = Environment.OSVersion.Platform;
	    
            return (platform == PlatformID.Win32NT || platform == PlatformID.Win32S
                    || platform == PlatformID.Win32Windows
                    || platform == PlatformID.WinCE);    
	}
    }
}
