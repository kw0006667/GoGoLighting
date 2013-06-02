using UnityEngine;
using System.Collections;

public class SectionChoice : MonoBehaviour
{
    public bool isUnlock;
    public int SectionNumber = 0;

    private RaycastHit hit;
    private Ray ray;

    private Collider tempCollider;

    // Use this for initialization
    void Start()
    {
        this.tempCollider = null;
    }

    // Update is called once per frame
    void Update()
    {
        this.ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(this.ray, out this.hit, 20) && Input.GetMouseButtonDown(0))
        {
            if (this.hit.collider.Equals(this.collider))
            {
                this.tempCollider = this.hit.collider;
            }
        }
        else if (Physics.Raycast(this.ray, out this.hit, 20) && Input.GetMouseButtonUp(0))
        {
            if (this.tempCollider != null)
            {
                if (this.hit.collider.Equals(this.tempCollider))
                {
                    if (this.isUnlock)
                    {
                        switch (this.SectionNumber)
                        {
                            case 0:
                                Application.LoadLevel(AppMacro.GAMETUTORIALSCENE_NAME);
                                break;
                            default:
                                break;
                        }
                    }

                }
            }
            this.tempCollider = null;
        }
    }
}
