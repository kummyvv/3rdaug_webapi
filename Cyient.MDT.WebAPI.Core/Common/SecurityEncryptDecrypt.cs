using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Cyient.MDT.WebAPI.Core.Common
{
    public class SecurityEncryptDecrypt
    {


        public static string Encrypt(string InputText)
        {

            MemoryStream memoryStream = null;
            CryptoStream cryptoStream = null;
            string KeyString = "MDT26072018CON";
            try
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 128;
                    AES.BlockSize = 128;
                    byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);
                    PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(KeyString,Encoding.ASCII.GetBytes(KeyString.Length.ToString()));
                    using (ICryptoTransform Encryptor = AES.CreateEncryptor(SecretKey.GetBytes(16),SecretKey.GetBytes(16)))
                    {
                        using (memoryStream = new MemoryStream())
                        {
                            using (cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(PlainText, 0, PlainText.Length);
                                cryptoStream.FlushFinalBlock();
                                return Convert.ToBase64String(memoryStream.ToArray());
                            }
                        }
                    }
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                if (memoryStream != null)
                    memoryStream.Close();
                if (cryptoStream != null)
                    cryptoStream.Close();
            }
        }

        public static string Decrypt(string InputText)
        {
            MemoryStream memoryStream = null;
            CryptoStream cryptoStream = null;
            string KeyString = "MDT26072018CON";
            try
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 128;
                    AES.BlockSize = 128;
                    byte[] EncryptedData = Convert.FromBase64String(InputText);
                    PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(KeyString,Encoding.ASCII.GetBytes(KeyString.Length.ToString()));
                    using (ICryptoTransform Decryptor = AES.CreateDecryptor(SecretKey.GetBytes(16),SecretKey.GetBytes(16)))
                    {
                        using (memoryStream = new MemoryStream(EncryptedData))
                        {
                            using (cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read))
                            {
                                byte[] PlainText = new byte[EncryptedData.Length];
                                return Encoding.Unicode.GetString(PlainText, 0, cryptoStream.Read(PlainText, 0,PlainText.Length));
                            }
                        }
                    }
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                if (memoryStream != null)
                    memoryStream.Close();
                if (cryptoStream != null)
                    cryptoStream.Close();
            }
        }

        //public static string Encrypt(string clearText)
        //{
        //    string EncryptionKey = "MDT26072018CON";
        //    byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(clearBytes, 0, clearBytes.Length);
        //                cs.Close();
        //            }
        //            clearText = Convert.ToBase64String(ms.ToArray());
        //        }
        //    }
        //    return clearText;
        //}

        ///// <summary>
        ///// Decrypt the 
        ///// </summary>
        ///// <param name="cipherText"></param>
        ///// <returns></returns>
        //public static string Decrypt(string cipherText)
        //{
        //    string EncryptionKey = "MDT26072018CON";
        //    byte[] cipherBytes = Convert.FromBase64String(cipherText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(cipherBytes, 0, cipherBytes.Length);
        //                cs.Close();
        //            }
        //            cipherText = Encoding.Unicode.GetString(ms.ToArray());
        //        }
        //    }
        //    return cipherText;
        //}
    }
}
