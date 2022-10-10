using System.Runtime.InteropServices;

public static class PeLibraryTests {
	public enum LLFileTypes {
		VanillaExe,
		LiteModExe,
		BedrockPdb,
		ApiDef,
		VarDef,
		SymbolList
	}

	public static List<KeyValuePair<LLFileTypes, string>> filenameDefaultsLookup = new List<KeyValuePair<LLFileTypes, string>> {
		new KeyValuePair<LLFileTypes, string> ((LLFileTypes)LLFileTypes.VanillaExe, "bedrock_server.exe"),
		new KeyValuePair<LLFileTypes, string> ((LLFileTypes)LLFileTypes.LiteModExe, "bedrock_server_mod.exe"),
		new KeyValuePair<LLFileTypes, string> ((LLFileTypes)LLFileTypes.BedrockPdb, "bedrock_server.pdb"),
		new KeyValuePair<LLFileTypes, string> ((LLFileTypes)LLFileTypes.ApiDef, "bedrock_server_api.def"),
		new KeyValuePair<LLFileTypes, string> ((LLFileTypes)LLFileTypes.VarDef, "bedrock_server_var.def"),
		new KeyValuePair<LLFileTypes, string> ((LLFileTypes)LLFileTypes.SymbolList, "bedrock_server_symbols.txt")
	};

	public static string GetFilenameLookup(LLFileTypes type) {
		return filenameDefaultsLookup.First(x => x.Key == type).Value;
    }

	[DllImport("LLPeProviderService.dll", EntryPoint = "?ProcessFunctionList@LLPE@@YA_NPEBD@Z")]
	public static extern bool ProcessFunctionList(string pdbFile);

	[DllImport("LLPeProviderService.dll", EntryPoint = "?CreateSymbolList@LLPE@@YA_NPEBD0@Z")]
	public static extern bool CreateSymbolList(string symFileName, string pdbFile);

	[DllImport("LLPeProviderService.dll", EntryPoint = "?ProcessLibFile@LLPE@@YA_NPEBD0@Z")]
	public static extern bool ProcessLibFile(string libName, string modExeName);

	[DllImport("LLPeProviderService.dll", EntryPoint = "?ProcessLibDirectory@LLPE@@YA_NPEBD0@Z")]
	public static extern bool ProcessLibDirectory(string directoryName, string modExeName);

	[DllImport("LLPeProviderService.dll", EntryPoint = "?ProcessPlugins@LLPE@@YA_NPEBD@Z")]
	public static extern bool ProcessPlugins(string modExeName);

	[DllImport("LLPeProviderService.dll", EntryPoint = "?GenerateDefinitionFiles@LLPE@@YA_NPEBD00@Z")]
	public static extern bool GenerateDefinitionFiles(string pdbName, string apiName, string varName);

	[DllImport("LLPeProviderService.dll", EntryPoint = "?CreateModifiedExecutable@LLPE@@YA_NPEBD00@Z")]
	public static extern bool CreateModifiedExecutable(string bedrockName, string liteModName, string pdbName);

	[DllImport("LLPeProviderService.dll", EntryPoint = "?GetFilteredFunctionListCount@LLPE@@YAHPEBD@Z")]
	public static extern int GetFilteredFunctionListCount(string pdbName);

	public static void RunTest() {
		Console.WriteLine("LL PE Library tests begin.");
		string filename = "Test.exe";

		if (ProcessFunctionList(GetFilenameLookup(LLFileTypes.BedrockPdb))) {
			Console.WriteLine($"List returned a count of {GetFilteredFunctionListCount(GetFilenameLookup(LLFileTypes.VanillaExe))} filtered symbols.");
		}

		CreateSymbolList("symTest.def",	GetFilenameLookup(LLFileTypes.BedrockPdb));

		GenerateDefinitionFiles(GetFilenameLookup(LLFileTypes.BedrockPdb), "apiTest.def", "varTest.def");

		GenerateDefinitionFiles(GetFilenameLookup(LLFileTypes.BedrockPdb), GetFilenameLookup(LLFileTypes.ApiDef), GetFilenameLookup(LLFileTypes.VarDef));

		ProcessPlugins(filename);

		CreateModifiedExecutable(GetFilenameLookup(LLFileTypes.VanillaExe), filename, GetFilenameLookup(LLFileTypes.BedrockPdb));

		CreateSymbolList(GetFilenameLookup(LLFileTypes.SymbolList), GetFilenameLookup(LLFileTypes.BedrockPdb));
    }

	public static void Main(string[] args) {
		RunTest();
	}
}
