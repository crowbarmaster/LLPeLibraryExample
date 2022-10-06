
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

	[DllImport("LLPeProviderService.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?CreateModifiedExecutable@LLPE@@YA_NXZ")]
    public static extern bool CreateModifiedExecutable();

	[DllImport("LLPeProviderService.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?SetEditorFilename@LLPE@@YAXHPEBD@Z")]
	public extern static void SetEditorFilename(int fileTpe, char[] filenameCharArray);

	[DllImport("LLPeProviderService.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?ProcessPlugins@LLPE@@YA_NXZ")]
	public extern static bool ProcessPlugins();

	public static void RunTest() {
		SetEditorFilename((int)LLFileTypes.LiteModExe, "BedrockService.Test.exe".ToCharArray());
		ProcessPlugins();
		CreateModifiedExecutable();
    }

	public static void Main(string[] args) {
		RunTest();
	}
}
