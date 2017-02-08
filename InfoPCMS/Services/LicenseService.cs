using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Text;
using System.Security.Cryptography;

namespace InfoPCMS.Services
{
    class LicenseService
    {

        private byte[] key = {
        
    };
        private byte[] IV = {
        0x12,
        0x34,
        0x56,
        0x78,
        0x90,
        0xab,
        0xcd,
        0xef
    };

        private const string EncryptionKey = "abcdefgh";

        public string Decrypt(string stringToDecrypt)
        {
            try
            {
                byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
                key = System.Text.Encoding.UTF8.GetBytes(EncryptionKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                //oops - add your exception logic
                return null;
            }
        }
        public string Encrypt(string stringToEncrypt)
        {
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(EncryptionKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return null;
                //oops - add your exception logic
            }
        }

        private char[] Left(string EncryptionKey, int p2)
        {
            throw new NotImplementedException();
        }


        public String getCurrentKey() {


            String currentkey = null;

            try
            {
                DataTable dt = InfoPCMS.db.executeSelectQuery("select Value from Company where Id = '1'");
                if(dt.Rows.Count>0){

                    currentkey = dt.Rows[0]["Value"].ToString();
                
                }
                return currentkey;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        
        }


    }
}
