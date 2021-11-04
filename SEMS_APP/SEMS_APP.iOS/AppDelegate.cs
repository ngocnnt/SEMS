using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Plugin.FirebasePushNotification;
using UIKit;
using UserNotifications;

namespace SEMS_APP.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Rg.Plugins.Popup.Popup.Init();
            ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfButtonRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfSwitchRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfRadioButtonRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfCheckBoxRenderer.Init();
            Syncfusion.SfNumericUpDown.XForms.iOS.SfNumericUpDownRenderer.Init();
            Syncfusion.XForms.iOS.Border.SfBorderRenderer.Init();
            Syncfusion.XForms.iOS.TabView.SfTabViewRenderer.Init();
            Syncfusion.SfGauge.XForms.iOS.SfGaugeRenderer.Init();
            Syncfusion.SfDataGrid.XForms.iOS.SfDataGridRenderer.Init();
            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();
            Syncfusion.XForms.iOS.ProgressBar.SfLinearProgressBarRenderer.Init();
            Syncfusion.XForms.iOS.EffectsView.SfEffectsViewRenderer.Init();
            FirebasePushNotificationManager.Initialize(options, true);
            LoadApplication(new App());
            FirebasePushNotificationManager.Initialize(options, new NotificationUserCategory[]
            {
                new NotificationUserCategory("message",new List<NotificationUserAction> {
                    new NotificationUserAction("Reply","Reply",NotificationActionType.Foreground)
                }),
                new NotificationUserCategory("request",new List<NotificationUserAction> {
                    new NotificationUserAction("Accept","Accept"),
                    new NotificationUserAction("Reject","Reject",NotificationActionType.Destructive)
                })

            });
            // Request Permissions
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // Request Permissions
                UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound, (granted, error) =>
                {
                    // Do something if needed
                });
            }
            else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null);

                app.RegisterUserNotificationSettings(notificationSettings);
            }
            return base.FinishedLaunching(app, options);
        }
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);

        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);
        }
        // To receive notifications in foregroung on iOS 9 and below.
        // To receive notifications in background in any iOS version
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            // If you are receiving a notification message while your app is in the background,
            // this callback will not be fired 'till the user taps on the notification launching the application.

            // If you disable method swizzling, you'll need to call this method. 
            // This lets FCM track message delivery and analytics, which is performed
            // automatically with method swizzling enabled.
            FirebasePushNotificationManager.DidReceiveMessage(userInfo);
            // Do your magic to handle the notification data
            System.Console.WriteLine(userInfo);

            completionHandler(UIBackgroundFetchResult.NewData);
        }
    }
}
