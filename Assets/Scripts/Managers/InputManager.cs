using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private List<KeyCommand> keyCommands = new List<KeyCommand>();
    private List<AxisCommand> axisCommands = new List<AxisCommand>();
    private List<Vector2Command> vector2Commands = new List<Vector2Command>();

    private void Awake()
    {
        //BindKey(KeyCode.Alpha1, null);
    }

    private void Update()
    {
        HandleInput();
    }

    public void BindKey(KeyCode _key, ICommandKey _command, bool _executesOnKeyHeld = false)
    {

        keyCommands.Add(new KeyCommand(_key, _command, _executesOnKeyHeld));
    }

    public void UnbindKey(KeyCode _key)
    {
        var commands = keyCommands.FindAll(x => x.key == _key);
        commands.ForEach(x => keyCommands.Remove(x));
    }

    public void BindAxis(string _name, ICommandAxis _command)
    {
        if (!IsAxisAvailable(_name))
        {
            Debug.Log("Axis " + _name + " not defined in project settings!");
            return;
        }
        //var commands = axisCommands.FindAll(x => x.name == _name);
        AxisCommand axisCommand = new AxisCommand(_name, _command);
        
        axisCommands.Add(new AxisCommand(_name, _command));
    }

    public void UnbindAxis(string _name)
    {
        var commands = axisCommands.FindAll(x => x.name == _name);
        commands.ForEach(x => axisCommands.Remove(x));
    }

    public void BindVector2(string _xAxis, string _yAxis, ICommandVector2 _command)
    {
        if (!IsAxisAvailable(_xAxis) || !IsAxisAvailable(_yAxis))
        {
            Debug.Log("Axis " + _xAxis + " or " + _yAxis + " not defined in project settings!");
            return;
        }
        //var commands = axisCommands.FindAll(x => x.name == _name);
        //AxisCommand axisCommand = new AxisCommand(_name, _command);

        //axisCommands.Add(new AxisCommand(_name, _command));
        Vector2Command vector2Command = new Vector2Command(_xAxis, _yAxis, _command);
        vector2Commands.Add(vector2Command);
    }
    //public float GetAxis(string _name)
    //{
    //    if (!axisCommands.ContainsKey(_name))
    //    {
    //        Debug.Log("Axis " + _name + " is not registered!");
    //        return 0f;
    //    }
    //    return axisCommands[_name].pValue;
    //}

    //public float GetAxisRaw(string _name)
    //{
    //    if (!axisCommands.ContainsKey(_name))
    //    {
    //        Debug.Log("Axis " + _name + " is not registered!");
    //        return 0f;
    //    }
    //    return axisCommands[_name].pRawValue;
    //}


    private void HandleInput()
    {
        foreach (var keyCommand in keyCommands)
        {
            if (keyCommand.command == null)
            {
                continue;
            }

            if (keyCommand.executesOnKeyHeld)
            {
                if (Input.GetKey(keyCommand.key))
                {
                    keyCommand.command.Execute();
                }
            }
            else
            {
                if (Input.GetKeyDown(keyCommand.key))
                {
                    keyCommand.command.Execute();
                }
            }
        }

        foreach (var axisCommand in axisCommands)
        {
            if (axisCommand.command == null)
            {
                continue;
            }

            if (axisCommand.pValue != 0.0f)
            {
                axisCommand.command.Execute(axisCommand);
            }
        }

        foreach (var vector2Command in vector2Commands)
        {
            if (vector2Command.command == null)
            {
                continue;
            }

            if (vector2Command.pValue != Vector2.zero)
            {
                vector2Command.command.Execute(vector2Command);
            }
        }
    }

    private bool IsAxisAvailable(string _axis)
    {
        try
        {
            Input.GetAxis(_axis);
            return true;
        }
        catch (UnityException exc)
        {
            return false;
        }
    }
}

public class KeyCommand
{
    public KeyCode key;
    public ICommandKey command;
    public bool executesOnKeyHeld;

    public KeyCommand(KeyCode _key, ICommandKey _command, bool _executeOnKeyHeld = false)
    {
        key = _key;
        command = _command;
        executesOnKeyHeld = _executeOnKeyHeld;
    }
}

public class AxisCommand
{
    public string name;
    public ICommandAxis command;

    public float pValue
    {
        get 
        { 
            return Input.GetAxis(name); 
        }
    }
    
    public float pRawValue
    {
        get
        {
            return Input.GetAxisRaw(name);
        }
    }

    public AxisCommand(string _name, ICommandAxis _command)
    {
        name = _name;
        command = _command;
    }
}

public class Vector2Command
{
    public string xAxis, yAxis;
    public ICommandVector2 command;

    public Vector2 pValue
    {
        get
        {
            return new Vector2(Input.GetAxis(xAxis), Input.GetAxis(yAxis));
        }
    }

    public Vector2 pRawValue
    {
        get
        {
            return new Vector2(Input.GetAxisRaw(xAxis), Input.GetAxisRaw(yAxis));
        }
    }

    public Vector2Command(string _xAxis, string _yAxis, ICommandVector2 _command)
    {
        xAxis = _xAxis;
        yAxis = _yAxis;
        command = _command;
    }
}
