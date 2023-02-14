using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    public float[] ChanceToMove = { .25f, .12f };   //not sure how to include this
    public string[] terrainType = {"regular", "muddy" };
    
    public enum terrain { regularLawn, muddy}
    public Material regLawnColor, muddyColor;    

    public void SetColor(terrain color)
    {
        GetComponent<Renderer>().material = color == terrain.muddy ? muddyColor : regLawnColor;
        
        //Material mat = color == terrain.regularLawn ? regLawnColor : muddyColor;
        //GetComponent<Renderer>().material = mat;           
        
    }

    
}
