using System;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;

namespace GroupedApp
{
    class InternetConnectivity
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public static bool CheckServerConnection()
        {
            return InternetGetConnectedState(out _, 0);
        }
    }
}
