using Raylib_cs;

namespace MonkeyCards.Engine.Managers;

public class Resources
{
    public string RootFolderPath {get; set;} = "Resources";
    
    private static Resources _instance;
    public static Resources Instance => _instance ??= new Resources();
    
    private readonly Dictionary<string, object> _resources = new();
    
    private static readonly Dictionary<Type, Func<string, object>> Loaders = new()
    {
        { typeof(Texture2D), path => Raylib.LoadTexture(path) },
        { typeof(Font), path => Raylib.LoadFont(path) }
    };
    
    private static string GetSubfolder<T>()
    {
        return typeof(T) switch
        {
            Type t when t == typeof(Texture2D) => "Textures",
            Type t when t == typeof(Font) => "Fonts",
            _ => ""
        };
    }
    
    public T Load<T>(string relativePath)
    {
        string fullPath = Path.Combine(RootFolderPath, GetSubfolder<T>(), relativePath);

        if (!Loaders.TryGetValue(typeof(T), out var loader))
            throw new NotSupportedException($"Loader for type {typeof(T)} not found");

        var resource = (T) loader(fullPath);
        _resources[fullPath] = resource;

        return resource;
    }
    
    public T Get<T>(string relativePath)
    {
        string subfolder = GetSubfolder<T>();
        string fullPath = string.IsNullOrEmpty(subfolder) ? Path.Combine(RootFolderPath, relativePath) : Path.Combine(RootFolderPath, subfolder, relativePath);

        if (_resources.TryGetValue(fullPath, out var resource))
            return (T)resource;

        if (!Loaders.ContainsKey(typeof(T)))
            throw new NotSupportedException($"No loader registered for type {typeof(T)}");

        return Load<T>(relativePath);
    }
  
    public Texture2D Texture(string relativePath) => Get<Texture2D>(relativePath);
    public Font Font(string relativePath) => Get<Font>(relativePath);
    
    public Font FontEx(string relativePath, int fontSize, int[]? fontChars = null, int charCount = 0)
    {
        string fullPath = Path.Combine(RootFolderPath, "Fonts", relativePath);

        if (_resources.TryGetValue(fullPath, out var cachedFont))
            return (Font)cachedFont;

        Font font = Raylib.LoadFontEx(fullPath, fontSize, fontChars, charCount);
        _resources[fullPath] = font;
        return font;
    }
    
    public void Unload<T>(string relativePath)
    {
        string fullPath = Path.Combine(RootFolderPath, GetSubfolder<T>(), relativePath);

        if (_resources.TryGetValue(fullPath, out var resource))
        {
            if (resource is Texture2D texture)
                Raylib.UnloadTexture(texture);
            else if (resource is Font font)
                Raylib.UnloadFont(font);

            _resources.Remove(fullPath);
        }
    }
    
    public bool Exists<T>(string relativePath)
    {
        string fullPath = Path.Combine(RootFolderPath, GetSubfolder<T>(), relativePath);
        return File.Exists(fullPath);
    }
}