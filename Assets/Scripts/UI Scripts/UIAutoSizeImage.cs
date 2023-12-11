using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAutoSizeImage : MonoBehaviour
{
    [Header("Scaling")]
    public float scaleX = 1;
    public float scaleY = 1;

    // Start is called before the first frame update
    void Start()
    {
        Image imageComp = gameObject.GetComponent<Image>();
        RectTransform rectTransfComp = gameObject.GetComponent<RectTransform>();

        if(imageComp == null || rectTransfComp == null){
            Debug.LogWarning("Impossivel achar componente Image ou RectTransform para " + gameObject.name + ". Certifiquese que esse eh um objeto UIImage" );
        }
        else{
            Vector2 spriteSize = gameObject.GetComponent<Image>().sprite.bounds.size;
            Debug.Log("SpriteSize original para " + gameObject.name + ": " + spriteSize.x + ", " + spriteSize.y);
            rectTransfComp.sizeDelta = new Vector2(spriteSize.x * scaleX, spriteSize.y * scaleY);
            Debug.Log("SpriteSize final para " + gameObject.name + ": " + spriteSize.x * scaleX + ", " + spriteSize.y * scaleY);
        }
    }
}
