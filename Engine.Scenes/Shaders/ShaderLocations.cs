namespace Engine.Scenes.Shaders;

public class ShaderLocations
{
    public int Position { get; }
    public string MMatrix { get; }
    public string VMatrix { get; }
    public string PMatrix { get; }
    public string AmbientColor { get; }
    

    public ShaderLocations(int position, string mMatrix, string vMatrix, string pMatrix, string ambientColor)
    {
        Position = position;
        MMatrix = mMatrix;
        VMatrix = vMatrix;
        PMatrix = pMatrix;
        AmbientColor = ambientColor;
    }
}