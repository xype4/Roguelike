using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<GameObject> roomsInspector = new List<GameObject>();
    [Range(1, 21474836)] public int seed;
    private int maxLevelInspector = 4;
    public bool root = false;
    public Transform parentRoomObject;

    [HideInInspector] public int roomID;
    [HideInInspector] public static int maxLevel = 5;
    public static List<GameObject> rooms = new List<GameObject>();
    public static Transform parentRoomObjectStatic;
    public static List<Transform> roomsCollider = new List<Transform>();
    private List<Transform> doors = new List<Transform>();
    private static System.Random rand;
    public static List<ItemSpawnerDot> itemSpawnPoints = new List<ItemSpawnerDot>();

    void Start()
    {
        if(root)
        {
            if(Settings.seed !="")
                seed = int.Parse(Settings.seed);
            else
                seed = Random.Range(0, 100000000);
            rooms = new List<GameObject>(roomsInspector);
            rand = new System.Random(seed);
            parentRoomObjectStatic = parentRoomObject;
            maxLevel = (int)((float)maxLevelInspector*(Settings.size+1.5f));
            roomID = 0;

            for(int i = 0; i < rooms.Count; i++)
            {
                rooms[i].transform.rotation = new Quaternion(0,0,0,0);
                rooms[i].GetComponent<Room>().roomID = i+1;
            }

            for(int i = 0; i < gameObject.transform.childCount;i++)
                if(gameObject.transform.GetChild(i).tag == "Room")
                {
                    roomsCollider.Add(gameObject.transform.GetChild(i));
                }

            GenerateRooms(0);
        }
    }
    public void GenerateRooms(int level)
    {
        if(level>=maxLevel)
        {
            for(int i = 0; i < gameObject.transform.childCount;i++)
                if(gameObject.transform.GetChild(i).tag == "Door")
                {
                    GameObject door = gameObject.transform.GetChild(i).gameObject;
                    for(int j = 0; j < door.transform.childCount; j++)
                    {
                        if(door.transform.GetChild(j).tag == "Wall")
                            door.transform.GetChild(j).gameObject.SetActive(true);
                        else
                            door.transform.GetChild(j).gameObject.SetActive(false);
                    } 
                }  
                    
            return;
        }

        for(int i = 0; i < gameObject.transform.childCount;i++)
        {
            if(gameObject.transform.GetChild(i).tag == "Door")
            {
                doors.Add(gameObject.transform.GetChild(i));
            }  

            if(gameObject.transform.GetChild(i).tag == "ItemSlot")
            {
                itemSpawnPoints.Add(new ItemSpawnerDot(gameObject.transform.GetChild(i).transform.position, level));
            }
        }

        List<Room> newRoomsGenerate = new List<Room>();


        for(int i = 0;i< doors.Count;i++)
        {
            Vector3 doorCoord = new Vector3();
            for(int j = 0; j < doors[i].transform.childCount; j ++)
            {
                if(doors[i].transform.GetChild(j).tag == "DoorPoint")
                {
                    doorCoord = doors[i].transform.GetChild(j).position;
                    break;
                }
            }

            bool spawned = false;

            int newRoomNumberBase = (int)rand.Next()%rooms.Count; //Инициализация начальной команаты
            int newRoomNumberCurrent = newRoomNumberBase;

            while (spawned == false)
            {
                newRoomNumberCurrent++;
                if(newRoomNumberCurrent >= rooms.Count) //Переполнен лист команат
                    newRoomNumberCurrent= 0;

                if(newRoomNumberCurrent == newRoomNumberBase)//Перебраны все возможные комтаны
                {
                    for(int j = 0; j < doors[i].transform.childCount; j++)
                    {
                        if(doors[i].transform.GetChild(j).tag == "Wall")
                            doors[i].transform.GetChild(j).gameObject.SetActive(true);
                        else
                            doors[i].transform.GetChild(j).gameObject.SetActive(false);
                    } 
                    break;
                } 

                Transform rotationRoom = rooms[newRoomNumberCurrent].transform;

                if(roomID == rooms[newRoomNumberCurrent].GetComponent<Room>().roomID) // если id соседних комант равны
                    continue;

                GameObject newRoom = new GameObject();
                Destroy(newRoom,0);             //Не знаю как, но оно надо

                bool deleted = false;

                for(int rotationRoomStep = 0; rotationRoomStep < 4; rotationRoomStep++)
                {
                    rotationRoom.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
                    newRoom = Instantiate(rooms[newRoomNumberCurrent], doorCoord, rotationRoom.rotation, parentRoomObjectStatic); 

                    deleted = false;

                    for(int j = 0; j< roomsCollider.Count; j++)
                    {
                        for(int k = 0; k < newRoom.transform.childCount; k++)
                        {
                            if(newRoom.transform.GetChild(k).tag == "Room")
                            {
                                if (roomsCollider[j].GetComponent<Collider>().bounds.Intersects(newRoom.transform.GetChild(k).gameObject.GetComponent<Collider>().bounds))
                                {
                                    Destroy(newRoom,0);
                                    deleted = true;
                                    break;
                                }
                            }
                        }
                    } 
                    if(deleted == true)
                        continue;
                    else
                        break;
                }

                if(deleted == true)
                continue;
                
                for(int j= 0; j < newRoom.transform.childCount;j++)
                    if(newRoom.transform.GetChild(j).tag == "Room")
                    {
                        roomsCollider.Add(newRoom.transform.GetChild(j));
                    }

                spawned = true;
                newRoomsGenerate.Add(newRoom.GetComponent<Room>());
            }
        }

        for(int i =0 ;i< newRoomsGenerate.Count;i++)
        {
            newRoomsGenerate[i].GenerateRooms(level+1);
        }

        if(root)
        {
            gameObject.GetComponent<ItemSpawner>().StartItemGenerate(itemSpawnPoints);
        }
    }
}
