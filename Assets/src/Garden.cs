using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;
using Unity.VisualScripting;

public class Garden : MonoBehaviour
{
    public Flower[] agentPrefab;
    List<Flower> agents = new List<Flower>();

    [Range(10, 500)]
    public int startingCount = 50;
    public AudioLoudnessDetection detector;
    // Update is called once per frame

    public float loudnessSenibility = 100;
    public float threshold = 0.5f;

    // public AudioSource audioClip;


    public void FillFloorWithFlower()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        Bounds bounds = room.GetRoomBounds();


        float stepSize = 0.5f;

        // float agentColliderSizeX = GetComponent<BoxCollider>().size.x;
        float agentPrefabSizeY = transform.localScale.y;
        float agentPrefabSizeZ = transform.localScale.z;

        // for (int i =)


        for (float j = bounds.min.y * 1.1f; j < bounds.size.y * 0.9; j += stepSize)
            for (float i = bounds.min.x * 1.1f; i < bounds.max.x * 0.9; i += stepSize)
            {
                for (float k = bounds.min.z * 1.1f; k < bounds.max.z * 0.9; k += stepSize)
                {
                    Vector3 randomRotation = new Vector3(Random.Range(0, 360), 0, 0);

                    Quaternion down = Quaternion.Euler(randomRotation);
                    Vector3 position = new Vector3(i, j, k);
                    bool isColliding = room.IsPositionInSceneVolume(position, testVerticalBounds: true, 0.5f);
                    // bool isInsideRoom = room.IsPositionInSceneVolume(position, 1f);

                    if (!isColliding)
                    {

                        Flower newAgent = Instantiate(
                            agentPrefab[Random.Range(0, agentPrefab.Length)],
                            position,
                            down,
                            transform
                        );
                        newAgent.name = "Agent " + agents.Count;
                        agents.Add(newAgent);
                    }

                }
            }
    }
    void Start()
    {
        // audioClip = GetComponent<AudioSource>();
        FindMicrophones();
    }
    void FindMicrophones()
    {
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }
    }
    void Update()
    {

        // float loudness = detector.GetLoudnessFromAudioClip(audioClip.timeSamples, audioClip.clip) * loudnessSenibility;
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSenibility;

        // Debug.Log("Loudness: " + loudness);

        if (loudness > 0.5)
        {
            foreach (var agent in agents)
            {
                agent.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        // OVRCameraRig cameraRig = FindObjectOfType<OVRCameraRig>();

        // if (OVRInput.GetDown(OVRInput.Button.One))
        // {

        //     Vector3 startPosition = cameraRig.rightControllerAnchor.position;
        //     Flower newAgent2 = Instantiate(
        //                                   agentPrefab,
        //                                   startPosition,
        //                                   Quaternion.Euler(Vector3.down),
        //                                   transform
        //                               );
        //     newAgent2.name = "Agent " + agents.Count;
        //     agents.Add(newAgent2);

        //     // for (int i = 0; i < floor.Height; i++)
        //     // {
        //     //     for (int k = 0; k < floor.Width; k++)
        //     //     {
        //     //         Vector3 position = startPosition + new Vector3(i, 0, k); // Adjust this if you want a different spacing
        //     //         Flower newAgent = Instantiate(
        //     //                       agentPrefab,
        //     //                       position,
        //     //                       Quaternion.Euler(Vector3.down),
        //     //                       transform
        //     //                   );
        //     //         newAgent.name = "Agent " + agents.Count;
        //     //         agents.Add(newAgent);
        //     //     }
        //     // }
        // }

        // if (OVRInput.GetDown(OVRInput.Button.Two))
        // {
        //     foreach (var agent in agents)
        //     {
        //         Destroy(agent.gameObject);
        //     }
        //     agents.Clear();
        // }
    }



    // private float CalculateDistanceToWall(Vector3 position, OVRScenePlane wall)
    // {
    //     Vector3 wallNormal = wall.transform.forward;
    //     float distanceToWall = -Vector3.Dot(wallNormal, wall.transform.position);
    //     float distance = Math.Abs(Vector3.Dot(wallNormal, position) + distanceToWall) / wallNormal.magnitude;
    //     return distance;
    // }
}
