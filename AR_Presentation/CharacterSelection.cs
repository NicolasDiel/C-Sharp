using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private Sprite[] characters;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;



    private int pos = 0;


    public void Previous()
    {       
        if (pos == 0)
        {
            ChangeImage(characters.Length-1);
            return;
        }

        ChangeImage(pos - 1);
    }

    public void Next()
    {
        if (pos == characters.Length-1)
        {
            ChangeImage(0);
            return;
        }

        ChangeImage(pos + 1);
    }

    public void ChangeImage(int position)
    {
        image.sprite = characters[position];
        pos = position;

        ChangeText(position);
        SetCharacter(position);
    }

    private void ChangeText(int value)
    {
        if (value == 0)
            text.text = "My name is Einstein!" + "\n" + "I’ll be you guide today";
        else if (value == 1)
            text.text = "My name is Appy!" + "\n" + "I’ll be you guide today";
        else if (value == 2)
            text.text = "My name is Astro!" + "\n" + "I’ll be you guide today";
        else if (value == 3)
            text.text = "My name is Codey!" + "\n" + "I’ll be you guide today";
    }

    private void SetCharacter(int value)
    {
        if (value == 0)
            UserData.SetCharacter("Einstein");
        else if (value == 1)
            UserData.SetCharacter("Appy");
        else if (value == 2)
            UserData.SetCharacter("Astro");
        else if (value == 3)
            UserData.SetCharacter("Codey");
    }

}
