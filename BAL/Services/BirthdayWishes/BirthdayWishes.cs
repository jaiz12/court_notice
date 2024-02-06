using BAL.Services.Common;
using DTO.Models;
using DTO.Models.BirthdayWishesDTO;
using DTO.Models.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.BirthdayWish
{
    public class BirthdayWishService : Hub
    {
        private readonly ICommonService _commonService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BirthdayWishService(ICommonService commonService, IHttpContextAccessor httpContextAccessor)
        {
            _commonService = commonService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Send(BirthdayWishesDTO birthdayWish)
        {
            try
            {
                if (birthdayWish.birthday_comment_id > 0)
                {
                    await _commonService.PostOrUpdateAsync("emp_update_birthday_comment", birthdayWish, true);
                }
                else
                {
                    await _commonService.PostOrUpdateAsync("emp_post_birthday_comment", birthdayWish, false);
                }
                await BroadCastBirthdayBoyWishes(birthdayWish.employee_id);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in Send method: {ex.Message}");
                throw new Exception(ex.Message); // Rethrow the exception to signal the error
            }
        }

        public async Task Delete(BirthdayWishesDTO birthdayWish)
        {
            try
            {
                DataResponse res = await Task.Run(() => _commonService.DeleteById("emp_delete_birthday_comment", "prm_birthday_comment_id", birthdayWish.birthday_comment_id));
                await Clients.Caller.SendAsync("DeleteCompleted", res);
                await BroadCastBirthdayBoyWishes(birthdayWish.employee_id);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in Delete method: {ex.Message}");
                throw new Exception(ex.Message); // Rethrow the exception to signal the error
            }
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                string birthday_boy_employee_id = _httpContextAccessor.HttpContext.Request.Query["access_token"];
                List<EmployeeBirthday_DTO> birthdayWishes = await _commonService.GetListByIdAsync<EmployeeBirthday_DTO>("emp_get_birthday_comment", "prm_employee_id", birthday_boy_employee_id);
                await Clients.Caller.SendAsync($"InitializeBirthdayWishes_{birthday_boy_employee_id}", birthdayWishes);
                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in OnConnectedAsync method: {ex.Message}");
                throw new Exception(ex.Message); // Rethrow the exception to signal the error
            }
        }

        public async Task BroadCastBirthdayBoyWishes(string birthday_boy_employee_id)
        {
            try
            {
                List<EmployeeBirthday_DTO> birthdayWishes = await _commonService.GetListByIdAsync<EmployeeBirthday_DTO>("emp_get_birthday_comment", "prm_employee_id", birthday_boy_employee_id);
                await Clients.All.SendAsync($"ReceiveBirthdayWishes_{birthday_boy_employee_id}", birthdayWishes);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in BroadCastBirthdayBoyWishes method: {ex.Message}");
                throw new Exception(ex.Message); // Rethrow the exception to signal the error
            }
        }
    }

}
