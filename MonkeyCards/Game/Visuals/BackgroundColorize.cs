using System.Numerics;
using Raylib_cs;

namespace MonkeyCards.Game.Visuals;

public class BackgroundColorize
{
    private Shader _shader;
    private int _timeLoc;
    private int _resolutionLoc;

    private Rectangle _fullscreenRect;
    
    public void SetSettings()
    {
        _shader = Raylib.LoadShader(null, "Resources/Shaders/noise_background_shader.frag");
        
        var speedLoc = Raylib.GetShaderLocation(_shader, "speed");
        var scaleLoc = Raylib.GetShaderLocation(_shader, "scale");

        var color1Loc = Raylib.GetShaderLocation(_shader, "color1");
        var color2Loc = Raylib.GetShaderLocation(_shader, "color2");
        var color3Loc = Raylib.GetShaderLocation(_shader, "color3");
        var color4Loc = Raylib.GetShaderLocation(_shader, "color4");

        var threshold1Loc = Raylib.GetShaderLocation(_shader, "threshold1");
        var threshold2Loc = Raylib.GetShaderLocation(_shader, "threshold2");
        var threshold3Loc = Raylib.GetShaderLocation(_shader, "threshold3");

        Raylib.SetShaderValue(_shader, speedLoc, 0.1f, ShaderUniformDataType.Float);
        Raylib.SetShaderValue(_shader, scaleLoc, 1.7f, ShaderUniformDataType.Float);

        Raylib.SetShaderValue(_shader, color1Loc, new Vector3(0.22f, 0.50f, 0.36f), ShaderUniformDataType.Vec3);
        Raylib.SetShaderValue(_shader, color2Loc, new Vector3(0.22f, 0.55f, 0.36f), ShaderUniformDataType.Vec3);
        Raylib.SetShaderValue(_shader, color3Loc, new Vector3(0.22f, 0.58f, 0.36f), ShaderUniformDataType.Vec3);
        Raylib.SetShaderValue(_shader, color4Loc, new Vector3(0.22f, 0.64f, 0.36f), ShaderUniformDataType.Vec3);

        Raylib.SetShaderValue(_shader, threshold1Loc, 0.52f, ShaderUniformDataType.Float);
        Raylib.SetShaderValue(_shader, threshold2Loc, 0.55f, ShaderUniformDataType.Float);
        Raylib.SetShaderValue(_shader, threshold3Loc, 0.75f, ShaderUniformDataType.Float);

        _timeLoc = Raylib.GetShaderLocation(_shader, "time");
        _resolutionLoc = Raylib.GetShaderLocation(_shader, "resolution");
        
        _fullscreenRect = new Rectangle(0, 0, 800, 600);
    }

    public void BeforeDrawing()
    {
        var time = Raylib.GetTime();
        var resolution = new Vector2(800, 600);

        Raylib.SetShaderValue(_shader, _timeLoc, new float[] { (float) time }, ShaderUniformDataType.Float);
        Raylib.SetShaderValue(_shader, _resolutionLoc, new float[] { resolution.X, resolution.Y }, ShaderUniformDataType.Vec2);
    }

    public void Draw()
    {
        Raylib.BeginShaderMode(_shader);
        Raylib.DrawRectangleRec(_fullscreenRect, Color.White);
        Raylib.EndShaderMode();
    }

    public void UnloadShader() => Raylib.UnloadShader(_shader);
    static BackgroundColorize() => Instance = new();
    public static BackgroundColorize Instance { get; private set; }
}