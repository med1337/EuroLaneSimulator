using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConstructor : MonoBehaviour
{
    // Contains Section Pieces
    [SerializeField] GameObject[] standard_sections;
    [SerializeField] GameObject[] depot_sections;

    // Dictates Size of the City
    [SerializeField] int city_width = 10;
    [SerializeField] int city_height = 10;

    [SerializeField] int total_no_depots = 10;


    private int[,] city_grid;

    // 0 = standard section
    // 1 = Depot section


    // Use this for initialization
    void Start ()
    {
        city_grid = new int[city_width, city_height];

        GenerateCityData();
        GenerateDepotData();

        PopulateCity();
	}
	


    private void GenerateCityData()
    {
        for (int h = 0; h < city_height; h++)
        {
            for (int w = 0; w < city_width; w++)
            {
                city_grid[h, w] = 0;
            }
        }
    }



    private void GenerateDepotData()
    {
        int w = 0;
        int h = 0;

        for (int i = 0; i < total_no_depots; i++)
        {
            h = Random.Range(3, city_height - 3);
            w = Random.Range(3, city_width - 3);

            city_grid[h, w] = 1;
        }
    }



    private void PopulateCity()
    {
        Vector3 section_pos = Vector3.zero;

        int section_size_w = 100;
        int section_size_h = 100;

        for (int h = 0; h < city_height; h++)
        {
            for (int w = 0; w < city_width; w++)
            {
                int random_standard_section = Random.Range(0, standard_sections.Length);
                int random_depot_section = Random.Range(0, depot_sections.Length);

                section_pos = new Vector3(section_size_w, 0.0f, section_size_h);

                if (city_grid[h, w] == 0)
                {
                    Instantiate(standard_sections[random_standard_section], section_pos, standard_sections[random_standard_section].transform.rotation);
                }

                else if(city_grid[h, w] == 1)
                {
                    Instantiate(depot_sections[random_depot_section], section_pos, depot_sections[random_depot_section].transform.rotation);
                }

                section_size_w += 200;
            }

            section_size_w = 100;
            section_size_h += 200;
        }
    }
}
