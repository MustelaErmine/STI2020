using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GenerateLocation : MonoBehaviour
{
    [SerializeField] GameObject living, medicine, police, administration, fireworkers, other, fun;
    const int Lenght = 7, Width = 7;
    Quaternion houses_quaternion = Quaternion.Euler(-90,0,0);
    const float CellSize = 2.8f, HousesY = 1;

    public void Generate (int key)
    {
        // Deleting old houses
        GameObject[] old_houses = GameObject.FindGameObjectsWithTag("House");
        foreach (GameObject h in old_houses)
        {
            Destroy(h);
        }
        bool was_adm = false, was_police = false, was_fire = false, was_medicine = false;
        float x, y;
        for (int row = 0; row < Lenght; row ++)
        {
            for (int column = 0; column < Width; column++)
            {
                x = (row * CellSize + CellSize * 0.5f) - (Lenght * CellSize * 0.5f);
                y = (column * CellSize + CellSize * 0.5f) - (Width * CellSize * 0.5f);
                int cell = int.Parse(row.ToString() + column.ToString());
                int cell_key = cell ^ key;
                int house_type = cell_key % 13;
                if (house_type <= 3)
                    Instantiate(living, new Vector3(x, HousesY, y), houses_quaternion);
                else if (house_type <= 7)
                    Instantiate(fun, new Vector3(x, HousesY, y), houses_quaternion);
                else if (house_type <= 11)
                {
                    if (!was_adm)
                    {
                        Instantiate(administration, new Vector3(x, HousesY, y), houses_quaternion);
                        was_adm = true;
                    }
                    else if (!was_fire)
                    {
                        Instantiate(fireworkers, new Vector3(x, HousesY, y), houses_quaternion);
                        was_fire = true;
                    }
                    else if (!was_medicine)
                    {
                        Instantiate(medicine, new Vector3(x, HousesY, y), houses_quaternion);
                        was_medicine = true;
                    }
                    else if (!was_police)
                    {
                        Instantiate(police, new Vector3(x, HousesY, y), houses_quaternion);
                        was_police = true;
                    }
                }
                else
                    Instantiate(other, new Vector3(x, HousesY, y), houses_quaternion);
            }
        }
        if (!was_adm)
        {
            x = (key + 1) % Lenght;
            y = (key + 1) % Width;
            x = (x * CellSize + CellSize * 0.5f) - (Lenght * CellSize * 0.5f);
            y = (y * CellSize + CellSize * 0.5f) - (Width * CellSize * 0.5f);
            Instantiate(administration, new Vector3(x, HousesY, y), houses_quaternion);
        }
        if (!was_fire)
        {
            x = (key + 2) % Lenght;
            y = (key + 2) % Width;
            x = (x * CellSize + CellSize * 0.5f) - (Lenght * CellSize * 0.5f);
            y = (y * CellSize + CellSize * 0.5f) - (Width * CellSize * 0.5f);
            Instantiate(fireworkers, new Vector3(x, HousesY, y), houses_quaternion);
        }
        if (!was_medicine)
        {
            x = (key + 3) % Lenght;
            y = (key + 3) % Width;
            x = (x * CellSize + CellSize * 0.5f) - (Lenght * CellSize * 0.5f);
            y = (y * CellSize + CellSize * 0.5f) - (Width * CellSize * 0.5f);
            Instantiate(medicine, new Vector3(x, HousesY, y), houses_quaternion);
        }
        if (!was_police)
        {
            x = (key + 4) % Lenght;
            y = (key + 4) % Width;
            x = (x * CellSize + CellSize * 0.5f) - (Lenght * CellSize * 0.5f);
            y = (y * CellSize + CellSize * 0.5f) - (Width * CellSize * 0.5f);
            Instantiate(police, new Vector3(x, HousesY, y), houses_quaternion);
        }
    }

    public void Generate ()
    {
        Generate(int.Parse(GameObject.Find("Canvas/InputField").GetComponent<InputField>().text));
    }
}
