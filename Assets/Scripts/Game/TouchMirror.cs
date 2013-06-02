using UnityEngine;
using System.Collections;

public class TouchMirror : MonoBehaviour
{
    public LayerMask Mask;

    private RaycastHit hit;
    private Ray ray;

    private Collider tempCollider;

    private float totalTime;

    private bool isMoving;

    private Vector3 currentMousePosition;
    private Vector3 preMousePosition;
    private Vector3 offest;

    // Use this for initialization
    void Start()
    {
        this.tempCollider = null;
        this.totalTime = 0.0f;
        this.isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(this.ray, out this.hit, 30, this.Mask) && Input.GetMouseButtonDown(0))
        {
            
            if (this.hit.collider.tag.Equals("Mirror"))
            {
                this.tempCollider = this.hit.collider;
                this.preMousePosition = Camera.main.WorldToScreenPoint(this.tempCollider.transform.position);
                this.offest = this.tempCollider.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.preMousePosition.z));
                print("Touched");
            }
        }
        else if (Physics.Raycast(this.ray, out this.hit, 30, this.Mask) && Input.GetMouseButton(0))
        {
            if (this.hit.collider == this.tempCollider)
            {
                if (!this.isMoving)
                {
                    this.totalTime += Time.deltaTime;
                    if (this.totalTime >= 0.25f)
                    {
                        this.isMoving = true;
                    }
                }
            }
        }
        else if ( Input.GetMouseButtonUp(0))
        {
            if (this.isMoving)
            {
                this.totalTime = 0;
                this.isMoving = false;
                this.tempCollider = null;
            }
            else
            {
                if (this.tempCollider != null)
                {
                    if (this.hit.collider.Equals(this.tempCollider) && this.tempCollider.tag.Equals("Mirror"))
                    {
                        this.tempCollider.transform.RotateAround(this.tempCollider.transform.position ,this.tempCollider.transform.TransformDirection(Vector3.up), 30);
                    }

                    this.tempCollider = null;
                }
            }


            print("Release");
        }

        if (this.isMoving)
        {
            print("Moving...");
            
            
            
            if (this.tempCollider != null)
            {
                Vector3 position = Input.mousePosition;

                this.currentMousePosition = new Vector3(position.x, position.y, this.preMousePosition.z);
                this.tempCollider.transform.position = Camera.main.ScreenToWorldPoint(this.currentMousePosition) + this.offest;
                //print(this.tempCollider.name);
                print(currentMousePosition);
                //this.tempCollider.transform.position = new Vector3(-currentMousePosition.x, -currentMousePosition.y, this.tempCollider.transform.position.z);
            }
        }
    }
}
