using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room{
        //[SerializeField] private float yLength;
        //[SerializeField] private  float xLength;
        private int exit_door_side;
        private int start_door_side; //integer from 1 to 3, representing left, top, right
        int room_num;
        [SerializeField]Transform roomObj;
        public Room(Transform room, RoomGenerator roomGen){
            room_num = RoomGenerator.currentRoom.room_num+1;
            roomObj = room;
            OnDrawGizmos();
            /* xLength = bounds.size.x;
            yLength = bounds.size.y;
 */
            //Figuring out which side the exit and start doors are
            /* exit_door_side =exit_door_index;
            start_door_side =start_door_index;
            if (exit_door_index>2||start_door_index>2){
                Debug.Log("door_index out of range");
            } */
            //roomObj = roomGenerator.MakeRoom(x,y);  
        }
        public int getExitDoorInt(){return exit_door_side;}
        public int getStartDoorInt(){return start_door_side;}
        public float getXLength(){return GetBoundingBox(roomObj).size.x;}
        public float getYLength(){return GetBoundingBox(roomObj).size.y;}

        // Method to calculate the bounding box of all child objects
        public Bounds GetBoundingBox(Transform room)
        {
            Bounds bounds = new Bounds(room.position, Vector3.zero);

            // Loop through all child objects and extend the bounds to include their bounds
            foreach (Renderer renderer in room.GetComponentsInChildren<Renderer>())
            {
                bounds.Encapsulate(renderer.bounds);
            }

            return bounds;
        }
        // Method to draw the bounding box in the Scene view using Gizmos
        public void OnDrawGizmos()
        {
            //Debug.Log("In");
            // Get the bounding box of the object and its children
            Bounds bounds = GetBoundingBox(roomObj);

            // Set Gizmos color
            Gizmos.color = Color.red;

            // Draw a wireframe box around the bounding box
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
    }
