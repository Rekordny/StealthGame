using UnityEngine;
using UnityEngine.Assertions;

public class ParentController : MonoBehaviour
{
    public Transform kitchenDest;
    public Transform livingRoomDest;
    
    public Vector3 NoiseLocation { get; private set; }

    public Animator stateMachine;

    private readonly int restingParam = Animator.StringToHash("Let It Simmer");
    private readonly int cookingParam = Animator.StringToHash("Boiling Over");
    private readonly int alertedParam = Animator.StringToHash("Heard Something");
    private readonly int allClearParam = Animator.StringToHash("All Clear");

    private void Start()
    {
        Assert.IsNotNull(stateMachine, "Error: You forgot to assign the animator!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            stateMachine.SetTrigger(restingParam);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            stateMachine.SetTrigger(cookingParam);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            stateMachine.SetTrigger(allClearParam);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo))
            {
                NoiseLocation = hitInfo.point;
                stateMachine.SetTrigger(alertedParam);
            }
        }
    }
}
