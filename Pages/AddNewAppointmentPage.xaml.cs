using AppointmentSimulator.ViewModels;

namespace AppointmentSimulator.Pages;

public partial class AddNewAppointmentPage : ContentPage
{
  public AddNewAppointmentPage()
  {
    InitializeComponent();
    BindingContext = new AppointmentViewModel();
  }
}