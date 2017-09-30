using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConstructor : MonoBehaviour
{
    // Contains Section Pieces
    [SerializeField] GameObject[] level_sections;

    // Dictates Size of the City
    [SerializeField] int city_width = 10;
    [SerializeField] int city_height = 10;



    // Use this for initialization
    void Start ()
    {
        GenerateLevel();
	}
	


    private void GenerateLevel()
    {
        Vector3 section_pos = Vector3.zero;

        int section_size_w = 100;
        int section_size_h = 100;

        int random_section = Random.Range(0, level_sections.Length);

        for (int h = 0; h < city_height; h++)
        {
            for (int w = 0; w < city_width; w++)
            {
                section_pos = new Vector3(section_size_w, 0.0f, section_size_h);

                Instantiate(level_sections[random_section], section_pos, level_sections[random_section].transform.rotation);

                section_size_w += 200;
            }

            section_size_w = 100;
            section_size_h += 200;
        }
    }
}
