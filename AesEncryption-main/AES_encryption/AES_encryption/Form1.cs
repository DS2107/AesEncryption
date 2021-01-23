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
using System.Threading;
using System.Security.Cryptography;

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

        // Список выделенных файлов
        List<string> MyFiles;

        // Список файлов c расшиерением aes
        List<string> FilesAES;


        // полный путь выбранной папки
        string fullname_saveFile;


        public static Form1 MyForm;
        public Form1()
        {
            InitializeComponent();
            // ставим для пароля *
            textBox_password.PasswordChar = '*';

            // Что бы брать только один файл
            SelectFile.Multiselect = true;

            // Фильтр файлов
            SelectFile.Filter = "aes files (*.aes)|*.aes|All files (*.*)|*.*";

            // Фильтр файлов
            open_key.Filter = "ka files (*.ka)|*.ka|All files (*.*)|*.*";



        }
        public void StartEncryptThread()
        {
            CheckEncryption(MyFiles);
        }
        public void StartDecryptThread()
        {


            int count = 0;
            foreach (var file in MyFiles)
            {
                if (Path.GetExtension(file) == ".aes")
                {
                    count++;
                }
            }
            MessageBox.Show("В файлах которые вы указали, были найдено: " + count + " .aes файлов");
            if (count == 0)
            {

                textBox_password.Invoke((MethodInvoker)(() => textBox_password.Text = ""));
            }
            else
            {

                CheckDescryption(MyFiles);
                count = 0;
            }

        }
        private void button_openFile_Click(object sender, EventArgs e)
        {

            // Проверяю нажаk ли 
            if (SelectFile.ShowDialog() == DialogResult.Cancel)
                return;

            else
            {
                listBox_SelectFiles.Items.Clear();
                foreach (var file in SelectFile.FileNames.ToList())
                {
                    listBox_SelectFiles.Items.Add(Path.GetFileName(file));
                    MyFiles = SelectFile.FileNames.ToList();
                }


            }

        } // button_openFile_Click

        // Штфрование
        private void button_Encryption_Click(object sender, EventArgs e)
        {
            if (checkBoxFileKEY.Checked)
            {
                if (MyFiles != null)
                {

                    Thread thread1 = new Thread(FileKeyEncrypt);
                    thread1.Start();

                } // if
                else
                {
                  
                    label_problem.ForeColor = Color.Red;
                    label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Выберите файл для шифрования"));
                }

            } // if
            else
            {
                Thread thread = new Thread(StartEncryptThread);
                thread.Start();
            }

           
        } // button_Encryption_Click

        // Дешифровка
        private void button_NonEncryption_Click(object sender, EventArgs e)
        {
            if (saveFileDesc.ShowDialog() == DialogResult.Cancel)
                return;

            if (checkBoxFileKEY.Checked)
            {

                Thread thread1 = new Thread(FileKeyDecrypt);
                thread1.Start();
            }
            else
            {
                Thread thread = new Thread(StartDecryptThread);
                thread.Start();
            }
           


        } // button_NonEncryption_Click


        private void CheckDescryption(List<string> MyFiles)
        {
            // Проверяю выбрал и я файл 
            if (MyFiles == null)
            {
                label_problem.ForeColor = Color.Red;
                label_problem.Text = "Такого файла не существует или вы его не выбрали";
            } // if 
            else
            {
                FilesAES = new List<string>();
                foreach (var file in MyFiles)
                {

                    if (Path.GetExtension(file) == ".aes")
                    {
                        FilesAES.Add(file);
                        // MyFiles.Remove(file);
                    }

                }
                // Задал ли я пароль
                if (String.IsNullOrEmpty(textBox_password.Text))
                {
                    label_problem.ForeColor = Color.Red;
                    label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Вы не указали пароль"));

                } // if     
                else
                {
                    int i = 0;
                    progressBar_encryption.Invoke((MethodInvoker)(() => progressBar_encryption.Maximum = FilesAES.Count));
                    progressBar_encryption.Minimum = 0;
                    bool flag = true;
                    button_Encryption.Invoke((MethodInvoker)(() => button_Encryption.Enabled = false));
                    button_NonEncryption.Invoke((MethodInvoker)(() => button_NonEncryption.Enabled = false));
                    button_openFile.Invoke((MethodInvoker)(() => button_openFile.Enabled = false));
                    button_SelectFolder.Invoke((MethodInvoker)(() => button_SelectFolder.Enabled = false));
                    foreach (var file in FilesAES)
                    {

                        fullname_saveFile = saveFileDesc.SelectedPath;
                        fullname_saveFile = fullname_saveFile + @"\" + Path.GetFileName(file);
                        fullname_saveFile = fullname_saveFile.Remove(fullname_saveFile.Length - 4);

                        if (myEncrypt.FileDecrypt(file, fullname_saveFile, textBox_password.Text))
                        {
                            i++;
                            progressBar_encryption.Invoke((MethodInvoker)(() => progressBar_encryption.Value = i));
                            label_problem.ForeColor = Color.Green;
                            label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Дешифровка..."));
                            flag = true;
                        }
                        else
                        {
                            i++;
                            progressBar_encryption.Invoke((MethodInvoker)(() => progressBar_encryption.Value = i));
                            label_problem.ForeColor = Color.Red;
                            label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Проблемы с паролем..."));
                            flag = false;
                        }

                    }
                    Thread.Sleep(3000);
                    button_Encryption.Invoke((MethodInvoker)(() => button_Encryption.Enabled = true));
                    button_NonEncryption.Invoke((MethodInvoker)(() => button_NonEncryption.Enabled = true));
                    button_openFile.Invoke((MethodInvoker)(() => button_openFile.Enabled = true));
                    button_SelectFolder.Invoke((MethodInvoker)(() => button_SelectFolder.Enabled = true));
                    i = 0;
                   
                    MyFiles.Clear();
                    progressBar_encryption.Invoke((MethodInvoker)(() => progressBar_encryption.Value = 0));
                    if (flag)
                    {
                        label_problem.ForeColor = Color.Green;
                        label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Дешифровка закончена..."));
                        textBox_password.Invoke((MethodInvoker)(() => textBox_password.Text = ""));
                        listBox_SelectFiles.Invoke((MethodInvoker)(() => listBox_SelectFiles.Items.Clear()));
                        MyFiles.Clear();
                    }
                    else
                    {
                        label_problem.ForeColor = Color.Red;
                        label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Дешифровка неудалась..."));
                        textBox_password.Invoke((MethodInvoker)(() => textBox_password.Text = ""));
                        //  listBox_SelectFiles.Invoke((MethodInvoker)(() => listBox_SelectFiles.Items.Clear()));
                    }


                }

            }
        }

        private void CheckEncryption(List<string> MyFiles)
        {
            // Проверяю выбрал и я файл 
            if (MyFiles == null)
            {
                label_problem.ForeColor = Color.Red;
                label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Такого файла не существует или вы его не выбрали"));
            } // if 
            else
            {
                // Задал ли я пароль
                if (String.IsNullOrEmpty(textBox_password.Text))
                {
                    label_problem.ForeColor = Color.Red;
                    label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Вы не указали пароль"));

                } // if
                else
                {
                    // Проверка пароля регуляркой и длину пароля
                    if (textBox_password.Text.Length < 8)
                    {
                        label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Пароль меньше 8 символов"));
                        label_problem.ForeColor = Color.Red;
                    } // if
                    else
                    {
                        string pattern = @"^((?=.*[a-z])|(?=.*[а-я]))((?=.*[A-Z])|(?=.*[А-Я]))(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S";
                        bool myreg = Regex.IsMatch(textBox_password.Text, pattern);
                        if (myreg)
                        {
                            label_problem.ForeColor = Color.Green;
                            label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Идет процес шифрования ждите..."));
                            progressBar_encryption.Invoke((MethodInvoker)(() => progressBar_encryption.Maximum = MyFiles.Count));
                            progressBar_encryption.Minimum = 0;
                            int i = 0;
                            // Шифруем
                            button_NonEncryption.Invoke((MethodInvoker)(() => button_NonEncryption.Enabled = false));
                            button_openFile.Invoke((MethodInvoker)(() => button_openFile.Enabled = false));
                            button_SelectFolder.Invoke((MethodInvoker)(() => button_SelectFolder.Enabled = false));
                            button_Encryption.Invoke((MethodInvoker)(() => button_Encryption.Enabled = false));
                            foreach (var file in MyFiles)
                            {
                                i++;
                                myEncrypt.FileEncrypt(file, textBox_password.Text);
                                progressBar_encryption.Invoke((MethodInvoker)(() => progressBar_encryption.Value = i));

                                // progressBar_encryption.Value = i;
                            }
                            i = 0;
                            Thread.Sleep(3000);
                            button_Encryption.Invoke((MethodInvoker)(() => button_Encryption.Enabled = true));
                            button_NonEncryption.Invoke((MethodInvoker)(() => button_NonEncryption.Enabled = true));
                            button_openFile.Invoke((MethodInvoker)(() => button_openFile.Enabled = true));
                            button_SelectFolder.Invoke((MethodInvoker)(() => button_SelectFolder.Enabled = true));
                            progressBar_encryption.Invoke((MethodInvoker)(() => progressBar_encryption.Value = 0));
                            label_problem.ForeColor = Color.Green;
                            label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Все удчано, обработанно файлов - " + MyFiles.Count));
                            textBox_password.Invoke((MethodInvoker)(() => textBox_password.Text = ""));
                            listBox_SelectFiles.Invoke((MethodInvoker)(() => listBox_SelectFiles.Items.Clear()));
                            MyFiles.Clear();

                        } //if
                        else
                        {
                            label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Пароль должен иметь - (Хотя бы 1 заглавную букву, цифру, и спец символ)"));
                        }
                    }

                }
            }
        }

        private void button_SelectFolder_Click(object sender, EventArgs e)
        {
            // указать папку для дешифровки файла 
            FolderBrowserDialog saveFileDesc = new FolderBrowserDialog();

            if (saveFileDesc.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            MyFiles = Directory.GetFiles(saveFileDesc.SelectedPath).ToList();

            foreach (var file in MyFiles)
            {
                listBox_SelectFiles.Items.Add(Path.GetFileName(file));

            }

        } // button_SelectFolder_Click

        // openFolder
        FolderBrowserDialog openFolder;
        private void button_HashFile_Click(object sender, EventArgs e)
        {
            openFolder = new FolderBrowserDialog();
             if (openFolder.ShowDialog() == DialogResult.Cancel)
                 return;
             var folder_file_key = openFolder.SelectedPath;

            // Создаю файл 
            FileStream fsCrypt = new FileStream("Key.ka", FileMode.Create);
            myEncrypt.GeneratePassword(openFolder.SelectedPath+@"\Key.ka");
        } // button_HashFile_Click

        private void checkBoxFileKEY_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxFileKEY.Checked )
            {
                textBox_password.Enabled = false;
                checkBoxFileKEY.Text = "Режим: Файл-Ключ";
                checkBoxFileKEY.ForeColor = Color.DarkGreen;
                if (open_key == null)
                {
                    textBox_password.Enabled = false;
                    button_Encryption.Enabled = false;
                    button_NonEncryption.Enabled = false;
                    textBox_password.Enabled = false;
                }
                else
                {
                 
                    button_Encryption.Enabled = true;
                    button_NonEncryption.Enabled = true;
                  
                    
                        
                }
              
               
            }
              
            else
            {
                checkBoxFileKEY.Text = "Режим: Ввод пароля";
                checkBoxFileKEY.ForeColor = Color.DarkGreen;
                textBox_password.Enabled = true;
                button_Encryption.Enabled = true;
                button_NonEncryption.Enabled = true;
            }
               
        }
        private void FileKeyEncrypt()
        {
            if(MyFiles == null)
            {
                label_problem.ForeColor = Color.Red;
                label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Выберите файл для шифрования"));
              
            }
            int i = 0;
            button_Encryption.Invoke((MethodInvoker)(() => button_Encryption.Enabled = false));
            button_NonEncryption.Invoke((MethodInvoker)(() => button_NonEncryption.Enabled = false));
            button_openFile.Invoke((MethodInvoker)(() => button_openFile.Enabled = false));
            button_SelectFolder.Invoke((MethodInvoker)(() => button_SelectFolder.Enabled = false));
            progressBar_encryption.Invoke((MethodInvoker)(() => progressBar_encryption.Maximum = MyFiles.Count));
            label_problem.ForeColor = Color.Green;
            label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Идет процес шифрования ждите..."));
            foreach (var file in MyFiles)
            {
                i++;
                myEncrypt.FileEncrypt(open_key.FileName, file, 1);
                progressBar_encryption.Invoke((MethodInvoker)(() => progressBar_encryption.Value = i));

            }
            i = 0;
            Thread.Sleep(3000);
            progressBar_encryption.Invoke((MethodInvoker)(() => progressBar_encryption.Value = 0));
            label_problem.ForeColor = Color.Green;
            button_Encryption.Invoke((MethodInvoker)(() => button_Encryption.Enabled = true));
            button_NonEncryption.Invoke((MethodInvoker)(() => button_NonEncryption.Enabled = true));
            button_openFile.Invoke((MethodInvoker)(() => button_openFile.Enabled = true));
            button_SelectFolder.Invoke((MethodInvoker)(() => button_SelectFolder.Enabled = true));
            label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Все удчано, обработанно файлов - " + MyFiles.Count));
            textBox_password.Invoke((MethodInvoker)(() => textBox_password.Text = ""));
            listBox_SelectFiles.Invoke((MethodInvoker)(() => listBox_SelectFiles.Items.Clear()));
            MyFiles.Clear();


        }


        private void FileKeyDecrypt()
        {
            // Проверяю выбрал и я файл 
            if (MyFiles == null)
            {
                label_problem.ForeColor = Color.Red;
                label_problem.Text = "Такого файла не существует или вы его не выбрали";
            } // if 
            else
            {
                FilesAES = new List<string>();
                foreach (var file in MyFiles)
                {

                    if (Path.GetExtension(file) == ".aes")
                    {
                        FilesAES.Add(file);
                        // MyFiles.Remove(file);
                    }

                }
                if (FilesAES.Count != 0)
                {
                    int i = 0;
                    progressBar_encryption.Invoke((MethodInvoker)(() => progressBar_encryption.Maximum = FilesAES.Count));
                    progressBar_encryption.Minimum = 0;
                    bool flag = true;
                    button_Encryption.Invoke((MethodInvoker)(() => button_Encryption.Enabled = false));
                    button_NonEncryption.Invoke((MethodInvoker)(() => button_NonEncryption.Enabled = false));
                    button_openFile.Invoke((MethodInvoker)(() => button_openFile.Enabled = false));
                    button_SelectFolder.Invoke((MethodInvoker)(() => button_SelectFolder.Enabled = false));
                    foreach (var file in FilesAES)
                    {

                        fullname_saveFile = saveFileDesc.SelectedPath;
                        fullname_saveFile = fullname_saveFile + @"\" + Path.GetFileName(file);
                        fullname_saveFile = fullname_saveFile.Remove(fullname_saveFile.Length - 4);

                        if (myEncrypt.FileDecrypt(file, fullname_saveFile, open_key.FileName, 1))
                        {
                            i++;
                            progressBar_encryption.Invoke((MethodInvoker)(() => progressBar_encryption.Value = i));
                            label_problem.ForeColor = Color.Green;
                            label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Дешифровка..."));
                            flag = true;
                        }
                        else
                        {
                            i++;
                            progressBar_encryption.Invoke((MethodInvoker)(() => progressBar_encryption.Value = i));
                            label_problem.ForeColor = Color.Red;
                            label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Проблемы с паролем..."));
                            flag = false;
                        }

                    }
                    Thread.Sleep(3000);
                    button_Encryption.Invoke((MethodInvoker)(() => button_Encryption.Enabled = true));
                    button_NonEncryption.Invoke((MethodInvoker)(() => button_NonEncryption.Enabled = true));
                    button_openFile.Invoke((MethodInvoker)(() => button_openFile.Enabled = true));
                    button_SelectFolder.Invoke((MethodInvoker)(() => button_SelectFolder.Enabled = true));
                    i = 0;

                    progressBar_encryption.Invoke((MethodInvoker)(() => progressBar_encryption.Value = 0));
                    if (flag)
                    {
                        label_problem.ForeColor = Color.Green;
                        label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Дешифровка закончена..."));
                        textBox_password.Invoke((MethodInvoker)(() => textBox_password.Text = ""));
                        listBox_SelectFiles.Invoke((MethodInvoker)(() => listBox_SelectFiles.Items.Clear()));
                        MyFiles.Clear();
                    }
                    else
                    {
                        label_problem.ForeColor = Color.Red;
                        label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Дешифровка неудалась..."));
                        textBox_password.Invoke((MethodInvoker)(() => textBox_password.Text = ""));
                        //  listBox_SelectFiles.Invoke((MethodInvoker)(() => listBox_SelectFiles.Items.Clear()));
                    }
                }
                else
                {
                    label_problem.ForeColor = Color.Red;
                    label_problem.Invoke((MethodInvoker)(() => label_problem.Text = "Выберете файл..."));
                }
                    


                

            }
        }

        OpenFileDialog open_key = new OpenFileDialog();
        private void button_searchKEY_Click(object sender, EventArgs e)
        {
            
            open_key.Multiselect = false;
            if (open_key.ShowDialog() == DialogResult.Cancel)
                return;

            if (open_key.FileName != "")
            {
                button_Encryption.Enabled = true;
                button_NonEncryption.Enabled = true;
            }

        } // button_searchKEY_Click
    }
}
