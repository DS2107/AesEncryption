using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AES_encryption
{
    public partial class Form1 : Form
    {
        // Выбор файла
        OpenFileDialog SelectFile = new OpenFileDialog();

        // указать папку для дешифровки файла 
        FolderBrowserDialog saveFileDesc = new FolderBrowserDialog();

        // мой класс в котором метод дешифровки и шифрования 
        MyEncrypt myEncrypt = new MyEncrypt();

        // полный путь выбранного файла 
        string fullname_selectFile;

        // полный путь выбранной папки
        string fullname_saveFile;
        
        public Form1()
        {
            InitializeComponent();
            // ставим для пароля *
            textBox_password.PasswordChar = '*';

           // Что бы брать только один файл
            SelectFile.Multiselect = false;
        }

        private void button_openFile_Click(object sender, EventArgs e)
        {
            // Проверяю нажаk ли 
            if (SelectFile.ShowDialog() == DialogResult.Cancel)
                return;
            else
            {
                // если нажал то выводим информацию о файле
                label_problem.Text = "";
                label_infoFile.Text = Path.GetFileName(SelectFile.FileName);
                label_infoFile.ForeColor = Color.Green;
                label_infoFile.Font = new Font(label_infoFile.Font, FontStyle.Bold);
                fullname_selectFile = SelectFile.FileName;
                
            }
            
        } // button_openFile_Click

        // Штфрование
        private void button_Encryption_Click(object sender, EventArgs e)
        {
            // Проверяю выбрал и я файл 
            if (!File.Exists(fullname_selectFile))
            {
                label_problem.ForeColor = Color.Red;
                label_problem.Text = "Такого файла не существует или вы его не выбрали";
            } // if 
            else
            {
                // Задал ли я пароль
                if (String.IsNullOrEmpty(textBox_password.Text))
                {
                    label_problem.ForeColor = Color.Red;
                    label_problem.Text = "Вы не указали пароль";

                } // if
                else
                {
                    // Проверка пароля регуляркой и длину пароля
                    if (textBox_password.Text.Length < 8)
                    {
                        label_problem.Text = "Пароль меньше 8 символов";
                        label_problem.ForeColor = Color.Red;
                    } // if
                    else
                    {
                        string pattern = @"^((?=.*[a-z])|(?=.*[а-я]))((?=.*[A-Z])|(?=.*[А-Я]))(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S";
                        bool myreg = Regex.IsMatch(textBox_password.Text, pattern);
                        if (myreg)
                        {
                            // Шифруем
                            myEncrypt.FileEncrypt(fullname_selectFile, textBox_password.Text);
                            label_problem.Text = "Файл " + label_infoFile.Text + " был зашифрован";
                            label_infoFile.Text = "";
                            label_problem.ForeColor = Color.DarkGreen;
                            textBox_password.Text = "";
                            fullname_selectFile = "";

                        } //if
                        else
                        {
                            label_problem.Text = "Пароль должен иметь - (Хотя бы 1 заглавную букву, цифру, и спец символ)";
                        }
                    }

                }
            }
           
           

        } // button_Encryption_Click

        // Дешифровка
        private void button_NonEncryption_Click(object sender, EventArgs e)
        {
            // Проверяю файл для дешифровки
            if (!File.Exists(fullname_selectFile))
                MessageBox.Show("Сначала укажите какой файл вы хотите Дешифровать");
            else
            {
                if (saveFileDesc.ShowDialog() == DialogResult.Cancel)
                    return;
                else
                {
                    // проверяю тот ли это файл
                    if(fullname_selectFile.Substring(fullname_selectFile.Length-4) == ".aes")
                    {
                        fullname_saveFile = saveFileDesc.SelectedPath;
                        fullname_saveFile = fullname_saveFile + @"\" + Path.GetFileName(fullname_selectFile);
                        fullname_saveFile = fullname_saveFile.Remove(fullname_saveFile.Length - 4);
                        // Дешифруем
                        myEncrypt.FileDecrypt(fullname_selectFile, fullname_saveFile, textBox_password.Text);
                        label_problem.ForeColor = Color.Green;
                        label_problem.Text = "Файл был дешифрован";
                    }
                    else
                    {
                        label_problem.ForeColor = Color.Red;
                        label_problem.Text = "Файл с таким расширением " + "'"+ Path.GetExtension(fullname_selectFile)+"'"
                            + " не может быть дешифрован";
                    }
                   
                }
                    
            }

          


        } // button_NonEncryption_Click
    }
}
