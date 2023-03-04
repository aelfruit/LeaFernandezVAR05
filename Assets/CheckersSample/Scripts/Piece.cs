using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public enum PlayerPiece { lightPiece, darkPiece}
    public PlayerPiece color;
    public int x, y;  //relative to board, actual y is z in Vector 3;
    public bool isCrowned;

    public void Crowned()
    {
        //piece reaches end of board
        //flip and can move forward/backwards        
    }

    public void Captured()
    {
        Destroy(gameObject);
        //for special fx
        //            Destroy(hit.transform.gameObject);
        //            var em = collisionParticleSystem.emission;
        //            em.enabled = true;
        //            collisionParticleSystem.Play();
        //            soundEffect.time = .1f;
        //            soundEffect.Play();
    }

    public void 

    

}
