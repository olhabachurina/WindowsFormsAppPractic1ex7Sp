using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppPractic1Sp
{
    public partial class Form1 : Form
    {
        static int[] numbers = new int[10000];
        static double average;
        static int min;
        static int max;
        static string filePath = "results.txt"; 
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(10000);
            }
            new Thread(FindMax).Start();
            new Thread(FindMin).Start();
            new Thread(FindAverage).Start();
            Thread fileOutputThread = new Thread(() =>
            {
                Thread.Sleep(1000);

                WriteResultsToFile();
                this.Invoke((MethodInvoker)delegate
                {
                    textBox1.Text = File.ReadAllText(filePath);
                });
            });
            fileOutputThread.Start();
        }
        static void FindMax()
        {
            max = numbers.Max();
        }
        static void FindMin()
        {
            min = numbers.Min();
        }
        static void FindAverage()
        {
            average = numbers.Average();
        }
        static void WriteResultsToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Максимальное значение: {max}");
                writer.WriteLine($"Минимальное значение: {min}");
                writer.WriteLine($"Среднее значение: {average}");
            }
        }
    }
}
    

