using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityConstruction : MonoBehaviour
{
    // Building array
    public GameObject[] buildings;

    // Road Peices
    public GameObject road_x;
    public GameObject road_z;
    public GameObject cross_road;

    // City Size
    [SerializeField] int map_width = 20;
    [SerializeField] int map_height = 20;

    [SerializeField] int max_block_length = 8;

    // Spacing between Grid spaces
    [SerializeField] float building_spacing = 1.0f;

    // Bool for user Seed
    [SerializeField] bool user_seed_set = false;
    [SerializeField] int user_seed_number;


    // Perlin Noise Data
    private int[,] map_grid;

    private void Start()
    {
        // Setup the Mapgrid
        map_grid = new int[map_width, map_height];

        GenerateBuildingData();

        if (user_seed_set == false)
        {
            GenerateRoadData();

            PopulateRoads();
        }

        PopulateBuildings();
    }



    private void GenerateBuildingData()
    {
        int seed = 0;

        // if user hasn't set a seed
        if (user_seed_set == false)
        {
            // needs to be Seeded, else same map!
            seed = Random.Range(0, 100);
        }
        
        // if user has set a seed
        if (user_seed_set == true)
        {
            seed = user_seed_number;
        }

        // Generate Map Data
        for (int h = 0; h < map_height; h++)
        {
            for (int w = 0; w < map_width; w++)
            {
                // Populate mapGrid with Perlin Values, representing
                // which building will be used at this position
                map_grid[w, h] = (int)(Mathf.PerlinNoise(w / 10.0f + seed, h / 10.0f + seed) * 10);
            }
        }
    }



    private void GenerateRoadData()
    {
        // X Axis (Right)
        int x = 0;
        for (int n = 0; n < map_height; n++)
        {
            for (int h = 0; h < map_height; h++)
            {
                // Set this position as a Horizontal Road
                map_grid[x, h] = -1;
            }

            x += 3;

            // If weve gone past the end of the grid
            if (x >= map_width)
                break;
        }

        // Z Axis (Up)
        int z = 0;
        for (int n = 0; n < map_width; n++)
        {
            for (int w = 0; w < map_width; w++)
            {
                // if its already a road from the Z axis,
                // update to a cross section
                if (map_grid[w, z] == -1)
                {
                    map_grid[w, z] = -3;
                }

                // otherwise its just a road on the Z axis
                else
                    map_grid[w, z] = -2;
            }

            // Use a Random Range to increase and Vary block Lengths
            z += Random.Range(3, max_block_length);

            // If weve gone past the end of the grid
            if (z >= map_height)
                break;
        }
    }



    private void PopulateBuildings()
    {
        for (int h = 0; h < map_height; h++)
        {
            for (int w = 0; w < map_width; w++)
            {
                int gridID = map_grid[w, h];

                Vector3 pos = new Vector3(w * building_spacing, 0, h * building_spacing);

                int building_no = 0;

                // 10 because of they Way im using Perlin Noise...
                // This is checking the no. stored in the mapGrid
                // Using this number it decided which building to spawn
                // at this location...
                for (int i = 2; i <= 10; i++, i++)
                {
                    if (gridID < i && gridID >= 0)
                    {
                        CreateBuilding(building_no, pos);

                        // Reset Building No and Height
                        building_no = 0;
                        break;
                    }

                    building_no++;
                }
            }
        }
    }



    private void PopulateRoads()
    {
        // Cycle through grid and create the roads
        for (int h = 0; h < map_height; h++)
        {
            for (int w = 0; w < map_width; w++)
            {
                int gridID = map_grid[w, h];

                Vector3 pos = new Vector3(w * building_spacing,
                    0, h * building_spacing);

                if (gridID < -2)
                {
                    CreateRoad(cross_road, pos);
                }
                else if (gridID < -1)
                {
                    CreateRoad(road_x, pos);
                }
                else if (gridID < 0)
                {
                    CreateRoad(road_z, pos);
                }
            }
        }
    }



    private void CreateBuilding(int building_no, Vector3 pos)
    {
        Instantiate(buildings[building_no], pos, buildings[building_no].transform.rotation);
    }



    private void CreateRoad(GameObject road_type, Vector3 pos)
    {
        Instantiate(road_type, pos, road_type.transform.rotation);
    }
}