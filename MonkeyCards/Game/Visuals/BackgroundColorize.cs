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
        
        var colorsLoc = Raylib.GetShaderLocation(_shader, "colors");
        var thresholdsLoc = Raylib.GetShaderLocation(_shader, "thresholds");
        
        float[] thresholds = new float[]
        {
            0.39f,
            0.40f,
            0.41f,
            0.42f,
            0.43f,
            0.44f,
            0.45f,
            0.46f,
            0.47f,
            0.48f,
            0.49f,
            0.55f,
            0.56f,
            0.57f,
            0.58f,
            0.59f,
            0.60f,
            0.61f,
            0.62f,
            0.63f,
            0.64f,
            0.65f,
            0.66f,
            0.67f,
            0.68f,
            0.69f,
            0.70f,
            0.71f,
            0.72f,
            0.73f,
            0.74f,
            0.75f,
        };
        Vector3[] colors = new Vector3[] {
            new Vector3(0.32f, 0.45f, 0.86f),
            new Vector3(0.32f, 0.48f, 0.86f),
            new Vector3(0.32f, 0.5f,  0.86f),
            new Vector3(0.32f, 0.55f, 0.86f),
            new Vector3(0.32f, 0.6f,  0.86f),
            new Vector3(0.32f, 0.7f,  0.86f),
            new Vector3(0.32f, 0.77f, 0.86f),
            new Vector3(0.32f, 0.79f, 0.86f),
            new Vector3(0.32f, 0.8f,  0.86f),
            new Vector3(0.32f, 0.79f, 0.86f),
            new Vector3(0.32f, 0.77f, 0.86f),
            new Vector3(0.32f, 0.7f,  0.86f),
            new Vector3(0.32f, 0.6f,  0.86f),
            new Vector3(0.32f, 0.55f, 0.86f),
            new Vector3(0.32f, 0.5f,  0.86f),
            new Vector3(0.32f, 0.48f, 0.86f),
            new Vector3(0.32f, 0.45f, 0.76f),
            new Vector3(0.32f, 0.48f, 0.76f),
            new Vector3(0.32f, 0.5f,  0.76f),
            new Vector3(0.32f, 0.55f, 0.76f),
            new Vector3(0.32f, 0.6f,  0.76f),
            new Vector3(0.32f, 0.7f,  0.76f),
            new Vector3(0.32f, 0.77f, 0.76f),
            new Vector3(0.32f, 0.79f, 0.76f),
            new Vector3(0.32f, 0.8f,  0.76f),
            new Vector3(0.32f, 0.79f, 0.76f),
            new Vector3(0.32f, 0.77f, 0.76f),
            new Vector3(0.32f, 0.7f,  0.76f),
            new Vector3(0.32f, 0.6f,  0.76f),
            new Vector3(0.32f, 0.55f, 0.76f),
            new Vector3(0.32f, 0.5f,  0.76f),
            new Vector3(0.32f, 0.48f, 0.76f),
            new Vector3(0.32f, 0.45f, 0.76f),
        };
        
        float[] colorsData = new float[colors.Length * 3];
        for (int i = 0; i < colors.Length; i++)
        {
            colorsData[i * 3] = colors[i].X;
            colorsData[i * 3 + 1] = colors[i].Y;
            colorsData[i * 3 + 2] = colors[i].Z;
        }

        Raylib.SetShaderValue(_shader, speedLoc, 0.15f, ShaderUniformDataType.Float);
        Raylib.SetShaderValue(_shader, scaleLoc, 1.2f, ShaderUniformDataType.Float);
        
        Raylib.SetShaderValueV(_shader, colorsLoc, colorsData, ShaderUniformDataType.Vec3, colors.Length);
        Raylib.SetShaderValueV(_shader, thresholdsLoc, thresholds, ShaderUniformDataType.Float, thresholds.Length);
        
        int colorCountLoc = Raylib.GetShaderLocation(_shader, "colorCount");
        Raylib.SetShaderValue(_shader, colorCountLoc, colors.Length, ShaderUniformDataType.Int);

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