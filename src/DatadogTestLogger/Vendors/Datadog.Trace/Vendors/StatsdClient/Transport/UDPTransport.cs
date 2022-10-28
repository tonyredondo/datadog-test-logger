//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
using System.Net;
using System.Net.Sockets;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.StatsdClient.Transport
{
    internal class UDPTransport : ITransport
    {
        private readonly Socket _socket;
        private readonly IPEndPoint _endPoint;

        public UDPTransport(IPEndPoint endPoint)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            try
            {
                // When closing, wait 2 seconds to send data.
                _socket.LingerState = new LingerOption(true, 2);
            }
            catch (SocketException e) when (e.SocketErrorCode == SocketError.ProtocolOption)
            {
                // It is not supported on Windows for Dgram with UDP.
            }

            _endPoint = endPoint;
        }

        public TransportType TransportType => TransportType.UDP;

        public string TelemetryClientTransport => "udp";

        /// <summary>
        /// Send the buffer.
        /// Must be thread safe.
        /// </summary>
        public bool Send(byte[] buffer, int length)
        {
            _socket.SendTo(buffer, 0, length, SocketFlags.None, _endPoint);
            return true;
        }

        public void Dispose()
        {
            _socket.Dispose();
        }
    }
}