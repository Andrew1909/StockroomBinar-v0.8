using StockroomBinar.BD;
using StockroomBinar.Class;
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

namespace StockroomBinar.Pages
{
    /// <summary>
    /// Логика взаимодействия для DeliveresInfoPage.xaml
    /// </summary>
    public partial class DeliveresInfoPage : Page
    {
        public DeliveriesProducts deliveriesProducts = new DeliveriesProducts();
        public Deliveries deliveries = new Deliveries();
        public PlasticProducts plasticProducts = new PlasticProducts();
        public DitalesProduction ditalesProduction = new DitalesProduction();
        
        public DeliveresInfoPage(Deliveries item)
        {
            InitializeComponent();
            CostomerText.Text = item.СustomerТame;
            DataText.Text = item.Date.ToString();
            ProcentText.Text = item.Status.ToString();
            var CountPosition = Connect.bd.DeliveriesProducts.Where(p => p.IDInside == item.ID).Count();
            int SumReadyDitales = 0;
            int SumNeseseryDitales = 0;
            CountPosition++;;
            for (int j=1; j<CountPosition; j++)
            {
                var objA = Connect.bd.DeliveriesProducts.Where(p => p.IDInside == item.ID && p.NumberPosition == j).Count();
                if (objA != 0)
                {
                    var objB = Connect.bd.DeliveriesProducts.First(p => p.IDInside == item.ID && p.NumberPosition == j);
                    var objC = Connect.bd.PlasticProducts.Where(p => p.ProductTypeID == objB.CodeDitals).Count();//проверяем, в какой таблице находится деатль(платик или с произдовдства)
                    var objD = Connect.bd.DitalesProduction.Where(p => p.CodeDitales == objB.CodeDitals).Count();
                    if (objC != 0)
                    {
                        //таблица с пластиковыми изделяим]
                        var objE = Connect.bd.PlasticProducts.First(p => p.ProductTypeID == objB.CodeDitals);
                        deliveriesProducts = objB;
                        plasticProducts = objE;
                        if (objE.EngravingStatus!=0)
                        {
                            //SumReadyDitales = SumReadyDitales + int.Parse(deliveriesProducts.ReadyDitals.ToString());
                            for (int n = 0; n < deliveriesProducts.NecessaryCountDitals; n++)
                            {
                                if (objE.EngravingStatus != 0)
                                { 
                                    if(deliveriesProducts.ReadyDitals == deliveriesProducts.NecessaryCountDitals)
                                    {
                                        SumReadyDitales = SumReadyDitales + int.Parse(deliveriesProducts.ReadyDitals.ToString());
                                        break;
                                    }
                                    else
                                    {
                                        deliveriesProducts.ReadyDitals = deliveriesProducts.ReadyDitals + 1;
                                        Connect.bd.SaveChanges();
                                        plasticProducts.EngravingStatus = plasticProducts.EngravingStatus - 1;
                                        plasticProducts.CountOnStoock = plasticProducts.CountOnStoock - 1;
                                        Connect.bd.SaveChanges();
                                        SumReadyDitales= SumReadyDitales+(int.Parse(deliveriesProducts.ReadyDitals.ToString())+1);
                                    }
                                    
                                    
                                    
                                }
                            }
                           
                        }
                        SumNeseseryDitales = SumNeseseryDitales + int.Parse(deliveriesProducts.NecessaryCountDitals.ToString());
                    }

                    if (objD != 0)
                    {
                        //таблица с изделиями с производства
                        var objE = Connect.bd.DitalesProduction.First(p => p.CodeDitales == objB.CodeDitals);
                        deliveriesProducts = objB;
                        ditalesProduction = objE;
                        if (objE.EngravingStatus != 0)
                        {
                            for (int n = 0; n < deliveriesProducts.NecessaryCountDitals; n++)
                            {
                                if (objE.EngravingStatus != 0)
                                {
                                    if (deliveriesProducts.ReadyDitals == deliveriesProducts.NecessaryCountDitals)
                                    {
                                        SumReadyDitales = SumReadyDitales + int.Parse(deliveriesProducts.ReadyDitals.ToString());
                                        break;
                                    }
                                    else
                                    {
                                        deliveriesProducts.ReadyDitals = deliveriesProducts.ReadyDitals + 1;
                                        Connect.bd.SaveChanges();
                                        ditalesProduction.EngravingStatus = ditalesProduction.EngravingStatus - 1;
                                        ditalesProduction.CountOnStoock = ditalesProduction.CountOnStoock - 1;
                                        Connect.bd.SaveChanges();
                                        SumReadyDitales = int.Parse(deliveriesProducts.ReadyDitals.ToString()) + 1;
                                    }
                                }
                            }
                        }
                        SumReadyDitales = SumReadyDitales + (int.Parse(deliveriesProducts.ReadyDitals.ToString()) + 1);

                    }
                }
            }

            deliveries = item;
            if (SumNeseseryDitales > 0) deliveries.Status = (SumReadyDitales * 100) / SumNeseseryDitales;
            else deliveries.Status = 0;
            Connect.bd.SaveChanges();
            ProcentText.Text = item.Status.ToString();

           DeliversInfoView.ItemsSource = Connect.bd.DeliveriesProducts.Where(p => p.IDInside == item.ID).ToList();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
