using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#elif UNITY_IOS
using Unity.Notifications.iOS;
#endif

using System;

public class NotificationManager : MonoBehaviour
{
    public void ExecuteNotification()
    {
#if UNITY_ANDROID
            CreateNotifChannel();
            SendNotification();
#elif UNITY_IOS
            StartCoroutine(RequestAuthorizationIOS());
            SendNotificationIOS();
#endif

    }

#if UNITY_ANDROID
    void CreateNotifChannel()
    {
        var c = new AndroidNotificationChannel()
        {
            Id = "notif1",
            Name = "reminder",
            Importance = Importance.High,
            Description = "This is a test notification created by the app"
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
       
    }

    void SendNotification()
    {
        var notification = new AndroidNotification();
        notification.Title = "Salesforce notification";
        notification.Text = "This is a test notification created by the app";
        notification.FireTime = System.DateTime.Now.AddSeconds(5);
        notification.LargeIcon = "icon_1";

        AndroidNotificationCenter.SendNotification(notification, "notif1");

    }
#endif

#if UNITY_IOS


    void SendNotificationIOS()
    {
        var timeTrigger = new iOSNotificationTimeIntervalTrigger()
        {
            TimeInterval = new TimeSpan(0, 0, 5),
            Repeats = false
        };

        var notification = new iOSNotification()
        {
            // You can specify a custom identifier which can be used to manage the notification later.
            // If you don't provide one, a unique string will be generated automatically.
            Identifier = "_notification_01",
            Title = "Salesforce notification",
            Body = "Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
            Subtitle = "This is a test notification created by the app",
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread1",
            Trigger = timeTrigger,
        };

        iOSNotificationCenter.ScheduleNotification(notification);
    }

    IEnumerator RequestAuthorizationIOS()
    {
        var authorizationOption = AuthorizationOption.Alert | AuthorizationOption.Badge;
        using (var req = new AuthorizationRequest(authorizationOption, true))
        {
            while (!req.IsFinished)
            {
                yield return null;
            };

            string res = "\n RequestAuthorization:";
            res += "\n finished: " + req.IsFinished;
            res += "\n granted :  " + req.Granted;
            res += "\n error:  " + req.Error;
            res += "\n deviceToken:  " + req.DeviceToken;
            Debug.Log(res);
        }
    }

#endif

}
