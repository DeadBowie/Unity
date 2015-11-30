using UnityEngine;
using System.IO;
using System.Collections.Generic;

[RequireComponent(typeof(ParticleSystem))]
public class Grapher2 : MonoBehaviour
{

    public int resolution = 5;
    bool click1 = false;
    private int currentResolution;
    ParticleSystem m_System;
    private ParticleSystem.Particle[] points = new ParticleSystem.Particle[250000];
    List<float> mas = new List<float>();

    public void tstclick(string ks)
    {
        
        try
        {
            using (StreamReader reader = new StreamReader(ks))
            {
                CreatePoints(ks);
            }
        }
        catch (System.Exception)
        { Debug.Log("Нет такого файла"); }


    }


    void Start()
    {
        
    }

    private void CreatePoints(string ks)
    {
        if (click1) m_System.SetParticles(points, points.Length);
        click1 = true;
        int counter = 0;
        string line;
        using (StreamReader reader = new StreamReader(ks))
        {
            while ((line = reader.ReadLine()) != null)
            {
                float k;
                float.TryParse(line, out k);
                mas.Add(k);
                line = null;
                counter++;

            }
            Debug.Log(counter);
        }
       
        if (resolution < 1 || resolution > 100)
        {
            Debug.LogWarning("Grapher resolution out of bounds, resetting to minimum.", this);
            resolution = 1;
        }
        currentResolution = resolution;
        int count = 0; while ((mas[count] != -1000))
        {

            for (int i = 0; i < resolution; i++)
            {
                try
                {

                    int il = count * resolution / 2 + i;
                    float x = 2 * 256 * count / counter;
                    float a = mas[count] + (mas[count + 2] - mas[count]) * i / resolution;
                    float b = mas[count + 1] + (mas[count + 3] - mas[count + 1]) * i / resolution;
                    points[il].position = new Vector3(a, b, 0);
                    points[il].color = new Color(a / 10, b / 10, 0f);
                    points[il].size = 0.1f;

                }

                catch (System.IndexOutOfRangeException)
                { Debug.Log("Ошибка IndexOutOfRangeException"); }

            }
            count += 2;
        }

        m_System = GetComponent<ParticleSystem>();
        if (currentResolution != resolution)
        {
            CreatePoints(ks);
        }
        m_System.SetParticles(points, points.Length);
        for (int i = 0; i < (int)points.Length; i++) points[i].position = new Vector3(0f, 0f, 0f);
        mas.Clear();


    }


    void Update()
    {
       

    }
}