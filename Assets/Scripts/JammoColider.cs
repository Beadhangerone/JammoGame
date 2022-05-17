using UnityEngine;

public class JammoColider : MonoBehaviour
{
    private MyLevelManager _levelManager;
    public GameObject deadZone;
    private const string TrampolineTag = "Trampoline";
    private const string FinishTag = "FinishArch";
    private const string DeadTag = "DeadZone";
    private const string TransportTag = "Transport";
    
    void Awake()
    {
        _levelManager = MyLevelManager.Instance;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject other = hit.gameObject;

        if (other.CompareTag(DeadTag))
        {
            _levelManager.FailLevel1();
        }
        deadZone.transform.position = new Vector3(transform.position.x, other.transform.position.y - 3, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
