using System.Runtime.InteropServices;

namespace UnoPInvoke; 

public class Program {
	[DllImport("SDL")]
	public static extern int SDL_Init(int flags);
	
	[DllImport("SDL", CharSet = CharSet.Ansi)]
	public static extern IntPtr SDL_CreateWindow(string title, int x, int y, int w, int h, int flags);
	
	public static void Main(string[] args) {
		Console.WriteLine($"please... {SDL_Init(0)}");

		IntPtr window = SDL_CreateWindow("omg", 0, 0, 800, 600, 0);
		
		
		while(true) {}
	}
}