﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Plugin.CurrentActivity;
using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEMS_APP.Droid
{
    //[Application]
#if DEBUG
    [Application(Debuggable = true, UsesCleartextTraffic = true)]
#else
    [Application(Debuggable = false, UsesCleartextTraffic = false)]
#endif
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            CrossCurrentActivity.Current.Init(this);
            FirebaseApp.InitializeApp(this);

            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                FirebasePushNotificationManager.DefaultNotificationChannelId = "DefaultChannel";

                //Change for your default notification channel name here
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }

//            //If debug you should reset the token each time.
//#if DEBUG
//            FirebasePushNotificationManager.Initialize(this, new NotificationUserCategory[]
//            {
//            new NotificationUserCategory("message",new List<NotificationUserAction> {
//                new NotificationUserAction("Reply","Reply",NotificationActionType.Foreground),
//                new NotificationUserAction("Forward","Forward",NotificationActionType.Foreground)

//            }),
//            new NotificationUserCategory("request",new List<NotificationUserAction> {
//                new NotificationUserAction("Accept","Accept",NotificationActionType.Default,"check"),
//                new NotificationUserAction("Reject","Reject",NotificationActionType.Default,"cancel")
//            })

//            }, true);
//#else
	            FirebasePushNotificationManager.Initialize(this,new NotificationUserCategory[]
		    {
			new NotificationUserCategory("message",new List<NotificationUserAction> {
			    new NotificationUserAction("Reply","Reply",NotificationActionType.Foreground),
			    new NotificationUserAction("Forward","Forward",NotificationActionType.Foreground)

			}),
			new NotificationUserCategory("request",new List<NotificationUserAction> {
			    new NotificationUserAction("Accept","Accept",NotificationActionType.Default,"check"),
			    new NotificationUserAction("Reject","Reject",NotificationActionType.Default,"cancel")
			})

		    },false);
//#endif

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("NOTIFICATION RECEIVED", p.Data);
            };
        }
    }
}