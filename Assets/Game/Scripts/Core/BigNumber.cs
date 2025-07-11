using System;
using UnityEngine;

namespace Game.Scripts.Core
{
[Serializable]
public struct BigNumber : IComparable<BigNumber>, ISerializationCallbackReceiver
{
    // These are the real fields for your logic.
    // They are marked NonSerialized so JsonUtility ignores them.
    [NonSerialized] public double Mantissa;
    [NonSerialized] public int Exponent;

    // --- SURROGATE FIELDS FOR SERIALIZATION ---
    // These are the simple fields that JsonUtility CAN save.
    // We hide them in the inspector to avoid confusion.
    [HideInInspector] [SerializeField] private string _mantissaString;
    [HideInInspector] [SerializeField] private int _exponent; // int is already supported!

    // The rest of the struct (constructors, operators, etc.) remains the same...
    #region Logic and Operators

    public BigNumber(double mantissa, int exponent)
    {
        Mantissa = mantissa;
        Exponent = exponent;
        
        // Initialize the surrogate fields as well
        _mantissaString = null; 
        _exponent = 0;

        Normalize();
    }
    
    // (Your existing Normalize, operators, and CompareTo methods go here...)
    private void Normalize()
    {
        if (Mantissa == 0)
        {
            Exponent = 0;
            return;
        }

        while (Mantissa >= 10.0)
        {
            Mantissa /= 10.0;
            Exponent++;
        }

        while (Mantissa < 1.0 && Mantissa != 0)
        {
            Mantissa *= 10.0;
            Exponent--;
        }
    }
    
    public static implicit operator BigNumber(int value) => new BigNumber(value, 0);
    public static explicit operator int(BigNumber value)
    {
        if (value.Exponent > 10) 
        {
            throw new OverflowException("BigNumber is too large to fit in an Int32.");
        }

        try
        {
            double fullNumber = value.Mantissa * Math.Pow(10, value.Exponent);
            return Convert.ToInt32(fullNumber);
        }
        catch (OverflowException)
        {
            throw new OverflowException("BigNumber value is outside the range of an Int32.");
        }
    }
    
    public static BigNumber operator +(BigNumber a, BigNumber b)
    {
        if (a.Exponent > b.Exponent)
        {
            double mantissaB = b.Mantissa * Math.Pow(10, b.Exponent - a.Exponent);
            return new BigNumber(a.Mantissa + mantissaB, a.Exponent);
        }
        else
        {
            double mantissaA = a.Mantissa * Math.Pow(10, a.Exponent - b.Exponent);
            return new BigNumber(mantissaA + b.Mantissa, b.Exponent);
        }
    }
    
    public static bool operator >(BigNumber a, BigNumber b) => a.CompareTo(b) > 0;
    public static bool operator <(BigNumber a, BigNumber b) => a.CompareTo(b) < 0;

    public int CompareTo(BigNumber other)
    {
        if (Exponent > other.Exponent) return 1;
        if (Exponent < other.Exponent) return -1;
        return Mantissa.CompareTo(other.Mantissa);
    }
    
    #endregion
    
    // --- SERIALIZATION CALLBACKS ---

    public void OnBeforeSerialize()
    {
        // Convert the double to a string. The "R" (round-trip) format
        // is crucial for ensuring no precision is lost.
        _mantissaString = Mantissa.ToString("R");
        _exponent = Exponent; // The int can be copied directly.
    }

    public void OnAfterDeserialize()
    {
        // Convert the string back to a double.
        if (!string.IsNullOrEmpty(_mantissaString))
        {
            double.TryParse(_mantissaString, out Mantissa);
        }
        
        Exponent = _exponent; // The int can be copied directly.
    }
}
}