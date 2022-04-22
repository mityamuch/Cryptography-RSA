using MyRSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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


namespace RSA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var tests = new ISimplicityTest[]
            {
                new FermTest(),
                new MillerRabinTest(),
                new SoloveyShtrassenTest()
            };

            var sb = new StringBuilder();

            BigInteger valueToTest = BigInteger.Parse("9746347772161");
            const double probability = 0.99;

            sb.AppendLine($"{valueToTest} testing with probability = {probability:N2}:");

            foreach (var test in tests)
            {
                sb.AppendLine($"{test.Name}: {test.CheckSimplicity(valueToTest, probability)}");
            }

            test.Text = sb.ToString();

            //test.Text = Convert.ToString(s.CheckSimplicity(13, 0.99));
            //test.Text = Convert.ToString(LegandrYakobiService.GetYakobiSymbol(219,383));        
        }
    }
}
