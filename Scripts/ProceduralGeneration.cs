using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] GameObject[] roomPrefabs; //This is the array we want to store the rooms which can be instantiated
    //These variables dictate the minimum and maximum number of rooms generated
    [Range(1,15)] [SerializeField] int minRooms;
    [Range(15, 100)] [SerializeField] int maxRooms;
    [SerializeField] int currentRoomCount; //Bruh, nearly forgot to track the number of rooms currently generating

    //Should probably have a function on the room triggers which should call a function to position the room

    void Start()
    {
        
    }

    //int roomOrder should be in range 0-3
    /*
        with 0 with up entry,
        1 - right entry
        2 - down entry
        3 - left entry
     */
    public void GenerateNextRoom(int roomOrder, Vector3 doorPosition)
    {
        if (currentRoomCount >= maxRooms) return;

        bool haveValidRoom = false;
        GameObject selectedRoom = null;
        Vector3 spawnPosition = doorPosition;
        float roomSize = 10f; // Size of your rooms (adjust based on your prefabs)

        while (!haveValidRoom)
        {
            int randomIndex = UnityEngine.Random.Range(0, roomPrefabs.Length);
            GameObject roomPrefab = roomPrefabs[randomIndex];
            RoomData roomData = roomPrefab.GetComponent<RoomData>();

            if (roomData == null)
            {
                Debug.LogWarning("Room prefab missing RoomData component!");
                continue;
            }

            // Check if the room has a door matching the player's entry
            switch (roomOrder)
            {
                case 0: if (roomData.hasSouthDoor) haveValidRoom = true; break; // Coming from below
                case 1: if (roomData.hasWestDoor) haveValidRoom = true; break;  // Coming from left
                case 2: if (roomData.hasNorthDoor) haveValidRoom = true; break; // Coming from above
                case 3: if (roomData.hasEastDoor) haveValidRoom = true; break;  // Coming from right
            }

            if (haveValidRoom)
            {
                selectedRoom = roomPrefab;
                // Calculate new room position
                switch (roomOrder)
                {
                    case 0: spawnPosition += new Vector3(0, roomSize, 0); break;
                    case 1: spawnPosition += new Vector3(roomSize, 0, 0); break;
                    case 2: spawnPosition += new Vector3(0, -roomSize, 0); break;
                    case 3: spawnPosition += new Vector3(-roomSize, 0, 0); break;
                }
            }
        }

        if (selectedRoom != null)
        {
            Instantiate(selectedRoom, spawnPosition, Quaternion.identity);
            currentRoomCount++;
        }
    }

}
