using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour
{
    public LayerMask Mask;

    private RaycastHit hit;
    private Ray ray;

    private Collider tempCollider;

    private bool isAsyncCompleted;

    // Use this for initialization
    void Start()
    {
        this.isAsyncCompleted = false;
        this.tempCollider = null;
        DontDestroyOnLoad(this);
        this.loadLevel(AppMacro.SECTIONSCENE_NAME);

        this.isAsyncCompleted = true;
        Debug.Log(this.isAsyncCompleted);
    }

    // Update is called once per frame
    void Update()
    {
        this.ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(this.ray, out this.hit, 30, this.Mask) && Input.GetMouseButtonDown(0))
        {
            this.tempCollider = this.hit.collider;
            print(this.tempCollider.name);
        }
        else if (Physics.Raycast(this.ray, out this.hit, 30, this.Mask) && Input.GetMouseButtonUp(0))
        {
            if (this.tempCollider != null)
            {
                if (this.tempCollider.Equals(this.hit.collider))
                {
                    string name = this.tempCollider.name;
                    switch (name)
                    {
                        case "Btn_Start":
                            if (this.isAsyncCompleted)
                            {
                                Application.LoadLevel(AppMacro.SECTIONSCENE_NAME);
                            }
                            break;
                        case "Btn_Option":
                            break;
                        case "Btn_Quit":
                            break;
                        default:
                            break;
                    }
                    print(name);
                }
            }
            this.tempCollider = null;
        }
    }

    // Load scene
    IEnumerable loadLevel(string _name)
    {
        AsyncOperation async = Application.LoadLevelAsync(AppMacro.SECTIONSCENE_NAME);
        while (!async.isDone)
        {
            print(async.progress + " %");
            yield return async;
        }
        
    }
}
