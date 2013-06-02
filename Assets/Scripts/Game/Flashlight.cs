using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour
{
    // 光線的長度
    public float ShotDistance;

    // 射線遮罩
    public LayerMask Mask;



    private RaycastHit hit;
    private Ray ray;

    private Light flashlight;
    private Transform currentTranform;

    // Is light on
    private bool isLight;

    // Use this for initialization
    void Start()
    {
        this.currentTranform = this.transform;
        this.flashlight = null;

        // Find the flashlight in Lydar
        Light[] lights = this.GetComponentsInChildren<Light>();
        foreach (Light item in lights)
        {
            if (item.gameObject.name.Equals("Flashlight"))
            {
                this.flashlight = item;
                break;
            }
        }

        if (this.flashlight != null)
        {
            this.isLight = this.flashlight.enabled;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.flashlight != null)
        {
            if (this.isLight)
            {
                // Shot the ray from current position forward to back.
                this.ray = new Ray(this.currentTranform.position, this.currentTranform.TransformDirection(Vector3.back));

                // Draw the ray as red.
                Debug.DrawRay(this.currentTranform.position, this.currentTranform.TransformDirection(Vector3.back) * this.ShotDistance, Color.red);

                // Verify the ray is hit colliders
                if (Physics.Raycast(this.ray, out this.hit, this.ShotDistance, this.Mask))
                {
                    // Verify hit collider's tag is "Mirror"
                    if (this.hit.collider.tag.Equals("Mirror"))
                    {
                        // Turn on the light which is shot by light.
                        this.hit.collider.gameObject.GetComponent<Flashlight>().TurnOnLight(true);
                    }
                }
            }
        }
    }

    public void TurnOnLight(bool isTurnOn)
    {
        if (isTurnOn)
        {
            this.isLight = isTurnOn;
            this.flashlight.enabled = true;
        }
        else
        {
            this.isLight = isTurnOn;
            this.flashlight.enabled = false;
        }
    }
}
