using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOverlayBehaviour : UIGenericBehaviour
{
    [Header("Overlay Settings")]
    public float alpha = 0.8f;

    public new void hide()
    {
        canvasGroup.alpha = 0;
    }

    public new void show()
    {
        canvasGroup.alpha = alpha;
    }
}
