using System.Runtime.InteropServices;
using Silk.NET.Core.Contexts;
using Silk.NET.Core.Loader;
using Silk.NET.SDL;
using Uno.Foundation;

namespace UnoPInvoke;

public class Program {
	[DllImport("__Internal_emscripten")]
	public static extern unsafe int dlopen(string fileName, int flags);
	
	[DllImport("__Internal_emscripten")]
	public static extern nint dlsym(nint handle, string name);
	
	public static unsafe void Main(string[] args) {
		try {
			Console.WriteLine($"OS description: {RuntimeInformation.OSDescription} ({RuntimeInformation.OSArchitecture})");

			Console.WriteLine($"{RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER"))}");
			
			Console.WriteLine($"{LibraryLoader.GetPlatformDefaultLoader().GetType()}");

			nint dlopen1 = dlopen(null, 0);
			
			Console.WriteLine($"dlopen1: {(nuint)dlopen1}");
			
			nint dlsym1 = dlsym(dlopen1, "SDL_Init");
			
			Console.WriteLine($"dlsym1: {(nuint)dlsym1}");

			WebAssemblyRuntime.InvokeJS(@"var canvas = document.createElement('canvas');
canvas.style.position = ""absolute"";
canvas.style.left       = ""0px"";
canvas.style.top        = ""0px"";
canvas.style.zIndex     = ""100"";
canvas.style.width      = ""1920"";
canvas.style.height     = ""1080"";
canvas.style.width = ""100%"";
canvas.style.height = ""100%"";
canvas.oncontextmenu=""event.preventDefault()"";
canvas.id = 'canvas';
document.body.appendChild(canvas);");

			// SDL_Init(0);
			
			Sdl sdl = new Sdl(new DefaultNativeContext("SDL"));

			Console.WriteLine($"please... {sdl.Init(0)}");

			Window* window = sdl.CreateWindow("omg", 0, 0, 800, 600, 0);

			bool run = true;
			while (run) {
				
			}

			sdl.Quit();
		}
		catch (Exception ex) {
			Console.WriteLine(ex.ToString());
		}
	}
}
