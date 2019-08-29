using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.IO.Ports;
using System.IO;

public class ArdBlue_Unity : MonoBehaviour
{

    private static ArdBlue_Unity instance;

    private static SerialPort ardport = new SerialPort("\\\\.\\COM5", 115200);
    public static Thread read;
    public static double[] makezero = { 0.13, 0, 0 };
    private static string[] xyz = {"no","x","y","z","button"};
    public static int slash = 0;
    public static bool check = false;
    public static bool button = false;
    public static double[] ok = new double[3];

    private static bool Th_Check = false;
    //private float timer = 0f;
    public int status = 0;

    private float parryTimer;
    private float slashTimer;

    void Awake() {
        if (instance != null) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }


    void Start()
    {
        
        try
        {
            ardport.Open();
            read = new Thread(new ThreadStart(Serial_read));
            Th_Check = true;
            read.Start();
            //StartCoroutine(setStatus());
        }
        catch (IOException e)
        {
            Debug.Log(e.Message);
        }
        parryTimer = 0f;
        slashTimer = 0f;
    }

    void Update() {
        parryTimer += Time.deltaTime;
        slashTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
            slashed();
        else if (Input.GetKeyDown(KeyCode.W))
            stabbed();
        else if (Input.GetKeyDown(KeyCode.LeftShift))
            pressed();

        if (slash != 0) {

            if (slash < 0) 
                slash *= -1;

            switch (slash) {

                case 1:
                    stabbed();
                    break;

                case 2:
                case 3:
                    slashed();
                    break;

                case 9:
                    pressed();
                    break;


                default:
                    Debug.Log("Not");
                    break;

            }

            slash = 0;

        }
    }

    /*IEnumerator setStatus() {
        float statusTimer = 0f;

        while(Th_Check) {
            yield return new WaitForSeconds(0.04f);
            statusTimer += Time.deltaTime;
            status = 0;
        }
    }*/

    private void OnApplicationQuit() {
        Th_Check = false;
        read.Interrupt();
        read.Abort();
        ardport.Close();
    }

    public static int MaxData(double[] ok)
    {
        int max = 0;
        for (int i = 0; i < ok.Length; i++)
        {
            if (Abs(ok[max]) < Abs(ok[i]))
                max = i;
        }
        return max;
    }

    public static double Abs(double num)
    {
        if (num < 0)
            return -num;
        return num;
    }

    public static int Absi(int num)
    {
        if (num < 0)
            return -num;
        return num;
    }


    void Serial_read()
    {
        string fw;
        string[] tmp;
        double num;
        double l;
        while (Th_Check)
        {
            fw = ardport.ReadLine();
            /////////////////////////////Debug.Log(fw);
            tmp = fw.Split(' ');
            for (int j = 0; j < tmp.Length; j++)
            {

                if (j == 0)
                {
                    if (tmp[j].Equals("0"))
                    {
                        button = true;
                        break;
                    }
                    continue;
                }
                ok[j - 1] = 0;
                num = double.Parse(tmp[j], System.Globalization.CultureInfo.InvariantCulture) + makezero[j - 1];

                l = (j == 1)?2.0:1.3;       /////////////// Important Range 
                
                if (num > l || num < -l)
                {
                    ok[j - 1] = num;
                    if(j == 2 || j == 3) ok[0] = 0;
                    //if(j == 1) Debug.Log(num);
                    check = true;
                }
            }
            if (check && !button)
            {
                slash = MaxData(ok) + 1;
                if(ok[slash-1] < 0){
                    slash = -slash;
                }
                check = false;
            }
            else if (button)
            {
                slash = 9;
                button = false;
            }
            
        }
    }


    public void stabbed() {
        status = 1;
        Debug.Log("Stab");

    }

    public void slashed() {
        if (slashTimer < 0.1f)
            status = 0;
        else {
            status = 2;
            slashTimer = 0f;
            Debug.Log("Slash");
        }
    }

    public void pressed() {
        if (parryTimer < 0.1f)
            status = 0;
        else {
            status = 3;
            parryTimer = 0f;
            Debug.Log("Parring");
        }
    }

    public int checkStatus() {
        int temp = status;
        status = 0;
        //Debug.Log(temp);
        return temp;
    }

}

