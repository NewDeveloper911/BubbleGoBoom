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

    //int roomOrder should be in range 0-3
    /*
        with 0 with up entry,
        1 - right entry
        2 - down entry
        3 - left entry
     */

    //Almost works perfectly, but now, need to investigate if the size and position of the bounding boxes will always match the room prefabs once loaded into the game
        //That, and also work on optimisation of this function, as it causes my potato of a laptop to crash frequently.
    public void GenerateNextRoom(int roomOrder, Vector3 doorPosition)
    {
        if (currentRoomCount >= maxRooms) return;

        bool haveValidRoom = false;
        GameObject selectedRoom = null;
        Vector3 spawnPosition = doorPosition;
        //Room size shouldn't be so hard-coded, but we should measure the size of the room to be piced later on in the script.

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
                //Should check if the selected room is that corner one. Needs to be lowered a bit first before used to connect the rooms
                int roomSize = (int)selectedRoom.gameObject.transform.GetComponent<Renderer>().bounds.size.x / 2;
                int yOffset = selectedRoom.gameObject.name == "L Room Variant" ? roomSize : 0;
                //Don't forget to disable the corresponding trigger so that returning back doesn't spawn a different one
                string triggerToDisable = "Wall" + (3 - roomOrder).ToString();
                GameObject.Find(triggerToDisable).GetComponent<Collider2D>().enabled = false;
                switch (roomOrder)
                {

                    case 0: spawnPosition += new Vector3(0, roomSize, 0); break;
                    case 1: spawnPosition += new Vector3(roomSize, yOffset, 0); break;
                    case 2: spawnPosition += new Vector3(0, -roomSize, 0); break;
                    case 3: spawnPosition += new Vector3(-roomSize, yOffset, 0); break;
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
