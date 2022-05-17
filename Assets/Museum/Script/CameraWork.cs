using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraWork : MonoBehaviour
{
    // Start is called before the first frame update
    public FirstPersonLook fpLook;
    public CameraMultiTarget CameraMt;
    public FirstPersonMovement fpsMove;
    public Rigidbody PlayerRigidbody;
    public float SpeedBackCamera;

    public Text TextPress;

    [Header("Camera Setting")]
    [Space(10f)]
    public float DefaultFov;
    public float ZoomFOV;
    public float ZoomSpeed;
    public Cinemachine.CinemachineVirtualCamera virtlCamera;

    private bool IsLookExhibit;
    private bool IsCamGoBack;
    private bool IsOrbit;
    private Vector3 CamPos;
    private Vector3 CamRot;
    private Cinemachine.CinemachineFreeLook _CameraOrbitPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLookExhibit == false && IsCamGoBack == false)
        {
            if (Input.GetMouseButton(0))
            {
                CameraZoom(ZoomFOV);
            }
            else
            {
                CameraZoom(DefaultFov);
            }
        }
        if (IsCamGoBack)
        {
            transform.position = Vector3.MoveTowards(transform.position, CamPos, Time.deltaTime * SpeedBackCamera);
            transform.LookAt(CamRot);
            CameraMt._targets[0].SetActive(false);
            CameraZoom(DefaultFov);
            TextPress.text = " ";


            //  transform.rotation = Quaternion.Lerp(transform.rotation, CamRot, Time.deltaTime * SpeedBackCamera);
            if (Vector3.Distance(transform.position, CamPos) <= 0.02f)
            {
                fpsMove.enabled = true;
                fpLook.enabled = true;
                PlayerRigidbody.isKinematic = false;
                Debug.Log("go back");
                IsLookExhibit = false;
                IsCamGoBack = false;
            }
        }
       else if(IsLookExhibit)
        {
            CameraZoom(ZoomFOV);

        }

        Transform cameraTransform = Camera.main.transform;
        RaycastHit HitInfo;
        if (IsLookExhibit && Input.GetKeyDown(KeyCode.E)&& IsOrbit == false)
        {
            CameraMt.enabled = false;
            fpLook.enabled = false;
            _CameraOrbitPos.enabled = true;
            CameraMt._targets[0].SetActive(false);

            IsOrbit = true;
            TextPress.text = "press E to go back | or RMB to exit viewing";

        }
        else if (IsLookExhibit && Input.GetKeyDown(KeyCode.E) && IsOrbit == true)   
        {
            _CameraOrbitPos.enabled = false;
            IsOrbit = false;
            CameraMt.enabled = true;
            CameraMt._targets[0].SetActive(true);
            TextPress.text = "press RMB to exit viewing";

            transform.LookAt(CamRot);


        }
        if (IsLookExhibit&& Input.GetMouseButtonDown(1))
        {           
                CameraMt.enabled = false;
                IsCamGoBack = true;
               _CameraOrbitPos.enabled = false;
                IsOrbit = false;
        }
        else if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out HitInfo, 10.0f))
        {
            if (HitInfo.transform.tag == "Exhibit")
            {

                if (Input.GetMouseButtonDown(1)&&IsLookExhibit == false )
                {
                  //  IsCamGoBack = false;
                    CamPos = transform.position;
                    CamRot = HitInfo.point;
                    Debug.Log("disable script");
                    SetCameraMt(HitInfo.transform.GetComponent<ExhibitTarget>());
                    _CameraOrbitPos = HitInfo.transform.GetComponent<ExhibitTarget>().CameraOrbitPos;
                    PlayerRigidbody.isKinematic = true;
                    CameraMt.enabled = true;
                    fpLook.enabled = false;
                    IsLookExhibit = true;
                    fpsMove.enabled = false;
                    TextPress.text = "click E to rotate the camera";
                    // CameraMt.

                }
                Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 10.0f, Color.yellow);
            }
          

        }
       

    }
    public void SetCameraMt(ExhibitTarget target)
    {
        CameraMt.Pitch = target.Pitch;
        CameraMt.Yaw = target.Yaw;
        CameraMt.Roll = target.Roll;
        CameraMt.PaddingLeft = target.PaddingLeft;
        CameraMt.PaddingRight = target.PaddingRight;
        CameraMt.PaddingUp = target.PaddingUp;
        CameraMt.PaddingDown = target.PaddingDown;
        CameraMt.MoveSmoothTime = target.MoveSmoothTime;
        CameraMt._targets[0] = target.CanvasWithText;
        CameraMt._targets[1] = target.Statue;
    }

    public void CameraZoom(float zoom)
    {
        virtlCamera.m_Lens.FieldOfView = Mathf.MoveTowards(virtlCamera.m_Lens.FieldOfView, zoom, ZoomSpeed * Time.deltaTime);

    }
}
