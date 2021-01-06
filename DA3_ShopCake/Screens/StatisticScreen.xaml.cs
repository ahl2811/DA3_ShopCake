using DA3_ShopCake.db;
using ConsoleApp2.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;


namespace DA3_ShopCake.Screens
{
    /// <summary>
    /// Interaction logic for StatisticScreen.xaml
    /// </summary>
    public partial class StatisticScreen : UserControl
    {
        public StatisticScreen()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CatalogueDaoImp ctDaolmp = new CatalogueDaoImp();

            string[] Thangs = { "11", "12", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10" };
            int[] a = new int[12];
            int[] b = new int[12];
            int[] c = new int[12];
            int[] d = new int[12];

            int j = 0;
            foreach (string i in Thangs)
            {
                List<TotalRevunue> ct = ctDaolmp.GetTotalRevunuesByMonth(i);
                a[j] = ct[0].Sale;
                b[j] = ct[1].Sale;
                c[j] = ct[2].Sale;
                d[j] = ct[3].Sale;
                j++;
            }

            SeriesCollection = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Title = "Bánh cupCake",
                    Values = new ChartValues<int> {a[0], a[1], a[2], a[3], a[4], a[5], a[6], a[7], a[8], a[9], a[10], a[11]},
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true
                },
                new StackedColumnSeries
                {
                    Title = "Bánh Mì",
                    Values = new ChartValues<int> {b[0], b[1], b[2], b[3], b[4], b[5], b[6], b[7], b[8], b[9], b[10], b[11]},
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true
                },
                new StackedColumnSeries
                {
                    Title = "Bánh Sandwith",
                    Values = new ChartValues<int> {c[0], c[1], c[2], c[3], c[4], c[5], c[6], c[7], c[8], c[9], c[10], c[11]},
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true
                },
                new StackedColumnSeries
                {
                    Title = "Bánh Sinh Nhật",
                    Values = new ChartValues<int> {d[0], d[1], d[2], d[3], d[4], d[5], d[6], d[7], d[8], d[9], d[10], d[11]},
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true
                }

            };

            Labels = Thangs;
            Formatter = value => value + " Đồng";

            DataContext = this;
            ChartDb.FontSize = 12;
            ChartDb.Foreground = new SolidColorBrush(Colors.DimGray);


            foreach (TotalRevunue sale in ctDaolmp.GetTotalRevunues())
            {
                PieSeries ps = new PieSeries() { Title = sale.CatalogueName, Values = new ChartValues<int> { sale.Sale }, DataLabels = true };
                PieChartDb.Series.Add(ps);
            }
            PieChartDb.FontSize = 12;
            PieChartDb.Foreground = new SolidColorBrush(Colors.DimGray);
        }

        private void PieChartDb_DataClick(object sender, ChartPoint chartPoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartPoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartPoint.SeriesView;
            selectedSeries.PushOut = 8;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}

