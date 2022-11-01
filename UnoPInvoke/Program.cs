using System.Runtime.InteropServices;
using Silk.NET.Core.Contexts;
using Silk.NET.Core.Loader;
using Silk.NET.Input;
using Silk.NET.Input.Sdl;
using Silk.NET.OpenGL;
using Silk.NET.SDL;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Sdl;
using Uno.Foundation;
using Window = Silk.NET.Windowing.Window;

namespace UnoPInvoke;

public class Program {
	public static unsafe void Main(string[] args) {
		try {
			Console.WriteLine($"OS description: {RuntimeInformation.OSDescription} ({RuntimeInformation.OSArchitecture})");

			Console.WriteLine($"{RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER"))}");
			
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
document.body.appendChild(canvas);

//Set the module canvas
Module['canvas'] = canvas;
");

			SdlWindowing.RegisterPlatform();
			SdlInput.RegisterPlatform();

			ViewOptions options = ViewOptions.Default;

			options.FramesPerSecond = 60;
			options.UpdatesPerSecond = 60;

			options.API = new GraphicsAPI(ContextAPI.OpenGLES, ContextProfile.Core, ContextFlags.Debug, new APIVersion(3, 0));
			
			IView window = Window.GetView(options);

			GL gl = null;
			window.Load += () => {
				IInputContext inputContext = window.CreateInput();

				foreach (IKeyboard kb in inputContext.Keyboards) {
					kb.KeyDown += (keyboard, key, arg3) => {
						Console.WriteLine($"{keyboard.Name} pressed {key}");
					};
				}
				
				gl = window.CreateOpenGL();
			};

			window.Update += d => {
				Console.WriteLine($"update delta: {d * 1000d}ms");
			};

			window.Render += d => {
				Console.WriteLine($"render delta: {d * 1000d}ms");
				
				gl.ClearColor(0.5f, 0.5f, 0.5f, 1f);
				gl.Clear(ClearBufferMask.ColorBufferBit);
			};
			
			window.Run();
		}
		catch (Exception ex) {
			Console.WriteLine(ex.ToString());
		}
	}
}
