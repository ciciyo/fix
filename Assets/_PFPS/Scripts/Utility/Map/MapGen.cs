using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{


    public int testCount;

    public static List<Tile> tiles { get; private set; } = new List<Tile>();
    public static Dictionary<string, Building> buildings{ get; private set; } = new Dictionary<string, Building>();

    private List<Tile> activeTile = new List<Tile>();

    public delegate void ProgressUpdate(float progress);
    public ProgressUpdate onProgressUpdate;
    public delegate void LoadComplete();
    public LoadComplete onComplete;






    void Start()
    {
        // GenerateMap(testCount);
        Generate();
    }

    private void OnEnable()
    {
        onProgressUpdate += UpdateProgress;
        onComplete += OnLoadComplete;
    }

    private void OnDisable()
    {
        onProgressUpdate -= UpdateProgress;
        onComplete -= OnLoadComplete;
    }

    public void Generate()
    {
        StartCoroutine(LoadTiles("Map/Prefabs"));
        StartCoroutine(LoadBuilding("Map/Prefabs/Building"));
    }


    IEnumerator LoadTiles(string resourcePath)
    {
        Tile[] loadedPrefabs = Resources.LoadAll<Tile>(resourcePath);
        int totalPrefabs = loadedPrefabs.Length;

        Debug.Log("Total Prefabs " + totalPrefabs);

        for (int i = 0; i < totalPrefabs; i++)
        {
            tiles.Add(loadedPrefabs[i]);

            float loadProgress = (float)(i + 1) / totalPrefabs;
            onProgressUpdate?.Invoke(loadProgress);

            yield return null;
        }

        onComplete?.Invoke();

    }

    IEnumerator LoadBuilding(string resourcePath)
    {
        Building[] loadedPrefabs = Resources.LoadAll<Building>(resourcePath);
        int totalPrefabs = loadedPrefabs.Length;

        for (int i = 0; i < totalPrefabs; i++)
        {
            buildings.Add(loadedPrefabs[i].transform.name, loadedPrefabs[i]);

            float loadProgress = (float)(i + 1) / totalPrefabs;
            onProgressUpdate?.Invoke(loadProgress);

            yield return null;
        }

    }


    private void GenerateMap(int count)
    {

        for (int index = 0; index < count; index++)
        {
            GameObject go = Instantiate(tiles[Random.Range(0, tiles.Count - 1)].gameObject);
            go.transform.name = string.Format("Tile ke {0} ", index);
            Tile tile = go.GetComponent<Tile>();
            
            tile.Init();
            if (index == 0)
            {
                go.transform.position = new Vector3(100 * index, 100 * index, 100 * index);

            }
            else
            {
                //Get Random Gate
                //Current Gate
                int currGate = Random.Range(0, tile.gates.Count);
                //previous Tile's gate
                int prevGate = Random.Range(0, activeTile[index - 1].gates.Count);

                //Positioning the Tile
                go.transform.position = go.transform.position - (tile.gates[currGate].transform.position - activeTile[index - 1].gates[prevGate].transform.position) + tile.gates[currGate].offset;

                //Rotating the Tile
                Quaternion targetRotation = activeTile[index - 1].gates[prevGate].transform.parent.rotation * activeTile[index - 1].gates[prevGate].transform.localRotation * Quaternion.Euler(0, 180, 0);
                Quaternion neededRotation = targetRotation * Quaternion.Inverse(tile.gates[currGate].transform.parent.rotation * tile.gates[currGate].transform.localRotation);
                go.transform.RotateAround(tile.gates[currGate].transform.position, Vector3.up, neededRotation.eulerAngles.y);

                //Takeout used Gate for next Gen
                OccupiedGate(tile, tile.gates[currGate]);
                OccupiedGate(activeTile[index - 1], activeTile[index - 1].gates[prevGate]);
            }
            SpawnObject(tile);
            activeTile.Add(tile);
        }
    }

    void SpawnObject(Tile activeTile){
        int idTargetSpot = Random.Range(0, activeTile.availSpots.Count-1);
        GameObject go = GameObject.Instantiate(buildings["Spawn"].gameObject);
        go.transform.SetParent(activeTile.transform, false);
        go.transform.localPosition = activeTile.availSpots[idTargetSpot].transform.localPosition;

    }


    private void OccupiedGate(Tile tile, Gate gate)
    {
        tile.gates.Remove(gate);
        tile.occupiedGates.Add(gate);
        gate.portal.GetComponent<MeshCollider>().isTrigger = true;
        
    }

    private void UpdateProgress(float progress)
    {
        Debug.Log($"Loading Progress: {progress * 100}%");
    }

    private void OnLoadComplete()
    {
        Debug.Log("All tiles have been loaded!");

        GenerateMap(4);
    }

}
