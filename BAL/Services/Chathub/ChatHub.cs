using BAL.Services.Common;
using Common.DbContext;
using DTO.Models.Chathub;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Chathub
{

public class ChatHub : Hub
{

    ICommonService _commonService;
    public ChatHub(ICommonService commonService)
    {
        _commonService = commonService;
    }
    
    public async Task Send(string name, string message)
    {
            var chatMessage = new ChatHubDTO
            {
                User = name,
                Message = message
            };


            await _commonService.PostOrUpdateAsync("chathub_post", chatMessage, false);

            // Broadcast the message
            await Clients.All.SendAsync(name, message);
            await OnConnectedAsync();

        }



        public override async Task OnConnectedAsync()
        {

            List<ChatHubDTO> pastMessages = await _commonService.GetListByIdAsync<ChatHubDTO>("chathub_get", null, null);

            // Retrieve past messages using Dapper

            await Clients.Caller.SendAsync("initializeChat", pastMessages);

            await base.OnConnectedAsync();
        }

    }
}
