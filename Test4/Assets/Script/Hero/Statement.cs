using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum E_Character_Statement
{
    Normal = 0x_0000_0000,
    Debuff_01 = 0x_0000_0001,
    Debuff_02 = 0x_0000_0002,
    Debuff_03 = 0x_0000_0004,
    Debuff_04 = 0x_0000_0008,

    Buff_01 = 0x_0001_0000,
    Buff_02 = 0x_0002_0000,
    Buff_03 = 0x_0004_0000,
    Buff_04 = 0x_0008_0000
}

public class Statement : MonoBehaviour
{
    private E_Character_Statement _state = E_Character_Statement.Normal;

    // Dictionary to map function names to corresponding action delegates
    private Dictionary<string, Action> _buffDebuffActions = new Dictionary<string, Action>();

    private void Awake()
    {
        // Initialize the dictionary with function names and their corresponding actions
        _buffDebuffActions["Debuff_01"] = Debuff_01;
        _buffDebuffActions["Debuff_02"] = Debuff_02;
        _buffDebuffActions["Buff_01"] = Buff_01;

        // Add more entries for other buffs and debuffs as needed
    }

    // Method to apply a buff or debuff to the character by function name
    public void ApplyBuffOrDebuff(string functionName, bool apply)
    {
        if (_buffDebuffActions.TryGetValue(functionName, out Action action))
        {
            // If the function name exists in the dictionary, call the corresponding action
            action.Invoke();

            // Now we also update the character's state based on the provided function name
            if (Enum.TryParse(functionName, ignoreCase: true, out E_Character_Statement newState))
            {
                _state = apply ? _state | newState : _state & ~newState;
            }
            else
            {
                Debug.LogWarning("Invalid function name: " + functionName);
            }
        }
        else
        {
            Debug.LogWarning("Invalid function name: " + functionName);
        }
    }

    // Buff and debuff functions on the main character (add your logic here)
    private void Debuff_01()
    {
        // Implement Debuff_01 logic here
    }

    private void Debuff_02()
    {
        // Implement Debuff_02 logic here
    }

    private void Buff_01()
    {
        // Implement Buff_01 logic here
    }

    // Add more functions for other buffs and debuffs as needed
    
}