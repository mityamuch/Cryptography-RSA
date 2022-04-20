﻿using MyRSA;
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
            FermTest f=new FermTest();
            MillerRabinTest m = new MillerRabinTest();
            SoloveyShtrassenTest s = new SoloveyShtrassenTest();
            test.Text =Convert.ToString(s.CheckSimplicity(807979, 0.99));
        }
    }
}
