//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------

namespace Vendor.Datadog.Trace.Vendors.MessagePack.Formatters
{
    // marker
    internal interface IMessagePackFormatter
    {

    }

    internal interface IMessagePackFormatter<T> : IMessagePackFormatter
    {
        int Serialize(ref byte[] bytes, int offset, T value, IFormatterResolver formatterResolver);
        T Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize);
    }
}
