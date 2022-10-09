/*
?CreateModifiedExecutable@LLPE@@YA_NXZ
?CreateSymbolList@LLPE@@YA_NXZ
?GenerateDefinitionFiles@LLPE@@YA_NXZ
?GetEditorFilename@LLPE@@YAPEADH@Z
?GetFilteredFunctionListCount@LLPE@@YAHXZ
?ProcessFunctionList@LLPE@@YA_NXZ
?ProcessLibDirectory@LLPE@@YA_NPEBD@Z
?ProcessLibFile@LLPE@@YA_NPEBD@Z
?ProcessPlugins@LLPE@@YA_NXZ
?SetEditorFilename@LLPE@@YAXHPEBDH@Z
*/
using System.ComponentModel;
using System.Runtime.InteropServices;

public static class PeLibraryTests {
	enum LLFileTypes {
		VanillaExe,
		LiteModExe,
		BedrockPdb,
		ApiDef,
		VarDef,
		SymbolList
	}

	[DllImport("LLPeProviderService.dll", EntryPoint = "?CreateModifiedExecutable@LLPE@@YA_NXZ")]
    public static extern bool CreateModifiedExecutable();

	[DllImport("LLPeProviderService.dll", EntryPoint = "?CreateSymbolList@LLPE@@YA_NXZ")]
	public static extern bool CreateSymbolList();

	[DllImport("LLPeProviderService.dll", EntryPoint = "?GenerateDefinitionFiles@LLPE@@YA_NXZ")]
	public static extern bool GenerateDefinitionFiles();
	
	[DllImport("LLPeProviderService.dll", EntryPoint = "?GetEditorFilename@LLPE@@YAPEBDH@Z")]
	public static extern string GetEditorFilename(int LLFileType);

	[DllImport("LLPeProviderService.dll", EntryPoint = "?GetFilteredFunctionListCount@LLPE@@YAHXZ")]
	public static extern int GetFilteredFunctionListCount();
	
	[DllImport("LLPeProviderService.dll", EntryPoint = "?ProcessFunctionList@LLPE@@YA_NXZ")]
	public static extern bool ProcessFunctionList();

	[DllImport("LLPeProviderService.dll", EntryPoint = "?ProcessLibDirectory@LLPE@@YA_NPEBD@Z")]
	public static extern bool ProcessLibDirectory(string dirName);
	
	[DllImport("LLPeProviderService.dll", EntryPoint = "?ProcessLibFile@LLPE@@YA_NPEBD@Z")]
	public static extern bool ProcessLibFile(string filename);

	[DllImport("LLPeProviderService.dll", EntryPoint = "?ProcessPlugins@LLPE@@YA_NXZ")]
	public extern static bool ProcessPlugins();

	[DllImport("LLPeProviderService.dll", EntryPoint = "?SetEditorFilename@LLPE@@YAXHPEBD@Z")]
	public extern static void SetEditorFilename(int fileTpe, string filename);

	public static void RunTest() {
		Console.WriteLine("LL PE Library tests begin.");
		string filename = "Test.exe";
		SetEditorFilename((int)LLFileTypes.LiteModExe, filename);
		if (ProcessFunctionList()) {
			Console.WriteLine($"List returned a count of {GetFilteredFunctionListCount()} filtered symbols.");
		}
		Console.WriteLine($"Filename from lib returned: {GetEditorFilename((int)LLFileTypes.LiteModExe)}");
		Console.WriteLine(GetEditorFilename((int)LLFileTypes.VanillaExe));
		Console.WriteLine(GetEditorFilename((int)LLFileTypes.BedrockPdb));
		Console.WriteLine(GetEditorFilename((int)LLFileTypes.ApiDef));
		Console.WriteLine(GetEditorFilename((int)LLFileTypes.VarDef));
		Console.WriteLine(GetEditorFilename((int)LLFileTypes.SymbolList));
		GenerateDefinitionFiles();
		CreateSymbolList();
		CreateModifiedExecutable();
		ProcessPlugins();
    }

	public static void Main(string[] args) {
		RunTest();
	}
}
