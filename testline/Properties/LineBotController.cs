using Line.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace testline.Properties
{
    //新增 LineBotController 這是 api 的接口，使用者傳訊息給機器人後，Line 會透過這個網址將訊息傳給我們進行後續處理，程式部屬完成後需要到 line developers 將 Webhook URL 改為這個網址。

    [Route("api/linebot")]
        public class LineBotController : Controller
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly HttpContext _httpContext;
            private readonly LineBotConfig _lineBotConfig;
            private readonly ILogger _logger;

            public LineBotController(IServiceProvider serviceProvider,
                LineBotConfig lineBotConfig)
            {
                _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                _httpContext = _httpContextAccessor.HttpContext;
                _lineBotConfig = lineBotConfig;
            }

        //完整的路由網址就是 https://xxx/api/linebot/run
        [HttpPost("run")]
        public async Task<IActionResult> Post()
        {
            try
            {
                var events = await _httpContext.Request.GetWebhookEventsAsync(_lineBotConfig.channelSecret);
                var lineMessagingClient = new LineMessagingClient(_lineBotConfig.accessToken);
                var lineBotApp = new LineBotApp(lineMessagingClient);
                await lineBotApp.RunAsync(events);
            }
            catch (Exception ex)
            {
                //需要 Log 可自行加入
                _logger.LogError(JsonConvert.SerializeObject(ex));
            }
            return Ok();
        }
        }
    }

