using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    private bool swimming = true;
    private Vector3 spawnPoint;
    GameObject[] sardines;
    GameObject[] whales;
    Vector3[] sardineSpawnPosition;
    Vector3[] whaleSpawnPosition;
    GameObject[] carps;
    Vector3[] carpSpawnPosition;
    GameObject[] probes;
    Vector3[] probeSpawnPosition;
    int arrayLength, arrayLength2, arrayLength3, arrayLength4;
    float timer, timerWhale, timerCarp, timerProbe;

    // Durations (in seconds)
    private const float SARDINE_RESPAWN_TIME = 5.0f; // Approx 300 frames at 60fps
    private const float WHALE_RESPAWN_TIME = 25.0f;  // Approx 1500 frames
    private const float CARP_RESPAWN_TIME = 5.0f;   // Approx 300 frames
    private const float PROBE_RESPAWN_TIME = 11.6f;  // Approx 700 frames

    // Altitude minimum
    private float minAltitude = 0.0f;

    // Use this for initialization
    void Start()
    {
        int count = 0;
        timer = 0;
        timerWhale = 0;
        timerCarp = 0;
        timerProbe = 0;

        //player spawn point
        spawnPoint = transform.position;

        //get initial position for sardines
        if (sardines == null)
        {
            sardines = GameObject.FindGameObjectsWithTag("SimpleSardineSkinWithControllerBoids");
        }
        arrayLength = sardines.Length;
        sardineSpawnPosition = new Vector3[arrayLength];
        foreach (GameObject pos in sardines)
        {
            sardineSpawnPosition[count] = pos.transform.position;
            count++;
        }

        //get initial position for whales
        if (whales == null)
        {
            whales = GameObject.FindGameObjectsWithTag("Whale");
        }
        arrayLength2 = whales.Length;
        count = 0;
        whaleSpawnPosition = new Vector3[arrayLength2];
        foreach (GameObject pos in whales)
        {
            whaleSpawnPosition[count] = pos.transform.position;
            count++;
        }

        //get initial position for carps
        if (carps == null)
        {
            carps = GameObject.FindGameObjectsWithTag("Carp");
        }
        arrayLength3 = carps.Length;
        count = 0;
        carpSpawnPosition = new Vector3[arrayLength3];
        foreach (GameObject pos in carps)
        {
            carpSpawnPosition[count] = pos.transform.position;
            count++;
        }

        //get initial position for probes
        if (probes == null)
        {
            probes = GameObject.FindGameObjectsWithTag("Probe");
        }
        arrayLength4 = probes.Length;
        count = 0;
        probeSpawnPosition = new Vector3[arrayLength4];
        foreach (GameObject pos in probes)
        {
            probeSpawnPosition[count] = pos.transform.position;
            count++;
        }

    }

    // Update is called once per frame
    void Update()
    {

        //move player
        if (swimming == true)
        {
            Vector3 newPosition = transform.position + Camera.main.transform.forward * 1.0f * Time.deltaTime;
            if (newPosition.y >= minAltitude)
            {
                transform.position = newPosition;
            }
            else
            {
                transform.position = new Vector3(newPosition.x, minAltitude, newPosition.z);
            }
        }

        float deltaTime = Time.deltaTime;

        //respawn sardines
        timer += deltaTime;
        if (timer >= SARDINE_RESPAWN_TIME)
        {
            for (int i = 0; i < arrayLength; i++)
            {
                sardines[i].transform.position = sardineSpawnPosition[i];
            }
            timer = 0;
        }

        //whale movement and respawn
        timerWhale += deltaTime;
        bool shouldResetWhale = timerWhale >= WHALE_RESPAWN_TIME;
        for (int i = 0; i < arrayLength2; i++)
        {
            if (shouldResetWhale)
            {
                whales[i].transform.position = whaleSpawnPosition[i];
            }
            else
            {
                whales[i].transform.Translate(Vector3.back * deltaTime);
                if (whales[i].transform.position.y <= minAltitude)
                {
                    whales[i].transform.position = new Vector3(whales[i].transform.position.x, minAltitude, whales[i].transform.position.z);
                }
            }
        }
        if (shouldResetWhale)
        {
            timerWhale = 0;
        }


        //carp movement and respawn
        timerCarp += deltaTime;
        bool shouldResetCarp = timerCarp >= CARP_RESPAWN_TIME;
        for (int i = 0; i < arrayLength3; i++)
        {
            if (shouldResetCarp)
            {
                carps[i].transform.position = carpSpawnPosition[i];
            }
            else
            {
                carps[i].transform.Translate(Vector3.forward * deltaTime);
                if (carps[i].transform.position.y <= minAltitude)
                {
                    carps[i].transform.position = new Vector3(carps[i].transform.position.x, minAltitude, carps[i].transform.position.z);
                }
            }
        }
        if (shouldResetCarp)
        {
            timerCarp = 0;
        }

        //probe movement and respawn
        timerProbe += deltaTime;
        bool shouldResetProbe = timerProbe >= PROBE_RESPAWN_TIME;
        for (int i = 0; i < arrayLength4; i++)
        {
            if (shouldResetProbe)
            {
                probes[i].transform.position = probeSpawnPosition[i];
            }
            else
            {
                probes[i].transform.Translate(Vector3.up * deltaTime);
                if (probes[i].transform.position.y <= minAltitude)
                {
                    probes[i].transform.position = new Vector3(probes[i].transform.position.x, minAltitude, probes[i].transform.position.z);
                }
            }
        }
        if (shouldResetProbe)
        {
            timerProbe = 0;
        }
    }
}
