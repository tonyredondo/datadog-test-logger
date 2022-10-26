//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, SYSLIB0011,SYSLIB0032
// dnlib: See LICENSE.txt for more info

namespace Vendor.Datadog.Trace.Vendors.dnlib.DotNet.Pdb.Managed {
	enum SymbolType : ushort {
		S_COMPILE = 0x0001,
		S_REGISTER_16t = 0x0002,
		S_CONSTANT_16t = 0x0003,
		S_UDT_16t = 0x0004,
		S_SSEARCH = 0x0005,
		S_END = 0x0006,
		S_SKIP = 0x0007,
		S_CVRESERVE = 0x0008,
		S_OBJNAME_ST = 0x0009,
		S_ENDARG = 0x000A,
		S_COBOLUDT_16t = 0x000B,
		S_MANYREG_16t = 0x000C,
		S_RETURN = 0x000D,
		S_ENTRYTHIS = 0x000E,

		S_BPREL16 = 0x0100,
		S_LDATA16 = 0x0101,
		S_GDATA16 = 0x0102,
		S_PUB16 = 0x0103,
		S_LPROC16 = 0x0104,
		S_GPROC16 = 0x0105,
		S_THUNK16 = 0x0106,
		S_BLOCK16 = 0x0107,
		S_WITH16 = 0x0108,
		S_LABEL16 = 0x0109,
		S_CEXMODEL16 = 0x010A,
		S_VFTABLE16 = 0x010B,
		S_REGREL16 = 0x010C,

		S_BPREL32_16t = 0x0200,
		S_LDATA32_16t = 0x0201,
		S_GDATA32_16t = 0x0202,
		S_PUB32_16t = 0x0203,
		S_LPROC32_16t = 0x0204,
		S_GPROC32_16t = 0x0205,
		S_THUNK32_ST = 0x0206,
		S_BLOCK32_ST = 0x0207,
		S_WITH32_ST = 0x0208,
		S_LABEL32_ST = 0x0209,
		S_CEXMODEL32 = 0x020A,
		S_VFTABLE32_16t = 0x020B,
		S_REGREL32_16t = 0x020C,
		S_LTHREAD32_16t = 0x020D,
		S_GTHREAD32_16t = 0x020E,
		S_SLINK32 = 0x020F,

		S_LPROCMIPS_16t = 0x0300,
		S_GPROCMIPS_16t = 0x0301,

		S_PROCREF_ST = 0x0400,
		S_DATAREF_ST = 0x0401,
		S_ALIGN = 0x0402,
		S_LPROCREF_ST = 0x0403,
		S_OEM = 0x0404,

		S_TI16_MAX = 0x1000,
		S_REGISTER_ST = 0x1001,
		S_CONSTANT_ST = 0x1002,
		S_UDT_ST = 0x1003,
		S_COBOLUDT_ST = 0x1004,
		S_MANYREG_ST = 0x1005,
		S_BPREL32_ST = 0x1006,
		S_LDATA32_ST = 0x1007,
		S_GDATA32_ST = 0x1008,
		S_PUB32_ST = 0x1009,
		S_LPROC32_ST = 0x100A,
		S_GPROC32_ST = 0x100B,
		S_VFTABLE32 = 0x100C,
		S_REGREL32_ST = 0x100D,
		S_LTHREAD32_ST = 0x100E,
		S_GTHREAD32_ST = 0x100F,
		S_LPROCMIPS_ST = 0x1010,
		S_GPROCMIPS_ST = 0x1011,
		S_FRAMEPROC = 0x1012,
		S_COMPILE2_ST = 0x1013,
		S_MANYREG2_ST = 0x1014,
		S_LPROCIA64_ST = 0x1015,
		S_GPROCIA64_ST = 0x1016,
		S_LOCALSLOT_ST = 0x1017,
		S_PARAMSLOT_ST = 0x1018,
		S_ANNOTATION = 0x1019,
		S_GMANPROC_ST = 0x101A,
		S_LMANPROC_ST = 0x101B,
		S_RESERVED1 = 0x101C,
		S_RESERVED2 = 0x101D,
		S_RESERVED3 = 0x101E,
		S_RESERVED4 = 0x101F,
		S_LMANDATA_ST = 0x1020,
		S_GMANDATA_ST = 0x1021,
		S_MANFRAMEREL_ST = 0x1022,
		S_MANREGISTER_ST = 0x1023,
		S_MANSLOT_ST = 0x1024,
		S_MANMANYREG_ST = 0x1025,
		S_MANREGREL_ST = 0x1026,
		S_MANMANYREG2_ST = 0x1027,
		S_MANTYPREF = 0x1028,
		S_UNAMESPACE_ST = 0x1029,

		S_ST_MAX = 0x1100,
		S_OBJNAME = 0x1101,
		S_THUNK32 = 0x1102,
		S_BLOCK32 = 0x1103,
		S_WITH32 = 0x1104,
		S_LABEL32 = 0x1105,
		S_REGISTER = 0x1106,
		S_CONSTANT = 0x1107,
		S_UDT = 0x1108,
		S_COBOLUDT = 0x1109,
		S_MANYREG = 0x110A,
		S_BPREL32 = 0x110B,
		S_LDATA32 = 0x110C,
		S_GDATA32 = 0x110D,
		S_PUB32 = 0x110E,
		S_LPROC32 = 0x110F,
		S_GPROC32 = 0x1110,
		S_REGREL32 = 0x1111,
		S_LTHREAD32 = 0x1112,
		S_GTHREAD32 = 0x1113,
		S_LPROCMIPS = 0x1114,
		S_GPROCMIPS = 0x1115,
		S_COMPILE2 = 0x1116,
		S_MANYREG2 = 0x1117,
		S_LPROCIA64 = 0x1118,
		S_GPROCIA64 = 0x1119,
		S_LOCALSLOT = 0x111A,
		S_PARAMSLOT = 0x111B,
		S_LMANDATA = 0x111C,
		S_GMANDATA = 0x111D,
		S_MANFRAMEREL = 0x111E,
		S_MANREGISTER = 0x111F,
		S_MANSLOT = 0x1120,
		S_MANMANYREG = 0x1121,
		S_MANREGREL = 0x1122,
		S_MANMANYREG2 = 0x1123,
		S_UNAMESPACE = 0x1124,
		S_PROCREF = 0x1125,
		S_DATAREF = 0x1126,
		S_LPROCREF = 0x1127,
		S_ANNOTATIONREF = 0x1128,
		S_TOKENREF = 0x1129,
		S_GMANPROC = 0x112A,
		S_LMANPROC = 0x112B,
		S_TRAMPOLINE = 0x112C,
		S_MANCONSTANT = 0x112D,
		S_ATTR_FRAMEREL = 0x112E,
		S_ATTR_REGISTER = 0x112F,
		S_ATTR_REGREL = 0x1130,
		S_ATTR_MANYREG = 0x1131,
		S_SEPCODE = 0x1132,
		S_LOCAL_2005 = 0x1133,
		S_DEFRANGE_2005 = 0x1134,
		S_DEFRANGE2_2005 = 0x1135,
		S_SECTION = 0x1136,
		S_COFFGROUP = 0x1137,
		S_EXPORT = 0x1138,
		S_CALLSITEINFO = 0x1139,
		S_FRAMECOOKIE = 0x113A,
		S_DISCARDED = 0x113B,
		S_COMPILE3 = 0x113C,
		S_ENVBLOCK = 0x113D,
		S_LOCAL = 0x113E,
		S_DEFRANGE = 0x113F,
		S_DEFRANGE_SUBFIELD = 0x1140,
		S_DEFRANGE_REGISTER = 0x1141,
		S_DEFRANGE_FRAMEPOINTER_REL = 0x1142,
		S_DEFRANGE_SUBFIELD_REGISTER = 0x1143,
		S_DEFRANGE_FRAMEPOINTER_REL_FULL_SCOPE = 0x1144,
		S_DEFRANGE_REGISTER_REL = 0x1145,
		S_LPROC32_ID = 0x1146,
		S_GPROC32_ID = 0x1147,
		S_LPROCMIPS_ID = 0x1148,
		S_GPROCMIPS_ID = 0x1149,
		S_LPROCIA64_ID = 0x114A,
		S_GPROCIA64_ID = 0x114B,
		S_BUILDINFO = 0x114C,
		S_INLINESITE = 0x114D,
		S_INLINESITE_END = 0x114E,
		S_PROC_ID_END = 0x114F,
		S_DEFRANGE_HLSL = 0x1150,
		S_GDATA_HLSL = 0x1151,
		S_LDATA_HLSL = 0x1152,
		S_FILESTATIC = 0x1153,
		S_LOCAL_DPC_GROUPSHARED = 0x1154,
		S_LPROC32_DPC = 0x1155,
		S_LPROC32_DPC_ID = 0x1156,
		S_DEFRANGE_DPC_PTR_TAG = 0x1157,
		S_DPC_SYM_TAG_MAP = 0x1158,
		S_ARMSWITCHTABLE = 0x1159,
		S_CALLEES = 0x115A,
		S_CALLERS = 0x115B,
		S_POGODATA = 0x115C,
		S_INLINESITE2 = 0x115D,
		S_HEAPALLOCSITE = 0x115E,

		S_RECTYPE_MAX,
	}
}
