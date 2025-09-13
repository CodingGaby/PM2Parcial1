using AppointmentSimulator.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AppointmentSimulator.ViewModels
{
    public partial class AppointmentViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _subject = string.Empty;

        [ObservableProperty]
        private DateTime _appointmentDate = DateTime.Today;

        [ObservableProperty]
        private TimeSpan _startingTime;

        [ObservableProperty]
        private TimeSpan _endingTime;

        [ObservableProperty]
        private Appointment _selectedAppointment;


        [RelayCommand]
        public async Task AddNewAppointment()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Subject))
            {
                await Shell.Current.DisplayAlert("Error", "Name/Subject cant be empty.", "OK");
                return;
            }

            if (GlobalData.Appointments.FirstOrDefault(a => a.AppointmentDate.Equals(AppointmentDate)) != null && GlobalData.Appointments.FirstOrDefault(a => a.StartingTime.Equals(StartingTime)) != null)
            {
                await Shell.Current.DisplayAlert("Error", "An appointment exists in this schedule.", "OK");
                return;
            }

            if (StartingTime == EndingTime)
            {
                await Shell.Current.DisplayAlert("Error", "Starting time cannot be the same as ending.", "OK");
                return;
            }

            if (EndingTime <= StartingTime)
            {
                await Shell.Current.DisplayAlert("Error", "Ending time cannot finish before its starts", "OK");
                return;
            }

            Appointment appointment = new()
            {
                Name = Name.Trim(),
                Subject = Subject,
                AppointmentDate = AppointmentDate,
                StartingTime = StartingTime,
                EndingTime = EndingTime
            };

            GlobalData.Appointments.Add(appointment);
            await Shell.Current.DisplayAlert("Success", "Appointment added.", "OK");
            await Shell.Current.GoToAsync("..");
        }


        [RelayCommand]
        public async Task DeleteAppointment()
        {
            if (SelectedAppointment == null) return;

            bool confirm = await Shell.Current.DisplayAlert("Confirm Delete",
                "Delete this appointment?", "Yes", "No");

            if (confirm)
            {
                GlobalData.Appointments.Remove(SelectedAppointment);
                await Shell.Current.DisplayAlert("Success", "Appointment deleted.", "OK");
            }
        }
    }
}