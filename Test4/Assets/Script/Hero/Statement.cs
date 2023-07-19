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
    Buff_04 = 0x_0008_0000,
}

public class Statement : MonoBehaviour
{
    private E_Character_Statement _state = E_Character_Statement.Normal;

    public void ApplyStatusAbnormality(string state_Name) => ChangeState(state_Name, true);

    public void RemoveStatusAbnormality(string state_Name) => ChangeState(state_Name, false);

    public bool IsInNormalState() => _state == E_Character_Statement.Normal;
    
    public bool HasStatusAbnormality(string state_Name)
    {
        //Enum.TryParse : string -> Enum으로 변환해줌 
        if (Enum.TryParse(state_Name, ignoreCase: true, out E_Character_Statement newState))
        { 
            return (_state & newState) != 0;
        }
        else
        {
            Debug.LogWarning("Invalid state_Name: " + state_Name);
            return false;
        }
    }
    
    private void ChangeState(string state_Name, bool add)
    {
        if (TryParseEnum(state_Name, out E_Character_Statement newState))
        {
            if (add)
                _state |= newState; // Use bitwise OR (|) to combine the new state with the current state
            else
                _state &= ~newState; // Use bitwise AND (~) to remove the state from the current state
        }
        else
        {
            Debug.LogWarning("Invalid state_Name: " + state_Name);
        }
    }

    private bool TryParseEnum<T>(string value, out T result) where T : struct
    {
        return Enum.TryParse(value, ignoreCase: true, out result);
    }
    
    // Method to apply a buff or debuff to the character
    public void ApplyBuffOrDebuff(E_Character_Statement buffOrDebuff, bool apply)
    {
        if (apply)
        {
            _state |= buffOrDebuff; // Use bitwise OR (|) to add the buff or debuff to the current state
            ApplyBuffOrDebuffEffect(buffOrDebuff, true); // Apply the corresponding effect
        }
        else
        {
            _state &= ~buffOrDebuff; // Use bitwise AND (~) to remove the buff or debuff from the current state
            ApplyBuffOrDebuffEffect(buffOrDebuff, false); // Remove the corresponding effect
        }
    }

    // Method to apply the effect of a specific buff or debuff
    private void ApplyBuffOrDebuffEffect(E_Character_Statement buffOrDebuff, bool apply)
    {
        switch (buffOrDebuff)
        {
            case E_Character_Statement.Debuff_01:
                if (apply)
                {
                    // Start the logic that reduces HP every second (example implementation)
                    StartCoroutine(Debuff01Effect());
                }
                else
                {
                    // Stop the effect or reverse its impact (e.g., restore HP) upon removing the debuff
                    StopCoroutine(Debuff01Effect());
                }
                break;

            case E_Character_Statement.Buff_01:
                if (apply)
                {
                    // Implement the logic to apply Buff_01 effect (if any)
                }
                else
                {
                    // Implement the logic to remove Buff_01 effect (if any)
                }
                break;

            // Add cases for other buffs and debuffs here

            default:
                Debug.LogWarning("Unsupported buff or debuff: " + buffOrDebuff);
                break;
        }
    }

    // Example implementation for Debuff_01 effect (reduces HP by 1 every second)
    private IEnumerator Debuff01Effect()
    {
        while (HasStatusAbnormality(E_Character_Statement.Debuff_01.ToString()))
        {
            // Implement the logic to reduce the character's HP by 1 unit every second
            // For example, you can access the character's HP variable and decrement it here.
            // For demonstration purposes, we'll assume there's a method called ReduceHP() to do this.
            // Replace this with your actual logic based on your game's architecture.
            ReduceHP(1);

            yield return new WaitForSeconds(1.0f); // Wait for 1 second before reducing HP again
        }
    }

    // Method to reduce the character's HP (replace this with your actual implementation)
    private void ReduceHP(int amount)
    {
        // Implement the logic to reduce the character's HP by the specified amount
    }
    
    
}