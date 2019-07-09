using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private Transform[] bgs;
    [SerializeField]
    private float[] parallaxVel;
    [SerializeField]
    private float smooth;
    [SerializeField]
    private Transform cam;

    private Vector3 previewCam;

    // Start is called before the first frame update
    void Start()
    {
        previewCam = cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        for ( int i = 0; i < bgs.Length; i++)
        {
            float parallaxX = (previewCam.x - cam.position.x) * parallaxVel[i]; //x
            float targetPosX = bgs[i].position.x - parallaxX;
            float parallaxY = (previewCam.y - cam.position.y) * parallaxVel[i]; //y
            float targetPosY = bgs[i].position.y - parallaxY;
            Vector3 targetPos = new Vector3(targetPosX, targetPosY, bgs[i].position.z);
            bgs[i].position = Vector3.Lerp(bgs[i].position, targetPos, smooth = Time.deltaTime);
        }

        previewCam = cam.position;
    }
}
