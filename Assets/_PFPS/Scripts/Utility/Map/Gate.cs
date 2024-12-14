
using UnityEngine;

[System.Serializable]
public class Gate : MonoBehaviour
{
    private Transform gate;
    public Vector3 offset;
    public bool occupied;
    public Transform portal;

    void Start(){
        Init();
        CalculateOffset();
    }
    void Init(){
        gate = transform;

    }

    public void CalculateOffset(){
        
        offset = transform.parent.position - gate.position;
    }
}
