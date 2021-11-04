using SEMS_APP.Global;
using SEMS_APP.ViewModels;
using Syncfusion.XForms.Border;
using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SEMS_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticsPage : ContentPage
    {
        StatisticsViewModel viewModel; 
        public static int year, month, day;
        public StatisticsPage()
        {
            InitializeComponent();
            dlMonth.Text = DateTime.Now.ToString("y");
            dlYear.Text = DateTime.Now.Year.ToString();
            dlDay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            day = DateTime.Now.Day;
            viewModel = new StatisticsViewModel();
            this.BindingContext = viewModel;
        }

        private void SfTabView_SelectionChanged(object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
            var tabView = sender as SfTabView;
            SetSelectionIndicator(tabView, e.Index);
            //if (e.Index == 1)
            //    viewModel.LoadDataMonth("CPC");
            //else if (e.Index == 2)
            //    viewModel.LoadDataYear("CPC");
        }


        private void SetSelectionIndicator(SfTabView tabView, int selectedIndex)
        {
            for (int i = 0; i < tabView.Items.Count; i++)
            {
                var tabItem = tabView.Items[i] as SfTabItem;
                var borderGrid = tabItem.HeaderContent as Grid;
                var border = (borderGrid.Children[1] as SfBorder);
                var label = (borderGrid.Children[0] as Label);
                if (i == selectedIndex)
                {
                    border.IsVisible = true;
                    label.TextColor = Color.DeepSkyBlue;
                }
                else
                {
                    border.IsVisible = false;
                    label.TextColor = Color.Black;
                }
            }
        }

        private void PrevYearClicked(object sender, EventArgs e)
        {
            DateTime nowDate = new DateTime(year, month, day);
            var previewDate = nowDate.AddYears(-1);
            dlYear.Text = previewDate.Year.ToString();
            year = previewDate.Year;
            month = previewDate.Month;
            day = previewDate.Day;
            viewModel.LoadDataYear(Config.ma_dvql, Config.ma_khang);
        }

        private void NextYearClicked(object sender, EventArgs e)
        {
            DateTime nowDate = new DateTime(year, month, day);
            var nextDate = nowDate.AddYears(+1);
            dlYear.Text = nextDate.Year.ToString();
            year = nextDate.Year;
            month = nextDate.Month;
            day = nextDate.Day;
            viewModel.LoadDataYear(Config.ma_dvql, Config.ma_khang);
        }

        private void PrevDayClicked(object sender, EventArgs e)
        {
            DateTime nowDate = new DateTime(year, month, day);
            var previewDate = nowDate.AddDays(-1);
            dlDay.Text = previewDate.ToString("dd/MM/yyyy");
            year = previewDate.Year;
            month = previewDate.Month;
            day = previewDate.Day;
            viewModel.LoadDataDay(Config.ma_dvql, Config.ma_khang);
        }

        private void NextDayClicked(object sender, EventArgs e)
        {
            DateTime nowDate = new DateTime(year, month, day);
            var previewDate = nowDate.AddDays(+1);
            dlDay.Text = previewDate.ToString("dd/MM/yyyy");
            year = previewDate.Year;
            month = previewDate.Month;
            day = previewDate.Day;
            viewModel.LoadDataDay(Config.ma_dvql, Config.ma_khang);
        }

        private void PrevMonthClicked(object sender, EventArgs e)
        {
            DateTime nowDate = new DateTime(year, month, day);
            var previewDate = nowDate.AddMonths(-1);
            dlMonth.Text = previewDate.ToString("y");
            year = previewDate.Year;
            month = previewDate.Month;
            day = previewDate.Day;
            viewModel.LoadDataMonth(Config.ma_dvql, Config.ma_khang);
        }
        private void NextMonthClicked(object sender, EventArgs e)
        {
            DateTime nowDate = new DateTime(year, month, day);
            var nextDate = nowDate.AddMonths(+1);
            dlMonth.Text = nextDate.ToString("y");
            year = nextDate.Year;
            month = nextDate.Month;
            day = nextDate.Day;
            viewModel.LoadDataMonth(Config.ma_dvql, Config.ma_khang);
        }
    }
}