using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitTarget : MonoBehaviour
{
    [Header("Exhibit object")]
    [Space(10f)]

    public GameObject CanvasWithText;
    public GameObject Statue;
    public Cinemachine.CinemachineFreeLook CameraOrbitPos;
    [Space(10f)]
   

    public float Pitch;
    public float Yaw;
    public float Roll;
    public float PaddingLeft;
    public float PaddingRight;
    public float PaddingUp;
    public float PaddingDown;
    public float MoveSmoothTime = 0.19f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
