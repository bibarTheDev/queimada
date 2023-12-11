using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textureTilingFix : MonoBehaviour
{
    [Header("Scaling")]
    public float factor = 1;

    // Start is called before the first frame update
    void Start() {
        // Eu detesto essa solucao mas c'est la vie
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null) {
            // pega o tamanho do objeto atraves dos limites da mesh
            Bounds limite = meshFilter.mesh.bounds;            
            Vector3 size = Vector3.Scale(limite.size, transform.localScale) * factor;
            
            // se nao tiver altura (plano), usar Z
            if (size.y < .001)
                size.y = size.z;
            
            // altera o scaling pelo renderer
            GetComponent<Renderer>().material.mainTextureScale = size;
        }
    }
}
