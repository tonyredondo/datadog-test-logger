//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.StatsdClient
{
    internal class StatsdUDP
    {
        internal static IPAddress GetIpv4Address(string name)
        {
            IPAddress ipAddress;
            bool isValidIPAddress = IPAddress.TryParse(name, out ipAddress);

            if (!isValidIPAddress)
            {
                ipAddress = null;
#if NET45
                IPAddress[] addressList = Dns.GetHostEntry(name).AddressList;
#else
                IPAddress[] addressList = Dns.GetHostEntryAsync(name).Result.AddressList;
#endif
                // The IPv4 address is usually the last one, but not always
                for (int positionToTest = addressList.Length - 1; positionToTest >= 0; --positionToTest)
                {
                    if (addressList[positionToTest].AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = addressList[positionToTest];
                        break;
                    }
                }

                // If no IPV4 address is found, throw an exception here, rather than letting it get squashed when encountered at sendtime
                if (ipAddress == null)
                {
                    throw new SocketException((int)SocketError.AddressFamilyNotSupported);
                }
            }

            return ipAddress;
        }
    }
}
