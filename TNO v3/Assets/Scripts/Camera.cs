using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    private Vector2 velocity;

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector2 smoothTime;
    [SerializeField]
    private Vector2 maxLimit;
    [SerializeField]
    private Vector2 minLimit;
    private Player instance;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
         if (target != null && GameManager.instance.status!=GameManager.GameStatus.DIE)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothTime.x);
            float posY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smoothTime.y);
            
            transform.position = new Vector3(posX, posY, transform.position.z);

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, minLimit.x, maxLimit.x),
                Mathf.Clamp(transform.position.y, minLimit.y, maxLimit.y),
                transform.position.z);
        }
    }
}
