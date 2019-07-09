using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBgLoop : MonoBehaviour
{
    [SerializeField]
    private float bgSpeed;
    private Vector3 initialPos;
    private float imageSize;
    private float imageScale;
    private float imageRealSize;
    private float deltaSpace;

    private void Awake()
    {
        initialPos = transform.position;
        imageSize = GetComponent<SpriteRenderer>().size.x;
        imageScale = transform.localScale.x;
        imageRealSize = imageSize * imageScale;
    }

    void Update()
    {
        if (deltaSpace <= imageRealSize)
            deltaSpace = +Time.timeSinceLevelLoad * bgSpeed;
        else
            deltaSpace = 0f;

        transform.position = initialPos + Vector3.left * deltaSpace; 
    }
}
