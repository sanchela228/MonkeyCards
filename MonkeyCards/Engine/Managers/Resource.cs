using Raylib_cs;

namespace MonkeyCards.Engine.Managers;

public class Resource
{
    private static Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
    private static Dictionary<string, Font> _fonts = new Dictionary<string, Font>();
    
    public void LoadTexture( string[] values )
    {
        
    }
    
    public void LoadTexture( string value )
    {
        
    }
    
    public void LoadFont( string[] values )
    {
        
    }
    
    public void LoadFont( string value )
    {
        
    }
    
    public static void Unload(string[] values)
    {
       
    }
    
    public static void UnloadAll()
    {
        foreach (var texture in _textures.Values)
            Raylib.UnloadTexture(texture);
            
        foreach (var font in _fonts.Values)
            Raylib.UnloadFont(font);
            
        _textures.Clear();
        _fonts.Clear();
    }
}