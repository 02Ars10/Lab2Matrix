using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Win32;
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

namespace MatrixOperations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public int[] Count { get; private set; }

        public List<string> Property 
        {
                get
                {
                    return new List<string>() { "+", "*" };
                }
            }
        

        private const int Min = 2;
        private const int Max = 5;
        public MainWindow()
        {
            //InitializeComponent();
            Count = Enumerable.Range(Min, Max - Min + 1).ToArray();
            DataContext = this;

        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {

            SelectedValuesForSizeOfMatrices();
        }
        private T Generate<T>(Func<int, int, T> func)
        {
            Random random = new Random();
            int i = 0, j = 0;
            return  (dynamic)func(i, j) + random.Next(-15, 15);
            
        }
        private void SelectedValuesForSizeOfMatrices()
        {
            wrpPanel.Children.Clear();
            wrpPanel2.Children.Clear();
            int leftMatrixRow = Convert.ToInt32(cmbLeftMatrixRow.SelectedItem);
            int leftMatrixCol = Convert.ToInt32(cmbLeftMatrixCol.SelectedItem);
            int rightMatrixRow = Convert.ToInt32(cmbRightMatrixRow.SelectedItem);
            int rightMatrixCol = Convert.ToInt32(cmbRightMatrixCol.SelectedItem);
            Random random = new Random();
            for (int i = 0; i < leftMatrixRow; i++)
            {
                for (int j = 0; j < leftMatrixCol; j++)
                {
                    TextBox x = new TextBox();
                    x.Name = "new_textbox";
                    x.Height = 350 / leftMatrixRow;
                    x.Width = 350 / leftMatrixCol;
                    x.FontSize = 20;
                    x.Text = random.Next(-25, 25).ToString();
                    x.VerticalAlignment = VerticalAlignment.Center;
                    x.TextAlignment = TextAlignment.Center;
                    wrpPanel.Children.Add(x);
                }
            }
            for (int i = 0; i < rightMatrixRow; i++)
            {
                for (int j = 0; j < rightMatrixCol; j++)
                {
                    TextBox x = new TextBox();
                    x.Name = "new_textbox";
                    x.Height = 350 / rightMatrixRow;
                    x.Width = 350 / rightMatrixCol;
                    x.FontSize = 20;
                    x.Text = random.Next(-25, 25).ToString();
                    x.TextAlignment = TextAlignment.Center;
                    wrpPanel2.Children.Add(x);
                }
            }
        }

        private void btnApply2_Click(object sender, RoutedEventArgs e)
        {
            DoOperation();
        }
        private GenericMatrix<int> result;
        private void DoOperation()
        {
            wrpPanel3.Children.Clear();
            int leftMatrixRow = Convert.ToInt32(cmbLeftMatrixRow.SelectedItem);
            int leftMatrixCol = Convert.ToInt32(cmbLeftMatrixCol.SelectedItem);
            int rightMatrixRow = Convert.ToInt32(cmbRightMatrixRow.SelectedItem);
            int rightMatrixCol = Convert.ToInt32(cmbRightMatrixCol.SelectedItem);

            GenericMatrix<int> genericMatrix1 = new GenericMatrix<int>(leftMatrixRow, leftMatrixCol);
            GenericMatrix<int> genericMatrix2 = new GenericMatrix<int>(rightMatrixRow, rightMatrixCol);

            int r = 0, c = 0;
            for (int i = 0; i < wrpPanel.Children.Count; i++)
            {
                var s = int.Parse((wrpPanel.Children[i] as TextBox).Text);
                genericMatrix1[r, c] = s;
                c++;
                if (c == leftMatrixCol)
                {
                    c = 0;
                    r++;
                }
            }
            r = 0; c = 0;
            for (int i = 0; i < wrpPanel2.Children.Count; i++)
            {
                var s = int.Parse((wrpPanel2.Children[i] as TextBox).Text);
                genericMatrix2[r, c] = s;
                c++;
                if (c == rightMatrixCol)
                {
                    c = 0;
                    r++;
                }
            }
            var rs = (myCmb.SelectedItem.ToString() == "+") ? "+" : "*";
            if (rs == "+")
            {
                result = new GenericMatrix<int>(leftMatrixRow, rightMatrixCol);
                try
                {
                    result = genericMatrix1 + genericMatrix2;
                }
                catch (ArgumentException ex)
                {

                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                result = new GenericMatrix<int>(leftMatrixCol, rightMatrixRow);
                try
                {
                    result = genericMatrix1 * genericMatrix2;
                }
                catch (ArgumentException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            for (int i = 0; i < result.Row; i++)
            {
                for (int j = 0; j < result.Column; j++)
                {
                    TextBox x = new TextBox();
                    x.Name = "new_textbox";
                    x.Text = result[i, j].ToString();
                    x.Height = 340 / result.Row;
                    x.Width = 340 / result.Column;
                    x.FontSize = 20;
                    x.TextAlignment = TextAlignment.Center;
                    wrpPanel3.Children.Add(x);
                }
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            SaveToFile();

        }

        public void SaveToFile()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Comma-Separated Values|*.csv";
            saveFileDialog1.Title = "Сохраняем csv файл";
            saveFileDialog1.ShowDialog();


            if (saveFileDialog1.FileName != "")
            {
                using (var sw = new StreamWriter(saveFileDialog1.FileName))
                {
                    for (int i = 0; i < result.Row; i++)
                    {
                        bool sep = false;
                        for (int j = 0; j < result.Column; j++)
                        {
                            if (sep)
                            {
                                sw.Write(";");
                            }
                            sep = true;
                            sw.Write($"{result[i, j]}");
                        }
                        sw.WriteLine();
                    }
                }
            }
        }

    }
   
}
