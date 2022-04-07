using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Path = System.IO.Path;


namespace SitekTestingApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        private static string[] Load(string filename)
        {
            List<string> strings = null;
            using (StreamReader sr = new StreamReader(filename))
            {
                strings = new List<string>();
                while (!sr.EndOfStream)
                {
                    strings.Add(sr.ReadLine());
                }
            }

            return strings.ToArray();
        }

        internal string[] fileRKK;
        internal string[] fileAppeals;


        private void openFileRKKButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialogRKK = new OpenFileDialog();
            if (openFileDialogRKK.ShowDialog() == true)
            {
                TextBlockRKK.Text = "Выбранный файл: " + Path.GetFileName(openFileDialogRKK.FileName);
                fileRKK = Load(openFileDialogRKK.FileName);
            }
        }

        private void openFileAppealsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialogAppeals = new OpenFileDialog();
            if (openFileDialogAppeals.ShowDialog() == true)
            {
                TextBlockAppeals.Text = "Выбранный файл: " + Path.GetFileName(openFileDialogAppeals.FileName);
                fileAppeals = Load(openFileDialogAppeals.FileName);
            }
        }

        private Dictionary<string, int> staffRKK = null;
        private Dictionary<string, int> staffAppeals = null;
        private Dictionary<string, int> staffGeneral = null;
        private List<Performer> performerList = null;


        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void HeaderAdding()
        {
            if (DataGrid != null)
            {
                DataGrid.Columns[0].Header = "Ответственный" + Environment.NewLine + "исполнитель";
                DataGrid.Columns[1].Header = "Количество" + Environment.NewLine + "неисполненных" + Environment.NewLine +
                                             "входящих документов";
                DataGrid.Columns[2].Header = "Количество" + Environment.NewLine + "неисполненных" + Environment.NewLine +
                                             "письменных " + Environment.NewLine + "обращений граждан";
                DataGrid.Columns[3].Header = "Общее количество " + Environment.NewLine + "документов и " +
                                             Environment.NewLine + "обращений";
            }
            else
                MessageBox.Show("Таблица не заполонена");
        }

        private void DataGridCellsFillng()
        {
            DataGrid.ItemsSource = performerList.Select(p => new
            {
                p.Name,
                p.CountRKK,
                p.CountAppeals,
                p.CountGeneral
            });
        }

        private void WriteInTable()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            staffRKK = new Dictionary<string, int>();
            staffAppeals = new Dictionary<string, int>();
            staffGeneral = new Dictionary<string, int>();
            performerList = new List<Performer>();
            //СЛОВАРЬ РКК
            if (fileRKK != null)
            {
                var queryOfRKK = from line in fileRKK
                                 let searchingPerson = line.Split('\t', ';')
                                 select new
                                 {
                                     ResponsiblePerson = (searchingPerson[0] == "Климов Сергей Александрович"
                                         ? searchingPerson[1].Replace("(Отв.)", "").Trim()
                                         : searchingPerson[0].Trim()),
                                 };

                foreach (var item in queryOfRKK)
                {
                    var value = item.ResponsiblePerson.Trim().Split(' ');
                    string fio;
                    if (value.Length == 3)
                    {
                        fio = value[0] + " " + value[1].Substring(0, 1) + "." + value[2].Substring(0, 1) + ".".Trim();
                    }
                    else
                    {
                        fio = item.ResponsiblePerson;
                    }

                    if (staffRKK.ContainsKey(fio))
                    {
                        staffRKK[fio]++;
                    }
                    else
                        staffRKK.Add(fio, 1);
                }
            }

            //СЛОВАРЬ ОБРАЩЕНИЙ
            if (fileAppeals != null)
            {
                var queryOfAppeals = from line in fileAppeals
                                     let searchingPerson = line.Split('\t', ';')

                                     select new
                                     {
                                         ResponsiblePerson = (searchingPerson[0] == "Климов Сергей Александрович"
                                             ? searchingPerson[1].Replace("(Отв.)", "").Trim()
                                             : searchingPerson[0]),
                                     };
                foreach (var item in queryOfAppeals)
                {
                    var value = item.ResponsiblePerson.Trim().Split(' ');
                    string fio;
                    if (value.Length == 3)
                    {
                        fio = value[0] + " " + value[1].Substring(0, 1) + "." + value[2].Substring(0, 1) + ".";
                    }
                    else
                    {
                        fio = item.ResponsiblePerson;
                    }

                    if (staffAppeals.ContainsKey(fio))
                    {
                        staffAppeals[fio]++;
                    }
                    else
                        staffAppeals.Add(fio, 1);
                }

            }

            foreach (var item in staffRKK)
            {
                performerList.Add(new Performer
                {
                    Name = item.Key,
                    CountRKK = item.Value,
                    CountAppeals =
                            staffAppeals.ContainsKey(item.Key) ? staffAppeals[item.Key] : 0,
                    CountGeneral = item.Value + (staffAppeals.ContainsKey(item.Key) ? staffAppeals[item.Key] : 0)
                }
                );
                staffAppeals.Remove(item.Key);
            }

            foreach (var item2 in staffAppeals)
            {
                performerList.Add(new Performer { Name = item2.Key, CountAppeals = item2.Value, CountGeneral = item2.Value });
            }

            DataGridCellsFillng();

            HeaderAdding();

            stopWatch.Stop();
            TextBlockTime.Text = $"{stopWatch.ElapsedMilliseconds} мс"; ;

        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            if (fileRKK != null && fileAppeals != null)
            {
                WriteInTable();
                TextBlockTodayDate.Text = $"Дата составления справки: {DateTime.Now.ToShortDateString()}";
                Total.Text = $"Не исполнено в срок {performerList.Sum(p => p.CountRKK + p.CountAppeals)} документов, из них:";
                TotalRKK.Text = $"- количество неисполненных входящих документов: {performerList.Sum(p => p.CountRKK)};";
                TotalAppeals.Text = $"- количество неисполненных письменных обращений граждан: {performerList.Sum(p => p.CountAppeals)}.";


            }
            else MessageBox.Show("Вы выбрали не все файлы!");
        }

        private void ButtonNameSort_Click(object sender, RoutedEventArgs e)
        {
            if (performerList != null)
            {
                performerList = performerList.Select(p => new Performer
                {
                    Name = p.Name,
                    CountRKK = p.CountRKK,
                    CountAppeals = p.CountAppeals,
                    CountGeneral = p.CountGeneral
                }).OrderBy((p => p.Name)).ToList();
                DataGridCellsFillng();
                HeaderAdding();
            }
            else
                MessageBox.Show("Данные не заполнены!");
        }

        private void ButtonRKKSort_Click(object sender, RoutedEventArgs e)
        {
            if (performerList != null)
            {
                performerList = performerList.Select(p => new Performer
                {
                    Name = p.Name,
                    CountRKK = p.CountRKK,
                    CountAppeals = p.CountAppeals,
                    CountGeneral = p.CountGeneral
                }).OrderByDescending((p => p.CountRKK)).ThenByDescending(p => p.CountAppeals).ToList();
                DataGridCellsFillng();
                HeaderAdding();
            }
            else
                MessageBox.Show("Данные не заполнены!");
        }

        private void ButtonAppealsSort_Click(object sender, RoutedEventArgs e)
        {
            if (performerList != null)
            {
                performerList = performerList.Select(p => new Performer
                {
                    Name = p.Name,
                    CountRKK = p.CountRKK,
                    CountAppeals = p.CountAppeals,
                    CountGeneral = p.CountGeneral
                }).OrderByDescending((p => p.CountAppeals)).ThenByDescending(p => p.CountRKK).ToList();
                DataGridCellsFillng();
                HeaderAdding();
            }
            else
                MessageBox.Show("Данные не заполнены!");
        }

        private void ButtonGeneralSort_Click(object sender, RoutedEventArgs e)
        {
            if (performerList != null)
            {
                performerList = performerList.Select(p => new Performer
                {
                    Name = p.Name,
                    CountRKK = p.CountRKK,
                    CountAppeals = p.CountAppeals,
                    CountGeneral = p.CountGeneral
                }).OrderByDescending((p => p.CountGeneral)).ThenByDescending(p => p.CountRKK).ToList();
                DataGridCellsFillng();
                HeaderAdding();
            }
            else
                MessageBox.Show("Данные не заполнены!");
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid == null)
            {
                MessageBox.Show("Нечего выводить!");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false))
                {
                    writer.WriteLine("Справка о неисполненных документах и обращениях граждан\n");
                    writer.WriteLine(Total.Text);
                    writer.WriteLine(TotalRKK.Text);
                    writer.WriteLine(TotalAppeals.Text);
                    writer.WriteLine();
                    writer.WriteLine("{0,4} |{1,20} |{2,11} |{3,16}|{4,13} ",
                        "№", "Исполнитель", "Кол-во ркк", "Кол-во обращений", "Общее кол-во");
                    int i = 1;
                    foreach (var item in performerList)
                    {
                        writer.WriteLine("------------------------------------------------------------------------");
                        writer.WriteLine("{0,4} |{1,20} |{2,11} |{3,15} |{4,13} ",
                            i++, item.Name, item.CountRKK, item.CountAppeals, item.CountGeneral);
                    }
                }
            }
        }

    }


    public class Performer
    {
        public string Name;
        public int CountRKK;
        public int CountAppeals;
        public int CountGeneral;

    }
}
