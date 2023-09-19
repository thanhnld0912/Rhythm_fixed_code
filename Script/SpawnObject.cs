using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private float spawnSpeed;
    [SerializeField] private Vector3 spawnPosition;
    private float delayTime;
    private float lastSpawnTime;
    private float[] spawnTimes;
    private int spawnIndex;

    void Start()
    {
        // Calculate spawn times for all objects
        float[] spectrumData = new float[256];
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
        float average = 0f;
        for (int i = 0; i < spectrumData.Length; i++)
        {
            average += spectrumData[i];
        }
        average /= spectrumData.Length;
        float bpm = average * 10;

        spawnTimes = new float[10];
        spawnTimes[0] = Time.time + 60f / (bpm * spawnSpeed);
        delayTime = 0.5f;

        for (int i = 1; i < spawnTimes.Length; i++)
        {
            spawnTimes[i] = spawnTimes[i - 1] + delayTime;
        }

        // Spawn the first object
        GameObject newObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        lastSpawnTime = Time.time;
        spawnIndex++;
    }

    void Update()
    {
        // Calculate the time for the next object to spawn
        float nextSpawnTime = lastSpawnTime + delayTime;

        // If the time for the next object to spawn has passed,
        // spawn a new object and update the last spawn time
        if (Time.time >= nextSpawnTime && spawnIndex < spawnTimes.Length)
        {
            GameObject newObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            lastSpawnTime = Time.time;
            spawnIndex++;
        }

        // Stop spawning objects if the total spawn time has passed
        if (Time.time >= spawnTimes[spawnTimes.Length - 1])
        {
            enabled = false;
        }
    }
}