using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScheduleXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ReactiveContentPage<ViewModel>
    {
        public MainPage(ViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }
    }
}
