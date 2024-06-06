using BlazorWebassembly_Appointment.Shared.Dtos;

namespace BlazorWebassembly_Appointment.Client.Services
{
    public class UserStateService
    {
        public CurrentUserDto LoggedInUser { get; private set; }

        public event Action OnChange;

        public void SetLoggedInUser(CurrentUserDto userDto)
        {
            LoggedInUser = userDto;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() =>    OnChange?.Invoke();
    }
}
