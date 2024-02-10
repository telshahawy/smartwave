using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SW.HomeVisits.Application.Abstract;
using SW.HomeVisits.Application.Notification;

namespace SW.HomeVisits.Infrastructure.Notifications
{
    public class PushNotificationManager : IPushNotificationManager
    {
        private readonly IHomeVisitsConfigurationProvider _homeVisitsConfiguration;

        public PushNotificationManager(IHomeVisitsConfigurationProvider homeVisitsConfiguration)
        {
            _homeVisitsConfiguration = homeVisitsConfiguration;
        }
        public async Task SendPushNotification(string id, string title, object message, string deviceToken, string clickAction)
        {
            string serverKey = _homeVisitsConfiguration.FireBaseServerKey;
            string senderId = _homeVisitsConfiguration.FireBaseSenderId;
            //string deviceToken = _homeVisitsConfiguration.DeviceToken;
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            var objNotification = new
            {
                to = deviceToken,//notification.DeviceToken,
                data = new
                {
                    title = title,//notification.NotificationTitle,
                    body = message,//notification.NotificationBody
                    id = id,
                    click_action = clickAction
                }
            };
            string jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(objNotification);

            Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);
            tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
            tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            tRequest.ContentLength = byteArray.Length;
            tRequest.ContentType = "application/json";
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);

                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String responseFromFirebaseServer = tReader.ReadToEnd();

                            var response = Newtonsoft.Json.JsonConvert.DeserializeObject(responseFromFirebaseServer);
                            //if (response.success == 1)if
                            //{
                            //    //new NotificationBLL().InsertNotificationLog(dayNumber, notication, true)
                            //    //return Ok(responseFromFirebaseServer);
                            //}
                            //else if (response.failure == 1)
                            //{
                            //    //new NotificationBLL().InsertNotificationLog(dayNumber, notification, false);
                            //    //sbLogger.AppendLine(string.Format("Error sent from FCM server, after sending request : {0} , for following device info: {1}", responseFromFirebaseServer, jsonNotificationFormat));
                            //    //return Ok(responseFromFirebaseServer);
                            //}

                        }
                    }

                }
            }

        }
    }
}
