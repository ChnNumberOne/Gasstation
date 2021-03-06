﻿using System;
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

namespace Gasstation.Pages
{
    /// <summary>
    /// Interaktionslogik für DisplayWelcome.xaml
    /// </summary>
    public partial class DisplayWelcome : Page
    {
        public DisplayWelcome(string specialText = null, string specialDetails = null, Uri imagePath = null)
        {
            InitializeComponent();
            if (specialText != null || specialDetails != null)
            {
                TextLabel.FontSize = 26;
                TextLabel.Content = specialText;
                DetailsBlock.Text = specialDetails;
                if (imagePath != null)
                {
                    DisplayIconImage.Source = new BitmapImage(imagePath);
                }
                //DisplayIconImage.Height = 150;
            }
        }
    }
}
