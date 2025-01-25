using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] Transform player;
    [SerializeField] Room firstRoom;
    // For the first room, 
    public static Room currentRoom;
    [SerializeField] Transform level;
    [SerializeField] Transform room; //Should initially be firstRoom
    [SerializeField] float minLenght;
    [SerializeField] float maxLenght;
    [SerializeField] int numOfRooms;
    [SerializeField] List<GameObject> rooms;
    void Start()
    {
        //firstRoom.OnDrawGizmos();
        Debug.Log(firstRoom.getXLength());
        currentRoom = firstRoom;
    }

    Transform MakeRoom(float new_room_x_length, float new_room_y_length){
        Vector3 side;
        Vector3 new_room_abs_center;
        if (currentRoom.getExitDoorInt() == 1){
            side = new Vector3(0,1,0);
        }
        else if (currentRoom.getExitDoorInt() == 2){
            side = new Vector3(1,0,0);
        }
        else{
            side = new Vector3(-1,0,0);
        }

        Vector3 displacement_from_current_room = new Vector3(side.x*currentRoom.getXLength()/2, side.y*currentRoom.getYLength()/2);
        Vector3 new_room_relative_center = new Vector3(side.x*new_room_x_length/2, side.y*new_room_y_length/2,0);

        // gives the position of the next room relative to the position of the level gameobject
        new_room_abs_center = room.localPosition + displacement_from_current_room + new_room_relative_center;

        GameObject newRoom = Instantiate(roomPrefab, new_room_abs_center, Quaternion.identity, level);
    
        
        int wall_for_exit_door = Random.Range(0,3);
        Transform newDoor = newRoom.transform.GetChild(4);
        newDoor.position = newRoom.transform.GetChild(wall_for_exit_door).position;

        if (wall_for_exit_door==0){
            player.position = player.position + new Vector3(-1,0,0);}
        else if (wall_for_exit_door==1){
            player.position = player.position + new Vector3(0,1,0);}
        else{
            player.position = player.position + new Vector3(1,0,0);}
        
        //Get new start side
        int new_start_side;
        if (currentRoom.getExitDoorInt() == 1){new_start_side = 3;}
        else if (currentRoom.getExitDoorInt() == 0){new_start_side = 2;}
        else{new_start_side = 0;}
        
        currentRoom = new Room(room, this);
    
        return newRoom.transform;
    }

        

    void OnTriggerEnter2D(Collider2D other){
        if (other == player.GetComponent<Collider2D>()){
            room = MakeRoom(Random.Range(minLenght, maxLenght), Random.Range(minLenght, maxLenght));
        }
    }

    // TODO: Maybe use start to generate the first room in code
    

    
}
