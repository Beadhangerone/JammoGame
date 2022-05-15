using UnityEngine;

public class JammoColider : MonoBehaviour
{
    public GameObject levelManager;
    public GameObject deadZone;
    private const string TrampolineTag = "Trampoline";
    private const string FinishTag = "FinishArch";
    private const string DeadTag = "DeadZone";
    private const string TransportTag = "Transport";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject other = hit.gameObject;
        
        //Debug.Log(other.tag);
        if (other.CompareTag(TrampolineTag))
        {
            gameObject.GetComponent<MyMoveInput>().SuperJump();
        }

        if (other.CompareTag(FinishTag))
        {
            levelManager.GetComponent<MyLevelManager>().FinishLevel1();
        }

        if (other.CompareTag(TransportTag))
        {
            //gameObject.transform.position
        }
        
        if (other.CompareTag(DeadTag))
        {
            levelManager.GetComponent<MyLevelManager>().FailLevel1();
        }
        deadZone.transform.position = new Vector3(transform.position.x, other.transform.position.y - 3, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
