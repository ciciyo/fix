using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public List<Gate> gates;
    public List<Gate> occupiedGates;

    public Dictionary<Gate, Transform> gatesDict = new Dictionary<Gate, Transform>();

    public List<Spot> availSpots;
    public List<Spot> occupiedSpots;

    private List<Gateway> gateways = new List<Gateway>();

    public void Init()
    {
        InitAllGates();
        InitAllSpots();
        // InitAllGateway();
    }

    void InitAllGates()
    {
        Gate[] gate = gameObject.GetComponentsInChildren<Gate>();
        foreach (Gate item in gate)
        {
            gates.Add(item);
        }
    }

    void InitAllSpots()
    {
        Spot[] spot = gameObject.GetComponentsInChildren<Spot>();
        foreach (Spot item in spot)
        {
            availSpots.Add(item);
        }
    }

    void InitAllGateway(){
        Gateway[] gateways = gameObject.GetComponentsInChildren<Gateway>();
        foreach (var item in gateways)
        {
            this.gateways.Add(item);
        }
    }
}
