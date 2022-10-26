//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, SYSLIB0011,SYSLIB0032
// dnlib: See LICENSE.txt for more info

namespace Vendor.Datadog.Trace.Vendors.dnlib.DotNet {
	/// <summary>
	/// Any ALG_CLASS_HASH type in WinCrypt.h can be used by Microsoft's CLI implementation
	/// </summary>
	internal enum AssemblyHashAlgorithm : uint {
		/// <summary/>
		None				= 0,
		/// <summary/>
		MD2					= 0x8001,
		/// <summary/>
		MD4					= 0x8002,
		/// <summary>This is a reserved value in the CLI</summary>
		MD5					= 0x8003,
		/// <summary>The only algorithm supported by the CLI</summary>
		SHA1				= 0x8004,
		/// <summary/>
		MAC					= 0x8005,
		/// <summary/>
		SSL3_SHAMD5			= 0x8008,
		/// <summary/>
		HMAC				= 0x8009,
		/// <summary/>
		TLS1PRF				= 0x800A,
		/// <summary/>
		HASH_REPLACE_OWF	= 0x800B,
		/// <summary/>
		SHA_256				= 0x800C,
		/// <summary/>
		SHA_384				= 0x800D,
		/// <summary/>
		SHA_512				= 0x800E,
	}

	internal static partial class Extensions {
		internal static string GetName(this AssemblyHashAlgorithm hashAlg) =>
			hashAlg switch {
				AssemblyHashAlgorithm.MD2 => null,
				AssemblyHashAlgorithm.MD4 => null,
				AssemblyHashAlgorithm.MD5 => "MD5",
				AssemblyHashAlgorithm.SHA1 => "SHA1",
				AssemblyHashAlgorithm.MAC => null,
				AssemblyHashAlgorithm.SSL3_SHAMD5 => null,
				AssemblyHashAlgorithm.HMAC => null,
				AssemblyHashAlgorithm.TLS1PRF => null,
				AssemblyHashAlgorithm.HASH_REPLACE_OWF => null,
				AssemblyHashAlgorithm.SHA_256 => "SHA256",
				AssemblyHashAlgorithm.SHA_384 => "SHA384",
				AssemblyHashAlgorithm.SHA_512 => "SHA512",
				_ => null,
			};
	}
}
