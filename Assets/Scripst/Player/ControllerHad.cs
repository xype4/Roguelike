using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ControllerHad : MonoBehaviour
{
    [HideInInspector]
    public float Sensetive;
    [HideInInspector]
    public float MoveX = 0f;
    private float MoveY = 0f;
    private float AngleY = 80;
    private float time_Factor;
    //public SettingsSave SettingsMenu;
    private GameObject Body;
    private string SensetivePath;
    
    void Start()
    {
        Body = transform.parent.gameObject;

        SensetivePath = Application.persistentDataPath + "/Settings.info";
        if (File.Exists(SensetivePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(SensetivePath, FileMode.Open);
            float sens = (float)bf.Deserialize(fs);
            fs.Close();
            Sensetive = sens;
        }
        else
            Sensetive = 2;
    }
    void Update()
    {   
        time_Factor = Body.GetComponent<ControllerBody>().timeFactor;
        MoveY += Input.GetAxis("Mouse Y")* Sensetive*Time.deltaTime * 70 * time_Factor;
        MoveX += Input.GetAxis("Mouse X")* Sensetive*Time.deltaTime * 70 * time_Factor;
        if(MoveX > 360f) MoveX -=360f;
        if(MoveX < -360f) MoveX +=360f;
        if(MoveY > AngleY) MoveY =AngleY;
        if(MoveY < -AngleY) MoveY =-AngleY;
        transform.rotation = Quaternion.Euler(-MoveY,MoveX,0f);
    }
    public void Sens(float sens)
    {
        Sensetive = sens;
    }

    public void SensetiveUpdate()
    {
        //Sensetive = SettingsMenu.Sensetive;
    }
}
