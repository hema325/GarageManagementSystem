using GMS.Client.Models;
using GMS.Client.Services.Users;
using GMS.Shared.Dtos.Responses.Users;
using Microsoft.AspNetCore.Components;

namespace GMS.Client.Pages.Users
{
    public partial class Users
    {
        [Inject]
        public IUsersService UsersService { get; set; }

        private List<UserDto> UsersList = new();
        private bool IsFetching = true;

        private UserFilterModel UserFilterModel = new();
        private IEnumerable<UserDto>? FilteredUsers;

        protected override async Task OnInitializedAsync()
        {
            var response = await UsersService.GetAllAsync();

            if (response.Succeeded)
                UsersList = response.Data!;

            FilteredUsers = UsersList;

            IsFetching = false;
        }

        private async Task DeleteAsync(int id)
        {
            var response = await UsersService.DeleteAsync(id);

            if (response.Succeeded)
                UsersList.RemoveAll(u => u.Id == id);
        }

        private void Filter()
        {
            FilteredUsers = UsersList.AsEnumerable();

            if (!string.IsNullOrEmpty(UserFilterModel.Name))
                FilteredUsers = FilteredUsers.Where(u => u.Name.StartsWith(UserFilterModel.Name));

            if (!string.IsNullOrEmpty(UserFilterModel.Email))
                FilteredUsers = FilteredUsers.Where(u => u.Email.StartsWith(UserFilterModel.Email));

            if (!string.IsNullOrEmpty(UserFilterModel.Role))
                FilteredUsers = FilteredUsers.Where(u => u.Role == UserFilterModel.Role);
        }

        private void Reset()
        { 
            FilteredUsers = UsersList;
            UserFilterModel = new();
        }
    }
}
