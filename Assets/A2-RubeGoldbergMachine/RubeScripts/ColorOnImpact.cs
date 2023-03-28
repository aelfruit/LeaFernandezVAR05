using UnityEngine;
using System.Collections;   //for Coroutines

public class ColorOnImpact : MonoBehaviour

{
    public Material NearColor; 
    public Material FarColor;
    public float lerpSpeed = 2f;

    private Renderer rendererComp;
    private Material startMaterial;

    void Start()
    {
        rendererComp = gameObject.GetComponent<Renderer>();
        startMaterial = rendererComp.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            StopAllCoroutines(); //ignore previous
            StartCoroutine(LerpMaterial(NearColor));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject)
        {
            StopAllCoroutines();
            StartCoroutine(LerpMaterial(FarColor));
        }
    }

    private IEnumerator LerpMaterial(Material targetMaterial)
    {
        float t = 0f;
        Material currentMaterial = startMaterial;

        while (t < 1f) 
        {
            t += Time.deltaTime * lerpSpeed;
            rendererComp.material.Lerp(currentMaterial, targetMaterial, t); //lerp smoothens transition
            yield return null;
        }
        rendererComp.material = targetMaterial;
    }
}

// from color challenge exercise ("proximity bruise")
//{

//    public Material NearColor;
//    public Material FarColor;


//    void Awake()
//    {        
//        // default color at start of game is grey to show color change
//        gameObject.GetComponent<Renderer>().material.color = Color.grey;
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject)
//        {
//            gameObject.GetComponent<Renderer>().material.color = NearColor.color;
//        }

//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject)
//        {
//            gameObject.GetComponent<Renderer>().material.color = FarColor.color;
//        }
//    }
//}
