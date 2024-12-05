using System;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour, IManager
{
    public event Action<int> OnKeyPressed;

    private bool numpadOnePress;
    private bool numpadTwoPress;
    private bool numpadThreePress;
    private bool numpadFourPress;
    private bool numpadFivePress;
    private bool numpadSixPress;
    private bool numpadSevenPress;
    private bool numpadEightPress;
    private bool numpadNinePress;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) { KeyPadOnePress(); }
        else { numpadOnePress = false; }

        if (Input.GetKeyDown(KeyCode.Keypad2)) { KeyPadTwoPress(); }
        else { numpadTwoPress = false; }

        if (Input.GetKeyDown(KeyCode.Keypad3)) { KeyPadThreePress(); }
        else { numpadThreePress = false; }

        if (Input.GetKeyDown(KeyCode.Keypad4)) { KeyPadFourPress(); }
        else { numpadFourPress = false; }

        if (Input.GetKeyDown(KeyCode.Keypad5)) { KeyPadFivePress(); }
        else { numpadFivePress = false; }

        if (Input.GetKeyDown(KeyCode.Keypad6)) { KeyPadSixPress(); }
        else { numpadSixPress = false; }

        if (Input.GetKeyDown(KeyCode.Keypad7)) { KeyPadSevenPress(); }
        else { numpadSevenPress = false; }

        if (Input.GetKeyDown(KeyCode.Keypad8)) { KeyPadEightPress(); }
        else { numpadEightPress = false; }

        if (Input.GetKeyDown(KeyCode.Keypad9)) { KeyPadNinePress(); }
        else { numpadNinePress = false; }
    }

    private void KeyPadOnePress()
    {
        if (numpadOnePress) return;

        numpadOnePress = true;

        OnKeyPressed?.Invoke(1);
    }

    private void KeyPadTwoPress()
    {
        if (numpadTwoPress) return;

        numpadTwoPress = true;

        OnKeyPressed?.Invoke(2);
    }

    private void KeyPadThreePress()
    {
        if (numpadThreePress) return;

        numpadThreePress = true;

        OnKeyPressed?.Invoke(3);
    }

    private void KeyPadFourPress()
    {
        if (numpadFourPress) return;

        numpadFourPress = true;

        OnKeyPressed?.Invoke(4);
    }

    private void KeyPadFivePress()
    {
        if (numpadFivePress) return;

        numpadFivePress = true;

        OnKeyPressed?.Invoke(5);
    }

    private void KeyPadSixPress()
    {
        if (numpadSixPress) return;

        numpadSixPress = true;

        OnKeyPressed?.Invoke(6);
    }

    private void KeyPadSevenPress()
    {
        if (numpadSevenPress) return;

        numpadSevenPress = true;

        OnKeyPressed?.Invoke(7);
    }

    private void KeyPadEightPress()
    {
        if (numpadEightPress) return;

        numpadEightPress = true;

        OnKeyPressed?.Invoke(8);
    }

    private void KeyPadNinePress()
    {
        if (numpadNinePress) return;

        numpadNinePress = true;

        OnKeyPressed?.Invoke(9);
    }
}
