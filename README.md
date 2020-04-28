# How to bind appointments in Xamarin.Forms Schedule (SfSchedule) using Reactive MVVM

The [SfSchedule](https://help.syncfusion.com/xamarin/scheduler/overview?) allows you to bind schedule appointments to the reactive UI ViewModel which is a composable and cross-platform model-view-viewmodel framework for all .NET platforms.

You can also refer the following article.

https://www.syncfusion.com/kb/11458/how-to-bind-appointments-in-xamarin-forms-schedule-sfschedule-using-reactive-mvvm

To achieve this, follow the below steps:

**Step 1:** Install the [ReactiveUI](https://www.nuget.org/packages/ReactiveUI/) and [ReactiveUI.XamForms](https://www.nuget.org/packages/ReactiveUI.XamForms/) in your project.

**Step 2:** Create ViewModel which should implement [ReactiveObject](https://reactiveui.net/docs/handbook/view-models/).
``` c#
public class ViewModel : ReactiveObject
    {
        /// <summary>
        /// collecions for meetings.
        /// </summary>
        private ObservableCollection<Meeting> meetings;
 
        /// <summary>
        /// color collection.
        /// </summary>
        private List<Color> colorCollection;
 
        /// <summary>
        /// current day meeting.
        /// </summary>
        private List<string> currentDayMeetings;
 
        public ViewModel()
        {
            this.Meetings = new ObservableCollection<Meeting>();
            this.AddAppointmentDetails();
            this.AddAppointments();
        }
 
        /// <summary>
        /// Gets or sets meetings.
        /// </summary>
        public ObservableCollection<Meeting> Meetings
        {
            get
            {
                return this.meetings;
            }
 
            set
            {
                this.RaiseAndSetIfChanged(ref meetings,value);
            }
        }
 
        /// <summary>
        /// adding appointment details.
        /// </summary>
        private void AddAppointmentDetails()
        {
            this.currentDayMeetings = new List<string>();
            this.currentDayMeetings.Add("General Meeting");
            this.currentDayMeetings.Add("Plan Execution");
            this.currentDayMeetings.Add("Project Plan");
            this.currentDayMeetings.Add("Consulting");
            this.currentDayMeetings.Add("Support");
            this.currentDayMeetings.Add("Development Meeting");
            this.currentDayMeetings.Add("Scrum");
            this.currentDayMeetings.Add("Project Completion");
            this.currentDayMeetings.Add("Release updates");
            this.currentDayMeetings.Add("Performance Check");
 
            this.colorCollection = new List<Color>();
            this.colorCollection.Add(Color.FromHex("#FFA2C139"));
            this.colorCollection.Add(Color.FromHex("#FFD80073"));
            this.colorCollection.Add(Color.FromHex("#FF1BA1E2"));
            this.colorCollection.Add(Color.FromHex("#FFE671B8"));
            this.colorCollection.Add(Color.FromHex("#FFF09609"));
            this.colorCollection.Add(Color.FromHex("#FF339933"));
            this.colorCollection.Add(Color.FromHex("#FF00ABA9"));
            this.colorCollection.Add(Color.FromHex("#FFE671B8"));
            this.colorCollection.Add(Color.FromHex("#FF1BA1E2"));
            this.colorCollection.Add(Color.FromHex("#FFD80073"));
            this.colorCollection.Add(Color.FromHex("#FFA2C139"));
            this.colorCollection.Add(Color.FromHex("#FFA2C139"));
            this.colorCollection.Add(Color.FromHex("#FFD80073"));
            this.colorCollection.Add(Color.FromHex("#FF339933"));
            this.colorCollection.Add(Color.FromHex("#FFE671B8"));
            this.colorCollection.Add(Color.FromHex("#FF00ABA9"));
        }
 
        /// <summary>
        /// Adds the appointments.
        /// </summary>
        private void AddAppointments()
        {
            var today = DateTime.Now.Date;
            var random = new Random();
            for (int month = -1; month < 2; month++)
            {
                for (int day = -5; day < 5; day++)
                {
                    for (int count = 0; count < 2; count++)
                    {
                        var meeting = new Meeting();
                        meeting.From = today.AddMonths(month).AddDays(random.Next(1, 28)).AddHours(random.Next(9, 18));
                        meeting.To = meeting.From.AddHours(1);
                        meeting.EventName = this.currentDayMeetings[random.Next(7)];
                        meeting.Color = this.colorCollection[random.Next(14)];
                        this.Meetings.Add(meeting);
                    }
                }
            }
        }
    }
```
**Step 3:** ContentPage should inherit from ReactiveContentPage<ViewModel> and we are going to use **ReactiveUI** Binding to bind our ViewModel to our View.

**XMAL**
``` xml
public partial class MainPage : ReactiveContentPage<ViewModel>
{
        public MainPage(ViewModel viewModel)
        {
            
        }
}
```
**C#**
``` xml
<?xml version="1.0" encoding="utf-8" ?>
<rxui:ReactiveContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:ScheduleXamarin"
             xmlns:rxui="clr-namespace:ReactiveUI.XamForms;assembly=ReactiveUI.XamForms"
             xmlns:schedule="clr-namespace:Syncfusion.SfSchedule.XForms;assembly=Syncfusion.SfSchedule.XForms"
             x:Class="ScheduleXamarin.MainPage"
             x:TypeArguments="local:ViewModel">
 
    <ContentPage.Content>
       <schedule:SfSchedule x:Name="Schedule"
                                 DataSource="{Binding Meetings}"
                                 ScheduleView="MonthView"
                                 ShowAppointmentsInline="True"
                                 Margin="0">
                <schedule:SfSchedule.AppointmentMapping>
                <schedule:ScheduleAppointmentMapping
                        ColorMapping="Color"
                        EndTimeMapping="To"
                        StartTimeMapping="From"
                        SubjectMapping="EventName"
                        />
            </schedule:SfSchedule.AppointmentMapping>
           
        </schedule:SfSchedule>
 </ContentPage.Content>
</rxui:ReactiveContentPage>
```
**Step 4:** View can be connected in one-way dependent manner to the ViewModel through [bindings](https://reactiveui.net/docs/handbook/data-binding/). You can set the **BindingContext** for the **SfSchedule** in MainPage.cs itself in code behind like below.
``` c#
public partial class MainPage : ReactiveContentPage<ViewModel>
{
        public MainPage(ViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }
}
```
