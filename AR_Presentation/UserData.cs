using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UserData
{
    private static string companyName = "Hey";
    private static string userName = "Hey";
    private static string salesforceAgent = "Hey";
    private static string character = "";


    public static void SetCompanyName(string value)
    {
        companyName = value;
    }

    public static string GetCompanyName()
    {
        return companyName;
    }


    public static void SetUserName(string value)
    {
        userName = value;
    }

    public static string GetUserName()
    {
        return userName;
    }

    public static void SetSalesforceAgent(string value)
    {
        salesforceAgent = value;
    }

    public static string GetSalesforceAgent()
    {
        return salesforceAgent;
    }

    public static void SetCharacter(string value)
    {
        character = value;
    }

    public static string GetCharacter()
    {
        return character;
    }
}