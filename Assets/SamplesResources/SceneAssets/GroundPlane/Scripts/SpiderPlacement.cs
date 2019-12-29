/*============================================================================== 
Copyright (c) 2018 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.   
==============================================================================*/

using UnityEngine;
using Vuforia;

public class SpiderPlacement : MonoBehaviour
{
    #region PUBLIC_MEMBERS
    public bool IsPlaced { get; private set; }
    #endregion // PUBLIC_MEMBERS


    #region PRIVATE_MEMBERS
    [Header("Spider")]
    [SerializeField] GameObject spider = null;
    [Header("Spider")]
    [SerializeField] GameObject spider2 = null;
    [Header("Spider")]
    [SerializeField] GameObject spider3 = null;
    [Header("Spider")]
    [SerializeField] GameObject spider4 = null;
    [Header("Spider")]
    [SerializeField] GameObject spider5 = null;
    [Header("Spider")]
    [SerializeField] GameObject spider6 = null;
    [Header("Spider")]
    [SerializeField] GameObject spider7 = null;

    [Header("Control Indicators")]
    [SerializeField] GameObject translationIndicator = null;
    [SerializeField] GameObject rotationIndicator = null;
    [Header("Control Indicators")]
    [SerializeField] GameObject translationIndicator2 = null;
    [SerializeField] GameObject rotationIndicator2 = null;
    [Header("Control Indicators")]
    [SerializeField] GameObject translationIndicator3 = null;
    [SerializeField] GameObject rotationIndicator3 = null;
    [Header("Control Indicators")]
    [SerializeField] GameObject translationIndicator4 = null;
    [SerializeField] GameObject rotationIndicator4 = null;
    [Header("Control Indicators")]
    [SerializeField] GameObject translationIndicator5 = null;
    [SerializeField] GameObject rotationIndicator5 = null;
    [Header("Control Indicators")]
    [SerializeField] GameObject translationIndicator6 = null;
    [SerializeField] GameObject rotationIndicator6 = null;
    [Header("Control Indicators")]
    [SerializeField] GameObject translationIndicator7 = null;
    [SerializeField] GameObject rotationIndicator7 = null;

    [Header("Augmentation Size")]
    [Range(0.06222595f, 0.13f)]
    [SerializeField] float productSize = 0.06222595f;

    MeshRenderer spiderRenderer;
    Material[] spiderMaterials, spiderMaterialsTransparent;

    GroundPlaneUI groundPlaneUI;
    Camera mainCamera;
    Ray cameraToPlaneRay;
    RaycastHit cameraToPlaneHit;

    float augmentationScale;
    Vector3 productScale;
    string floorName;

    // Property which returns whether spider visibility conditions are met
    bool SpiderVisibilityConditionsMet
    {
        // The Spider should only be visible if the following conditions are met:
        // 1. Tracking Status is Tracked or Limited
        // 2. Ground Plane Hit was received for this frame
        // 3. The Plane Mode is equal to PLACEMENT
        get
        {
            return
                PlaneManager.TrackingStatusIsTrackedOrLimited &&
                PlaneManager.GroundPlaneHitReceived &&
                (PlaneManager.CurrentPlaneMode == PlaneManager.PlaneMode.PLACEMENT);
        }
    }
    #endregion // PRIVATE_MEMBERS


    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
        this.mainCamera = Camera.main;
        this.groundPlaneUI = FindObjectOfType<GroundPlaneUI>();
        this.spiderRenderer = this.spider.GetComponent<MeshRenderer>();

        SetupFloor();


        this.augmentationScale = VuforiaRuntimeUtilities.IsPlayMode() ? 0.1f : this.productSize;

        this.productScale =
            new Vector3(this.augmentationScale,
                        this.augmentationScale,
                        this.augmentationScale);

        this.spider.transform.localScale = this.productScale;
        this.spider2.transform.localScale = this.productScale;
        this.spider3.transform.localScale = this.productScale;
        this.spider4.transform.localScale = this.productScale;
        this.spider5.transform.localScale = this.productScale;
        this.spider6.transform.localScale = this.productScale;
        this.spider7.transform.localScale = this.productScale;
    }


    void Update()
    {
        if (PlaneManager.CurrentPlaneMode == PlaneManager.PlaneMode.PLACEMENT)
        {
            EnablePreviewModeTransparency(!this.IsPlaced);
            if (!this.IsPlaced)
                UtilityHelper.RotateTowardCamera(this.spider);
        }

        if (PlaneManager.CurrentPlaneMode == PlaneManager.PlaneMode.PLACEMENT && this.IsPlaced)
        {
            this.rotationIndicator.SetActive(Input.touchCount == 2);
            this.rotationIndicator2.SetActive(Input.touchCount == 2);
            this.rotationIndicator3.SetActive(Input.touchCount == 2);
            this.rotationIndicator4.SetActive(Input.touchCount == 2);
            this.rotationIndicator5.SetActive(Input.touchCount == 2);
            this.rotationIndicator6.SetActive(Input.touchCount == 2);
            this.rotationIndicator7.SetActive(Input.touchCount == 2);

            this.translationIndicator.SetActive(
                (TouchHandler.IsSingleFingerDragging || TouchHandler.IsSingleFingerStationary) && !this.groundPlaneUI.IsCanvasButtonPressed());
            this.translationIndicator2.SetActive(
                (TouchHandler.IsSingleFingerDragging || TouchHandler.IsSingleFingerStationary) && !this.groundPlaneUI.IsCanvasButtonPressed());
            this.translationIndicator3.SetActive(
                (TouchHandler.IsSingleFingerDragging || TouchHandler.IsSingleFingerStationary) && !this.groundPlaneUI.IsCanvasButtonPressed());
            this.translationIndicator4.SetActive(
                (TouchHandler.IsSingleFingerDragging || TouchHandler.IsSingleFingerStationary) && !this.groundPlaneUI.IsCanvasButtonPressed());
            this.translationIndicator5.SetActive(
                (TouchHandler.IsSingleFingerDragging || TouchHandler.IsSingleFingerStationary) && !this.groundPlaneUI.IsCanvasButtonPressed());
            this.translationIndicator6.SetActive(
                (TouchHandler.IsSingleFingerDragging || TouchHandler.IsSingleFingerStationary) && !this.groundPlaneUI.IsCanvasButtonPressed());
            this.translationIndicator7.SetActive(
                (TouchHandler.IsSingleFingerDragging || TouchHandler.IsSingleFingerStationary) && !this.groundPlaneUI.IsCanvasButtonPressed());

            if (TouchHandler.IsSingleFingerDragging || (VuforiaRuntimeUtilities.IsPlayMode() && Input.GetMouseButton(0)))
            {
                if (!this.groundPlaneUI.IsCanvasButtonPressed())
                {
                    this.cameraToPlaneRay = this.mainCamera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(this.cameraToPlaneRay, out this.cameraToPlaneHit))
                    {
                        if (this.cameraToPlaneHit.collider.gameObject.name == floorName)
                        {
                            this.spider.PositionAt(this.cameraToPlaneHit.point);
                        }
                    }
                }
            }
        }
        else
        {
            this.rotationIndicator.SetActive(false);
            this.rotationIndicator2.SetActive(false);
            this.rotationIndicator3.SetActive(false);
            this.rotationIndicator4.SetActive(false);
            this.rotationIndicator5.SetActive(false);
            this.rotationIndicator6.SetActive(false);
            this.rotationIndicator7.SetActive(false);

            this.translationIndicator.SetActive(false);
            this.translationIndicator2.SetActive(false);
            this.translationIndicator3.SetActive(false);
            this.translationIndicator4.SetActive(false);
            this.translationIndicator5.SetActive(false);
            this.translationIndicator6.SetActive(false);
            this.translationIndicator7.SetActive(false);
        }
    }

    void LateUpdate()
    {
        if (!this.IsPlaced)
        {
            SetVisible(this.SpiderVisibilityConditionsMet);
        }
    }
    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS
    public void Reset()
    {
        this.spider.transform.position = Vector3.zero;
        this.spider.transform.localEulerAngles = Vector3.zero;
        this.spider.transform.localScale = this.productScale;
    }

    public void SetProductAnchor(Transform transform)
    {
        if (transform)
        {
            this.IsPlaced = true;
            this.spider.transform.SetParent(transform);
            this.spider.transform.localPosition = Vector3.zero;
            UtilityHelper.RotateTowardCamera(this.spider);
        }
        else
        {
            this.IsPlaced = false;
            this.spider.transform.SetParent(null);
        }
    }
    #endregion // PUBLIC_METHODS


    void SetupFloor()
    {
        if (VuforiaRuntimeUtilities.IsPlayMode())
        {
            this.floorName = "Emulator Ground Plane";
        }
        else
        {
            this.floorName = "Floor";
            GameObject floor = new GameObject(this.floorName, typeof(BoxCollider));
            floor.transform.SetParent(this.spider.transform.parent);
            floor.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            floor.transform.localScale = Vector3.one;
            floor.GetComponent<BoxCollider>().size = new Vector3(100f, 0, 100f);
        }
    }

    /// <summary>
    /// This method is used prior to spider being placed. Once placed, spider visibility is controlled
    /// by the DefaultTrackableEventHandler.
    /// </summary>
    /// <param name="visible">bool</param>
    void SetVisible(bool visible)
    {
        // Set the visibility of the spider and it's shadow
        this.spiderRenderer.enabled = visible;
    }

    /*void EnablePreviewModeTransparency(bool previewEnabled)
    {
        this.spiderRenderer.materials = previewEnabled ? this.spiderMaterialsTransparent : this.spiderMaterials;
    }*/

    void EnablePreviewModeTransparency(bool previewEnabled)
    {
        this.spiderRenderer.materials = previewEnabled ? this.spiderMaterialsTransparent : this.spiderMaterials;
    }

}
