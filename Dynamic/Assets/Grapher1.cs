using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Grapher1 : MonoBehaviour
{

    public int resolution = 5;

    private int currentResolution;
    ParticleSystem m_System;
    private ParticleSystem.Particle[] points;
    float[] mas = new float[250000];

    void Start()
    {
        CreatePoints();
    }

    private void CreatePoints()
    {
        int counter = 0;
        string line;
        System.IO.StreamReader file =
        new System.IO.StreamReader(@"C:\Users\user\Documents\Visual Studio 2013\Projects\DynKnn4\Points.txt");
        while ((line = file.ReadLine()) != null)
        {
            float k;
            float.TryParse(line, out k);
            mas[counter] = k;
            line = null;
            counter ++;
           
        }

        file.Close();

        if (resolution < 1 || resolution > 100)
        {
            Debug.LogWarning("Grapher resolution out of bounds, resetting to minimum.", this);
            resolution = 1;
        }
        currentResolution = resolution;
        points = new ParticleSystem.Particle[250000];
        int count = 0; while ((mas[count]!=-1000)){
           
            for (int i = 0; i < resolution; i++)
            {
                try
                {
                    
                    int il = count * resolution/2 + i;
                    float x = 2*256*count/counter;
                    float a = mas[count] + (mas[count + 2] - mas[count]) * i / resolution;
                    float b = mas[count + 1] + (mas[count + 3] - mas[count + 1]) * i / resolution;
                    points[il].position = new Vector3(a,b,0);
                    points[il].color = new Color(a/10, b/10, 0);
                    points[il].size = 0.1f;

                }

                catch (System.IndexOutOfRangeException)
                { }
                
            }
            count += 2;
        }
       


    }

    void Update()
    {
        
        m_System = GetComponent<ParticleSystem>();
        if (currentResolution != resolution)
        {
            CreatePoints();
        }
        m_System.SetParticles(points, points.Length);
    }
}