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


            switch (ev.Message.Fix())
            {
                //文字訊息
                case TextEventMessage textMessage:
                    if (textMessage.Text.Contains("A"))
                    {
                        await _messagingClient.ReplyMessageAsync(ev.ReplyToken,
                            $"收到的是文字訊息，內容: {textMessage.Text}");
                    }
                    else {
                        await _messagingClient.ReplyMessageAsync(ev.ReplyToken,$"HI");
                    }


                    break;

            }
        }
        }
    }

