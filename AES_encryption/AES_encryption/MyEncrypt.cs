using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace AES_encryption
{
    class MyEncrypt
    {

        // Генератор соли 
        public static byte[] GenerateRandomSalt()
        {
            // Создаю пустой массив байтов 
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    // Заполняю массив солью
                    rng.GetBytes(data);
                }
            }

            return data;
        } // GenerateRandomSalt


        public void FileEncrypt(string inputFile, string password)
        {
           // Делаем соль 
            byte[] salt = GenerateRandomSalt();

            // Создаю файл с расширением .aes
            FileStream fsCrypt = new FileStream(inputFile + ".aes", FileMode.Create);

            // Преобразую пароль в массив байтов
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            // Объект класса который реализует блочное шифрование (размеерр блока 128 бит)
            RijndaelManaged AES = new RijndaelManaged();

            // Размер ключа в байтах
            AES.KeySize = 256;
            
            // Размер блока
            AES.BlockSize = 128;

            // Режим заполнения блока
            AES.Padding = PaddingMode.PKCS7;

           // Формируем свой ключ
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            
            // Вектор инициализации (секретное число) будет использваться вместе с ключом
            AES.IV = key.GetBytes(AES.BlockSize / 8);

            AES.Mode = CipherMode.CFB;

            // Записываем байты в файл (массив байтов который записываем, смещение, максимальное кол-во байтов для записи)
            fsCrypt.Write(salt, 0, salt.Length);

            // 
            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);


            // Открыть файл начальный файл
            FileStream fsIn = new FileStream(inputFile, FileMode.Open);

            byte[] buffer = new byte[1048576];
            int read;

            try
            {
                
             
                // Пока не дочитаем файл до конца
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {

                    Console.WriteLine(read);
                    Application.DoEvents();
                 
                  
                    // записывваем 
                    cs.Write(buffer, 0, read);
                   
                }
                Thread.Sleep(3000);
                // Закрываем  файл 
                fsIn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cs.Close();
                fsCrypt.Close();
            }
        } // FileEncrypt


        public bool FileDecrypt(string inputFile, string outputFile, string password)
        {

            // Делаем все то же самое
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[32];

            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
            fsCrypt.Read(salt, 0, salt.Length);

            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
           
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CFB;
            //var ppp = key.CryptDeriveKey("TripleDES", "SHA256", AES.KeySize, AES.IV);
            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);

            // Создаем новый файл в который будем расшифровывать 
            FileStream fsOut = new FileStream(outputFile, FileMode.Create);
        
      
            int read;
            byte[] buffer = new byte[1048576];

            try
            {
                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Application.DoEvents();
                    fsOut.Write(buffer, 0, read);
                    
                }
               
            }
    
            catch (Exception ex)
            {
                return false;
                Console.WriteLine("Error: " + ex.Message);
            }

            try
            {
                cs.Close();
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error by closing CryptoStream: " + ex.Message);
                return false;
            }
            finally
            {
                fsOut.Close();
                fsCrypt.Close();
            }
            return true;
        } // FileDecrypt



    }
    }
