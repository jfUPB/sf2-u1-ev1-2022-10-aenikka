using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.Text;
using Microsoft.SqlServer.Server;

public class AppUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SerialControllerCustom SerialController1;
    [SerializeField] private SerialControllerBinary SerialController2;

    [SerializeField] private Text buttonAutomaticModeText;

    // For controller1

    [SerializeField] private Dropdown dropdownSerialPortsAvailableController1;
    [SerializeField] private Text inputFieldController1Speed;
    [SerializeField] private Text buttonOpenController1Text;
    [SerializeField] private Text textInputController1LastRx;
    [SerializeField] private Text textInputController1LastTx;
    [SerializeField] private Text textOutputWriteController1;
    [SerializeField] private Text textInputReadController1;

    // For controller2
    [SerializeField] private Dropdown dropdownSerialPortsAvailableController2;
    [SerializeField] private Text buttonOpenController2Text;
    [SerializeField] private Text inputFieldController2Speed;
    [SerializeField] private Text textInputController2LastRx;
    [SerializeField] private Text textInputController2LastTx;
    [SerializeField] private Text textOutputWriteController2;
    [SerializeField] private Text textInputReadController2;

    private bool isPortOpenController1 = false;
    private bool isPortOpenController2 = false;

    private bool isAutomaticMode = false;

    private float nextActionTime = 0.0f;
    public float period = 0.1f;

    void Start()
    {
        populatePortsForContoller1();
        populatePortsForContoller2();
    }

    public void setAutomaticMode()
    {
        if (isAutomaticMode == false)
        {
            buttonAutomaticModeText.text = "MANUAL MODE";
            isAutomaticMode = true;
        }
        else
        {
            buttonAutomaticModeText.text = "AUTOMATIC MODE";
            isAutomaticMode = false;
        }
    }


    private void populatePortsForContoller1()
    {
        dropdownSerialPortsAvailableController1.ClearOptions();
        string[] ports = SerialController1.SerialPortsAvailable();
        if (ports != null)
        {
            Dropdown.OptionData portOption;

            foreach (var port in ports)
            {
                portOption = new Dropdown.OptionData();
                portOption.text = port;
                dropdownSerialPortsAvailableController1.options.Add(portOption);
            }
        }
        else
        {
            Debug.Log("Connect Controller 1");
        }

    }

    private void populatePortsForContoller2()
    {
        dropdownSerialPortsAvailableController2.ClearOptions();
        string[] ports = SerialController2.SerialPortsAvailable();
        if (ports != null)
        {
            Dropdown.OptionData portOption;

            foreach (var port in ports)
            {
                portOption = new Dropdown.OptionData();
                portOption.text = port;
                dropdownSerialPortsAvailableController2.options.Add(portOption);
            }
        }
        else
        {
            Debug.Log("Connect Controller 2");
        }

    }

    public void openClosePortController1()
    {
        if (isPortOpenController1 == false)
        {
            int portSelection = dropdownSerialPortsAvailableController1.value;
            string port = dropdownSerialPortsAvailableController1.options[portSelection].text;
            SerialController1.portName = port;

            try
            {
                int speed = Int32.Parse(inputFieldController1Speed.text);
                SerialController1.baudRate = speed;
            }
            catch
            {
                Debug.Log("Default speed: " + SerialController1.baudRate.ToString());
            }
            isPortOpenController1 = true;
            buttonOpenController1Text.text = "CLOSE";
            SerialController1.OpenPort();
        }
        else
        {
            isPortOpenController1 = false;
            buttonOpenController1Text.text = "OPEN";
            SerialController1.ClosePort();
        }

    }
    public void openClosePortController2()
    {
        if (isPortOpenController2 == false)
        {
            int portSelection = dropdownSerialPortsAvailableController2.value;
            string port = dropdownSerialPortsAvailableController2.options[portSelection].text;
            SerialController2.portName = port;

            try
            {
                int speed = Int32.Parse(inputFieldController2Speed.text);
                SerialController2.baudRate = speed;
            }
            catch
            {
                Debug.Log("Default speed: " + SerialController2.baudRate.ToString());
            }
            isPortOpenController2 = true;
            buttonOpenController2Text.text = "CLOSE";
            SerialController2.OpenPort();
        }
        else
        {
            isPortOpenController2 = false;
            buttonOpenController2Text.text = "OPEN";
            SerialController2.ClosePort();
        }
    }

    public void WriteActuatorController1()
    {
        if (isPortOpenController1 == true)
        {
            textInputController1LastTx.text = textOutputWriteController1.text;
            SerialController1.SendSerialMessage(textOutputWriteController1.text);
        }
        else
        {
            Debug.Log("Serial port is closed");
        }
    }

    public void ReadSensorController1()
    {
        if (isPortOpenController1 == true)
        {
            SerialController1.SendSerialMessage("sensores");
        }
        else
        {
            Debug.Log("Serial port is closed");
        }
    }
    public void ReadActuatorController1()
    {
        if (isPortOpenController1 == true)
        {
            SerialController1.SendSerialMessage("actuadores");
        }
        else
        {
            Debug.Log("Serial port is closed");
        }
    }

    public void WriteActuatorController2()
    {
        if (isPortOpenController2 == true)
        {
            byte[] frame = new byte[3] { 0x02, 0x00, 0X00 };

            if (textOutputWriteController2.text == "11")
            {
                frame[0] = 0x03;
                frame[1] = 0x01;
                frame[2] = 0x01;
                textInputController2LastTx.text = "0x03 0x01 0x01";
            }
            else if (textOutputWriteController2.text == "10")
            {
                frame[0] = 0x03;
                frame[1] = 0x01;
                frame[2] = 0x00;
                textInputController2LastTx.text = "0x03 0x01 0x00";
            }
            else if (textOutputWriteController2.text == "21")
            {
                frame[0] = 0x03;
                frame[1] = 0x02;
                frame[2] = 0x01;
                textInputController2LastTx.text = "0x03 0x02 0x01";
            }
            else if (textOutputWriteController2.text == "20")
            {
                frame[0] = 0x03;
                frame[1] = 0x02;
                frame[2] = 0x00;
                textInputController2LastTx.text = "0x03 0x02 0x00";
            }
            else if (textOutputWriteController2.text == "31")
            {
                frame[0] = 0x03;
                frame[1] = 0x03;
                frame[2] = 0x01;
                textInputController2LastTx.text = "0x03 0x03 0x01";
            }
            else if (textOutputWriteController2.text == "30")
            {
                frame[0] = 0x03;
                frame[1] = 0x03;
                frame[2] = 0x00;
                textInputController2LastTx.text = "0x03 0x03 0x00";
            }
            SerialController2.SendSerialMessage(frame);
        }
        else
        {
            Debug.Log("Serial port is closed");
        }
    }

    public void ReadSensorController2()
    {
        if (isPortOpenController2 == true)
        {
            SerialController2.SendSerialMessage(new byte[3] { 0x01, 0x00, 0x00 });
        }
        else
        {
            Debug.Log("Serial port is closed");
        }
    }
    public void ReadActuatorController2()
    {
        if (isPortOpenController2 == true)
        {
            SerialController2.SendSerialMessage(new byte[3] { 0x02, 0x00, 0x00 });
        }
        else
        {
            Debug.Log("Serial port is closed");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPortOpenController1 == true)
        {
            string msgSC1 = SerialController1.ReadSerialMessage();

            if (msgSC1 != null)
            {
                textInputController1LastRx.text = msgSC1;
                textInputReadController1.text = msgSC1;
            }
        }

        if (isPortOpenController2 == true)
        {
            byte[] msg;
            msg = SerialController2.ReadSerialMessage();

            if (msg != null)
            {
                textInputController2LastRx.text = msg[0].ToString("x2") + msg[1].ToString("x2") + msg[2].ToString("x2");
                textInputReadController2.text = msg[0].ToString("x2") + msg[1].ToString("x2") + msg[2].ToString("x2");
            }
        }

        if (isAutomaticMode == true)
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime = Time.time + period;
                ReadSensorController1();
                ReadSensorController2();
            }
        }
    }
}
