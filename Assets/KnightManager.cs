using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class KnightManager : MonoBehaviour
{
    // vars
    public ARRaycastManager arRaycastManager;
    //Assign camera – should work with main tag but sometimes has issues 
    public Camera arCamera;
    public GameObject knightPrefab;

    void Update()
    {
        // touchcount condition
        if (Input.touchCount > 0)
        {
            // touch var
            var touch = Input.GetTouch(0);
            //touch.phase condition
            if (touch.phase == TouchPhase.Ended)
            {
                if (Input.touchCount == 1)
                {
                    List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();
                    //Raycast Planes
                    if (arRaycastManager.Raycast(touch.position, arRaycastHits))
                    {

                        Ray ray = arCamera.ScreenPointToRay(touch.position);

                        if (Physics.Raycast(ray, out RaycastHit hit))
                        {
                            if (hit.collider.tag == "knight")
                            {
                                DeleteKnight(hit.collider.gameObject);
                                return;
                            }
                            else
                            {
                                var pose = arRaycastHits[0].pose;
                                CreateKnight(pose.position);
                                return;
                            }
                        }

                        return;
                    }


                }
            }


            // end touch phase condition
            // end touchcount condition
        }
    }

    private void CreateKnight(Vector3 position)
    {
        Instantiate(knightPrefab, position, Quaternion.identity);
        Debug.Log("Created");
    }

    private void DeleteKnight(GameObject knightObject)
    {
        Destroy(knightObject);
        Debug.Log("Destroyed");
    }
}