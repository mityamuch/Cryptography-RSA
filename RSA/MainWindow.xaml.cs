using Microsoft.Win32;
using MyRSA;
using System;
using System.Collections.Generic;
using System.IO;
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

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VinnerAtack atack=new VinnerAtack();
            atack.Attack(9449868410449,6792605526025);
        }

        private void RSAtest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RSAService service = new RSAService(SimplifyTestMode.SoloveyShtrasen, 0.99, 512);

                BigInteger content = new BigInteger(File.ReadAllBytes("../../../../in.txt"), true);
                File.WriteAllText("../../../../out1.txt", System.Convert.ToBase64String(service.Encrypt(content)));



                System.Diagnostics.Process txt = new System.Diagnostics.Process();
                txt.StartInfo.FileName = "notepad.exe";
                txt.StartInfo.Arguments = @"../../../../out1.txt";
                txt.Start();

                BigInteger content1 = new BigInteger(System.Convert.FromBase64String(File.ReadAllText("../../../../out1.txt")));
                File.WriteAllBytes("../../../../out2.txt", service.Decrypt(content1));

                System.Diagnostics.Process txt1 = new System.Diagnostics.Process();
                txt1.StartInfo.FileName = "notepad.exe";
                txt1.StartInfo.Arguments = @"../../../../out2.txt";
                txt1.Start();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message);
            }
        }
    }
}
