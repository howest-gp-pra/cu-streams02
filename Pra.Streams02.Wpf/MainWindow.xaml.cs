﻿using System;
using System.Collections.Generic;
using System.IO;
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
using Pra.Streams02.Core;

namespace Pra.Streams02.Wpf
{

    public partial class MainWindow : Window
    {
        string AssetPath = @"../../../Assets/";

        public MainWindow()
        {
            InitializeComponent();
        }

        void ShowFeedback(string message, bool isSucces = false)
        {
            tbkFeedback.Visibility = Visibility.Visible;
            tbkFeedback.Text = message;
            if (isSucces)
                tbkFeedback.Background = new SolidColorBrush(Color.FromRgb(0, 200, 0));
            else
                tbkFeedback.Background = new SolidColorBrush(Color.FromRgb(200, 0, 0));
        }

        void GetFilesInAssets()
        {
            cmbFiles.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(AssetPath);
            foreach(FileInfo fi in di.GetFiles())
            {
                cmbFiles.Items.Add(fi.Name);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EncodingService encodingService = new EncodingService();
            cmbEncoding.ItemsSource = encodingService.AvailableCharSets;
            GetFilesInAssets();
            cmbEncoding.SelectedIndex = 0;
            cmbFiles.SelectedIndex = 0;
        }

        private void BtnReadFile_Click(object sender, RoutedEventArgs e)
        {
            txtContent.Text = "";
            tbkFeedback.Text = "";
            tbkFeedback.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            string fileName = (string)cmbFiles.SelectedItem;
            EncodingEntity encodingEntity = (EncodingEntity)cmbEncoding.SelectedItem;
            DirectoryInfo di = new DirectoryInfo(AssetPath);

            try
            {
                txtContent.Text = StreamReaderService.ReadFileToString(di.FullName, fileName, encodingEntity.CharacterSet);
                ShowFeedback($"Bestand {fileName} werd succesvol gelezen", true);
            }
            catch (Exception ex)
            {
                ShowFeedback(ex.Message);
            }
        }

        private void CmbFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtContent.Text = "";
            tbkFeedback.Text = "";
            tbkFeedback.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            txtNewFileName.Text = (string)cmbFiles.SelectedItem;
        }

        private void CmbEncoding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtContent.Text = "";
            tbkFeedback.Text = "";
            tbkFeedback.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void BtnWriteFile_Click(object sender, RoutedEventArgs e)
        {
            WriteFile(false);
        }
        private void BtnOverWriteFile_Click(object sender, RoutedEventArgs e)
        {
            WriteFile(true);
        }
        void WriteFile(bool overWriteExistingFile)
        {
            tbkFeedback.Text = "";
            tbkFeedback.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            EncodingEntity encodingEntity = (EncodingEntity)cmbEncoding.SelectedItem;
            string content = txtContent.Text.Trim();

            DirectoryInfo di = new DirectoryInfo(AssetPath);
            string fileName = txtNewFileName.Text.Trim();
            if (fileName.Length == 0)
            {
                ShowFeedback($"Bestandsnaam opgeven", false);
                return;
            }

            try
            {
                StreamWriterService.WriteStringToFile(content, di.FullName, fileName, encodingEntity.CharacterSet, overWriteExistingFile);
                GetFilesInAssets();
                cmbFiles.SelectedItem = fileName;
                BtnReadFile_Click(null, null);
                ShowFeedback($"Bestand {fileName} werd succesvol weggeschreven in {encodingEntity.CharacterSetName}", true);
            }
            catch (Exception ex)
            {
                ShowFeedback(ex.Message);
            }
        }
    }
}
