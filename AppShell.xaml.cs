namespace AppointmentSimulator
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Pages.AddNewAppointmentPage), typeof(Pages.AddNewAppointmentPage));
        }
    }
}
