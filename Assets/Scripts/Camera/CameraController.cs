using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Camera Follow Controller
 * Old version of update method contained in comment block at end of file.
 * This version of update will transition the camera to follow the player using lerp instad of a static tether to the player position.
 * There are a few scaling options that may be played with from within the inspection panel in unity ( camera asset must be selected, not script asset itself )
 * Aesthetic-oriented processes like this should be affordable since this will be a compiled version of the game instead of a browser version :)
 *
 * Last changed by Nate - 4/14/21
 */

public class CameraController : MonoBehaviour
{
    public Transform target; // transform that shall serve as the destination for camera follow

    public bool fixCamera = false; // camera snaps to fixedCameraPosition if true

    public Vector3 fixedCameraPosition = new Vector3( 0.0f, 0.0f, -10.0f ); // fixed camera position

    public float cameraZoom = 5.0f; // the target orthographic size for the camera

    public float zoomSpeed = 1.0f; // the time, in seconds, that it will take for the camera size to reach its target size

    public bool bindFollowToZoom = false; // display debug console output

    public float lerpScalingFactor = 0.04f; // the factor by which the distance between the camera and player is scaled to create a lerp gradient.

    public float minOffset = 0.03f; // the minimum distance between the camera and player before camera follow picks up

    public bool debugOutput = false; // display debug console output

    private float targetDistance; // the distance from the target to the camera

    float t; // a timer variable (used in camera zoom)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check if camera needs to be fixed
        if (fixCamera)
        {
            // snap camera to fixed position
            transform.position = fixedCameraPosition;
        }
        else // camera follow
        {
            // Calculate distance between camera and target
            /* This implementation avoids using any power or sqrt functions
             * In theory, this implementation should be cheaper than using Vector3.Distance() or creating two Vector2's and using Vector2.Distance()
             */
            targetDistance = Mathf.Abs((transform.position.x - target.transform.position.x)) + Mathf.Abs((transform.position.y - target.transform.position.y));

            // Checks follow-zoom binding boolean
            if (bindFollowToZoom)
            {
                // scale follow parameters by zoom ( more sensitive when zoomed in, less sensitive when zoomed out, less nauseating both ways )
                minOffset = 0.006f * cameraZoom;
                lerpScalingFactor = 0.008f * cameraZoom;
            }
        }

        // Zoom camera if desired size changes
        if (GetComponent<Camera>().orthographicSize != cameraZoom)
            CameraZoom();
    }

    void FixedUpdate()
    {
        // Cap the follow process to a certain distance ( performance check - process doesn't need to run if camera doesn't need to move )
        if (targetDistance >= minOffset)
            CameraFollow();
    }

    // Method for lerp'ing to a desired camera size
    void CameraZoom()
    {
        // lerp size
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, cameraZoom, t);
        // imcrement timer
        t += Time.deltaTime / zoomSpeed;
        //reset timer when finished lerp'ing
        if (t >= 1.0f)
            t = 0.0f;
    }

    // Method for lerp'ing camera to player position
    void CameraFollow()
    {   
        // Cap lerp ratio
        /* If any malfunction seperates the player and camera past a certain value the camera will snap back to the player's position.
            * This is the best fix I could find for this for now. */
        if (targetDistance > 10.0f)
            targetDistance = 10.0f;

        // Create a destination vector
        Vector3 dest = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z); //Follows player

        // Scale the distance by a scaling factor ( not really sure why but values between 0.02 and 0.07 work best )
        float lerpGradiant = targetDistance * lerpScalingFactor;

        // Lerp to destination vector by a scaling factor of the distance between the two
        transform.position = Vector3.Lerp(transform.position, dest, lerpGradiant);

        // Debug stuff ¯\_(ツ)_/¯
        if (lerpGradiant != 0.0f && debugOutput == true)
            Debug.Log("\nTarget distance: " + targetDistance + " Lerp Ratio: " + lerpGradiant);   
    }

    /* OLD VERSION:
     *  void Update()
     *   {
     *       transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z); //Follows player
     *   }
     */
}
