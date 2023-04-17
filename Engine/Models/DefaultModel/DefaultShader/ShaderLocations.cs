namespace Engine.Models.DefaultModel.DefaultShader;

public class ShaderLocations
{
    public int Position { get; }
    public string MMatrix { get; }
    public string VMatrix { get; }
    public string PMatrix { get; }
    
    public ShaderLocations(int position, string mMatrix, string vMatrix, string pMatrix)
    {
        Position = position;
        MMatrix = mMatrix;
        VMatrix = vMatrix;
        PMatrix = pMatrix;
    }
}