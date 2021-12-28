using Line.Messaging;
using Line.Messaging.Webhooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testline
{
    public class LineBotApp : WebhookApplication
    {
        private readonly LineMessagingClient _messagingClient;
        public LineBotApp(LineMessagingClient lineMessagingClient)
        {
            _messagingClient = lineMessagingClient;
        }

        //OnMessageAsync: 接收使用者訊息。
        //ReplyMessageAsync: 傳訊息給使用者。
        protected override async Task OnMessageAsync(MessageEvent ev)
        {
            var result = null as List<ISendMessage>;

            var textmessage = ev;
            switch (ev.Message)
            {
                //文字訊息
                case EventMessage textMessage:
                    {

                        //頻道Id
                        var channelId = ev.Source.Id;
                        //使用者Id
                        var userId = ev.Source.UserId;

                        if (ev.Equals(textmessage))
                        {
                            result = new List<ISendMessage>
                            {
                                new TextMessage("HELLLLLLLLLLLLLLLLLLLO")
                           
                        };
                            if (result != null)
                                break;
                        }
                        
                  

                        //回傳 hellow
                        result = new List<ISendMessage>
                    {
                        new TextMessage("hellow")
                    };
                    }
                    break;
             
            }

            if (result != null)
                await _messagingClient.ReplyMessageAsync(ev.ReplyToken, result);
        }
    }
}
