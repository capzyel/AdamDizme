using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KargaGames.Drawing;
using TMPro;

public class LevelFinisher : MonoBehaviour
{
    public GameObject LevelEndCubes;
    public List<Material> cubeMaterials;
    public Vector3[] posList;

    public Vector3 CameraFollow;
    public Vector3 CameraTarget;

    SplineByDrawing splineDrawer;
    SplineGenerator splineGenerator;
    GameObject drawingUI;

    CameraController cameraController;

    public bool passed = false;
    /*
    private void OnValidate()
    {
        if (isActiveAndEnabled)
        {
            int i = 0;
            foreach (Transform t in LevelEndCubes.transform)
            {
                t.GetComponent<Renderer>().material = cubeMaterials[i%cubeMaterials.Count];

                i++;
                t.GetComponentInChildren<TextMeshPro>().text = i.ToString() + "x";
            }
        }
    }
    */
    // Start is called before the first frame update
    void Start()
    {
        splineDrawer = FindObjectOfType<SplineByDrawing>();
        splineGenerator = FindObjectOfType<SplineGenerator>();
        drawingUI = GameObject.FindGameObjectWithTag("DrawingArea");

        cameraController = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (passed)
        {
            cameraController.FollowOffset = Vector3.Lerp(cameraController.FollowOffset,CameraFollow,Time.deltaTime * 2f);
            cameraController.TargetOffset = Vector3.Lerp(cameraController.TargetOffset, CameraTarget, Time.deltaTime * 2f);
        }
    }

    public void FinishReached()
    {
        splineDrawer.enabled = false;
        drawingUI.SetActive(false);
        splineGenerator.GenerateSplineFromPointList(posList);
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!passed)
        {
            passed = true; 
            ColumnHeader column = other.GetComponent<ColumnHeader>();
            if (column != null)
            {
                FinishReached();
            }
        }
        
    }
}
