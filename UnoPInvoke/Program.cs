using System.Runtime.InteropServices;
using Silk.NET.SDL;
using Uno.Foundation;

namespace UnoPInvoke; 

public class Program {
	public static unsafe void Main(string[] args) {
		Sdl sdl = Sdl.GetApi();

		WebAssemblyRuntime.InvokeJS(@"canvas = document.createElement('canvas');
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
		
		Console.WriteLine($"please... {sdl.Init(0)}");

		Window* window = sdl.CreateWindow("omg", 0, 0, 800, 600, 0);
		
		sdl.Quit();
	}
}