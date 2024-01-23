using BAL.Services.Common;
using DTO.Models;
using DTO.Models.BirthdayWishes;
using DTO.Models.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.BirthdayWish
{
    public class BirthdayWishes:Hub {


        ICommonService _commonService;
        IHttpContextAccessor _httpContextAccessor;

        public BirthdayWishes(ICommonService commonService, IHttpContextAccessor httpContextAccessor)
        {
            _commonService = commonService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Send(BirthdayWishesDTO birthdayWish)
        {
            if(birthdayWish.birthday_comment_id > 0)
            {
                await _commonService.PostOrUpdateAsync("emp_update_birthday_comment", birthdayWish, true);
            }
            else
            {
                await _commonService.PostOrUpdateAsync("emp_post_birthday_comment", birthdayWish, false);
            }
            await BroadCastBirthdayBoyWishes(birthdayWish.employee_id);
        }

        public async Task Delete(BirthdayWishesDTO birthdayWish)
        {
            DataResponse res = await Task.Run(() => _commonService.DeleteById("emp_delete_birthday_comment", "prm_birthday_comment_id", birthdayWish.birthday_comment_id));
            await Clients.Caller.SendAsync("DeleteCompleted", res);
            await BroadCastBirthdayBoyWishes(birthdayWish.employee_id);
        }


        public override async Task OnConnectedAsync()
        {
            string birthday_boy_employee_id = _httpContextAccessor.HttpContext.Request.Query["access_token"];
            List<EmployeeBirthday_DTO> birthdayWishes = await _commonService.GetListByIdAsync<EmployeeBirthday_DTO>("emp_get_birthday_comment", "prm_employee_id", birthday_boy_employee_id);
            await Clients.Caller.SendAsync($"InitializeBirthdayWishes_{birthday_boy_employee_id}", birthdayWishes);
            await base.OnConnectedAsync();
        }


        public async Task BroadCastBirthdayBoyWishes(string birthday_boy_employee_id)
        {
            List<EmployeeBirthday_DTO> birthdayWishes = await _commonService.GetListByIdAsync<EmployeeBirthday_DTO>("emp_get_birthday_comment", "prm_employee_id", birthday_boy_employee_id);
            await Clients.All.SendAsync($"ReceiveBirthdayWishes_{birthday_boy_employee_id}", birthdayWishes);
        }

    }
}
