using System;
using System.Collections.Generic;
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
using Helpers.Common;
using Helpers.Enums;
using People.Data;

namespace People
{
    /// <summary>
    /// Логика взаимодействия для PeopleEdit.xaml
    /// </summary>
    public partial class PeopleEdit : Window
    {
        //Не стал городить биндинги, так как слишком простое окно, да еще и копипаста с заказов ибо идентичный функционал
        private ConnectionSettings _connectionSettings;
        private OpenType type;
        public PeopleEdit(ConnectionSettings connectionSettings, OpenType type, int id = 0)
        {
            _connectionSettings = connectionSettings;
            this.type = type;
            InitializeComponent();
            if (type == OpenType.View)
            {
                Save.Visibility = Visibility.Hidden;
                ID.IsReadOnly = true;
                Name.IsReadOnly = true;
                Family.IsReadOnly = true;
            }
            else
            {
                Save.Visibility = Visibility.Visible;
                ID.IsReadOnly = true;
                Name.IsReadOnly = false;
                Family.IsReadOnly = false;
            }
            if (type != OpenType.New)
            {
                FillData(id);
            }
        }

        private void FillData(int code)
        {
            var _connection = new SqlConnection(_connectionSettings.ConnectionString);
            using (var command = new SqlCommand(SqlCommands.SelectByID, _connection))
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
                        Family.Text = reader.IsDBNull(0)? "" : reader.GetString(0);
                        Name.Text = reader.IsDBNull(1) ? "" : reader.GetString(1);
                    }
                    else
                    {
                        throw new Exception("Посетитель не найден");
                    }
                }
                _connection.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Family.Text == "")
            {
                MessageBox.Show("Фамилия должна быть заполнена!");
                return;
            }
            if (type == OpenType.Edit)
            {
                int count = 0;
                var _connection = new SqlConnection(_connectionSettings.ConnectionString);
                using (var command = new SqlCommand(SqlCommands.Update, _connection))
                {
                    if (!command.Parameters.Contains("@ID"))
                    {
                        command.Parameters.AddWithValue("@ID", Convert.ToInt32(ID.Text));
                    }
                    if (!command.Parameters.Contains("@Family"))
                    {
                        command.Parameters.AddWithValue("@Family", Family.Text.Trim());
                    }

                    if (string.IsNullOrWhiteSpace(Name.Text))
                    {
                        if (!command.Parameters.Contains("@Name"))
                        {
                            command.Parameters.AddWithValue("@Name", DBNull.Value);
                        }
                    }
                    else
                    {
                        if (!command.Parameters.Contains("@Name"))
                        {
                            command.Parameters.AddWithValue("@Name", Name.Text.Trim());
                        }
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
                    if (!command.Parameters.Contains("@Family"))
                    {
                        command.Parameters.AddWithValue("@Family", Family.Text.Trim());
                    }

                    if (string.IsNullOrWhiteSpace(Name.Text))
                    {
                        if (!command.Parameters.Contains("@Name"))
                        {
                            command.Parameters.AddWithValue("@Name", DBNull.Value);
                        }
                    }
                    else
                    {
                        if (!command.Parameters.Contains("@Name"))
                        {
                            command.Parameters.AddWithValue("@Name", Name.Text.Trim());
                        }
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