
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

	[DllImport("LLPeProviderService.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?SetEditorFilename@LLPE@@YAXHPEBDH@Z")]
	public extern static void SetEditorFilename(int fileTpe, char[] filenameCharArray, int filenameCharCount);

	[DllImport("LLPeProviderService.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?ProcessPlugins@LLPE@@YA_NXZ")]
	public extern static bool ProcessPlugins();

	public static void RunTest() {
		Console.ReadKey();
		char[] filenameArray = "Test.exe".ToCharArray();
		SetEditorFilename((int)LLFileTypes.LiteModExe, filenameArray, filenameArray.Length);
		ProcessPlugins();
		CreateModifiedExecutable();
    }

	public static void Main(string[] args) {
		RunTest();
	}
}
