using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConstructor : MonoBehaviour
{
    // Contains Section Pieces
    [SerializeField] GameObject[] standard_sections;
    [SerializeField] GameObject[] depot_sections;
    [SerializeField] GameObject[] edge_sections;

    // Dictates Size of the City
    [SerializeField] int city_width = 10;
    [SerializeField] int city_height = 10;

    [SerializeField] int total_no_depots = 10;

    // 0 = standard section
    // 1 = Depot section

    // Used to create the border for the city
    private int section_bottom_left_corner = 0;
    private int section_bottom_across = 1;
    private int section_bottom_right_corner = 2;
    private int section_left_up = 3;
    private int section_right_up = 4;
    private int section_top_left_corner = 5;
    private int section_top_across = 6;
    private int section_top_right_corner = 7;

    private int[,] city_grid;


    // Use this for initialization
    void Start ()
    {
        city_grid = new int[city_height, city_width];

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
                EdgeSectionCheck(h, w);
            }
        }
    }



    private void EdgeSectionCheck(int h, int w)
    {
        // Remember the Grid is generate bottom left to top right,
        // across (w), then up (h) repeated

        // If this section is the bottom left section
        if (w == 0 && h == 0)
        {
            city_grid[h, w] = section_bottom_left_corner;
            return;
        }

        // if this id is a bottom across section
        if ((w != 0 && w != city_width - 1) && h == 0)
        {
            city_grid[h, w] = section_bottom_across;
            return;
        }

        // if this id the bottom right corner section
        if ((w == (city_width - 1) && h == 0))
        {
            Debug.Log("Cheese");
            city_grid[h, w] = section_bottom_right_corner;
            return;
        }

        // if this id is a left up section
        if (w == 0 && h > 0 && h < (city_height - 1))
        {
            city_grid[h, w] = section_left_up;
            return;
        }

        // if this id is a right up section
        if (w == (city_width - 1) && h > 0 && h < (city_height - 1))
        {
            city_grid[h, w] = section_right_up;
            return;
        }

        // if this id the top left corner section
        if (w == 0 && h == (city_height - 1))
        {
            city_grid[h, w] = section_top_left_corner;
            return;
        }

        // if this id is a top across section
        if (w > 0 && w < (city_width - 1) && h == (city_height - 1))
        {
            city_grid[h, w] = section_top_across;
            return;
        }

        // if this id is a top across section
        if (w == (city_width - 1) && h == (city_height - 1))
        {
            city_grid[h, w] = section_top_right_corner;
            return;
        }

        // if its not a edge section, set it to a grid section
        // (Depots are added after)
        else
            city_grid[h, w] = 0;
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

                // if section is inner city
                if (h > 0 && w > 0 && (h < city_height - 1) && (w < city_width - 1))
                {
                    // Grid section
                    if (city_grid[h, w] == 0)
                    {
                        Debug.Log(city_grid[h, w]);
                        Instantiate(standard_sections[random_standard_section], section_pos, standard_sections[random_standard_section].transform.rotation);
                    }

                    // Depot section
                    else if (city_grid[h, w] == 1)
                    {
                        Instantiate(depot_sections[random_depot_section], section_pos, depot_sections[random_depot_section].transform.rotation);
                    }
                }

                // if this is an edge section
                if(h == 0 || w == 0 || (h == city_height - 1) || (w == city_width - 1))
                {
                    GenerateEdgeSection(h, w, section_pos);
                }

                section_size_w += 200;
            }

            section_size_w = 100;
            section_size_h += 200;
        }
    }


    private void GenerateEdgeSection(int h, int w, Vector3 section_pos)
    {
        for (int i = 0; i < edge_sections.Length; i++)
        {
            if (i == city_grid[h, w])
            {
                if (i == 2)
                {
                    Debug.Log("Setting Bottom Right Corner");
                }

                Instantiate(edge_sections[i], section_pos, edge_sections[i].transform.rotation);
            }
        }
    }
}
