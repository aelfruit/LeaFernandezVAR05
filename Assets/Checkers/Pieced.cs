using UnityEngine;

public class Pieced : MonoBehaviour
{
    public int x, z;
    
    public void movePiece(int nx, int nz)
    {
        x = nx;
        z = nz;

        transform.position = new Vector3(x, 0, z);
    }
}
