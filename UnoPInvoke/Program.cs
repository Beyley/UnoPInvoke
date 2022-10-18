using System.Runtime.InteropServices;

namespace UnoPInvoke; 

public class Program {
	[DllImport("__Internal_emscripten")]
	public static extern void emscripten_console_log(string str);
	
	public static void Main(string[] args) {
		emscripten_console_log("teststststs");
	}
}