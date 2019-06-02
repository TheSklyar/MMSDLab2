using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using Book;
using Helpers.Common;
using Helpers.DB;
using Helpers.Enums;
using People;
using PeopleBooks.Data;

namespace PeopleBooks
{
    /// <summary>
    /// Логика взаимодействия для OrderEdit.xaml
    /// </summary>
    public partial class OrderEdit : Window
    {
        private ConnectionSettings _connectionSettings;
        private OpenType type;
        public OrderEdit(ConnectionSettings connectionSettings, OpenType type, int id = 0)
        {
            _connectionSettings = connectionSettings;
            this.type = type;
            InitializeComponent();
            if (type == OpenType.View)
            {
                Save.Visibility = Visibility.Hidden;
                ID.IsReadOnly = true;
                BookChoose.IsEnabled = false;
                ReaderChoose.IsEnabled = false;
            }
            else
            {
                Save.Visibility = Visibility.Visible;
                ID.IsReadOnly = true;
                BookChoose.IsEnabled = true;
                ReaderChoose.IsEnabled = true;
            }
            if (type != OpenType.New)
            {
                FillData(id);
            }
        }
        private void Reader_click(object sender, RoutedEventArgs e)
        {
            var cds = new PeopleGrid(_connectionSettings, true);
            cds.Owner = this;
            cds.ShowDialog();
            if (TemporaryStorage.Holder.TryGetValue("ID", out string ID))
            {
                Reader.Text = ID.ToString();
            }
            TemporaryStorage.Holder.Remove("ID");
        }

        private void Book_click(object sender, RoutedEventArgs e)
        {
            var cds = new BookGrid(_connectionSettings, true);
            cds.Owner = this;
            cds.ShowDialog();
            if (TemporaryStorage.Holder.TryGetValue("ID", out string ID))
            {
                Book.Text = ID.ToString();
            }
            TemporaryStorage.Holder.Remove("ID");
        }
        private void FillData(int code)
        {
            var _connection = new SqlConnection(_connectionSettings.ConnectionString);
            using (var command = new SqlCommand(SqlCommands.GetAllDataByID, _connection))
            {
                if (!command.Parameters.Contains("@ID"))
                {
                    command.Parameters.AddWithValue("@ID", code);
                }
                _connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ID.Text = code.ToString();
                        Book.Text= reader.GetInt32(0).ToString();
                        Reader.Text = reader.GetInt32(1).ToString();
                    }
                    else
                    {
                        throw new Exception("Заказ не найден");
                    }
                }
                _connection.Close();
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Book.Text == "" || Reader.Text =="")
            {
                MessageBox.Show("Все поля должны быть заполнены!");
                return;
            }
            if (type == OpenType.Edit)
            {
                int count = 0;
                var _connection = new SqlConnection(_connectionSettings.ConnectionString);
                using (var command = new SqlCommand(SqlCommands.SaveItem, _connection))
                {
                    if (!command.Parameters.Contains("@ID"))
                    {
                        command.Parameters.AddWithValue("@ID", Convert.ToInt32(ID.Text));
                    }
                    if (!command.Parameters.Contains("@BookID"))
                    {
                        command.Parameters.AddWithValue("@BookID",Convert.ToInt32(Book.Text));
                    }
                    if (!command.Parameters.Contains("@PeopleID"))
                    {
                        command.Parameters.AddWithValue("@PeopleID", Convert.ToInt32(Reader.Text));
                    }
                    _connection.Open();
                    count = command.ExecuteNonQuery();
                    _connection.Close();

                }
                if (count == 0)
                {
                    MessageBox.Show("Произошла ошибка, сохранение не удалось!");
                }
                else
                {
                    MessageBox.Show("Успешно сохранено!");
                }
            }
            else
            {
                int count = 0;
                var _connection = new SqlConnection(_connectionSettings.ConnectionString);
                using (var command = new SqlCommand(SqlCommands.SaveNew, _connection))
                {
                    if (!command.Parameters.Contains("@BookID"))
                    {
                        command.Parameters.AddWithValue("@BookID", Convert.ToInt32(Book.Text));
                    }
                    if (!command.Parameters.Contains("@PeopleID"))
                    {
                        command.Parameters.AddWithValue("@PeopleID", Convert.ToInt32(Reader.Text));
                    }
                    _connection.Open();
                    try
                    {
                        count = Convert.ToInt32(command.ExecuteScalar());
                    }
                    finally
                    {
                        _connection.Close();
                    }

                }
                if (count == 0)
                {
                    MessageBox.Show("Произошла ошибка, сохранение не удалось!");
                }
                else
                {
                    MessageBox.Show("Успешно сохранено!");
                    type = OpenType.Edit;
                    ID.Text = count.ToString();
                }

            }
        }
    }
}
