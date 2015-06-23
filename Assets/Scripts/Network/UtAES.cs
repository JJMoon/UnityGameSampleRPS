using UnityEngine;
using System;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using SimpleJSON;

public class UTAES
{
    static byte[] joycity_key = {
        0x47, 0xda, 0xd3, 0x45, 0xa6, 0x47, 0x5f, 0x37, 0x80, 0x71, 0xae, 0x13, 0x63, 0x47, 0x02, 0x7d
    };
    static byte[] joycity_iv = {
        0xab, 0x39, 0xf2, 0xda, 0xfa, 0x2e, 0xfe, 0x25, 0x73, 0x4b, 0x29, 0x55, 0xd4, 0xdc, 0x65, 0x56
    };

    static byte[] PrivateKEY = {
        0x77, 0x4a, 0x53, 0x45, 0x76, 0x4d, 0x2a, 0x89, 0x99, 0xab, 0xa7, 0xb3, 0xd3, 0x28, 0x92, 0x2d
    };
    static byte[] PrivateIV = {
        0xa8, 0xd9, 0x42, 0x2a, 0x4a, 0x29, 0x69, 0x77, 0x83, 0x74, 0xd9, 0x55, 0xd4, 0xdc, 0xd5, 0x36
    };

    public static string AESDecrypt128 (string Input, bool isPrivate = false)
    {
        // Unity에서 .net subset 을 사용하는 경우, 아래의 AesCryptoServiceProvider() 대신 AesManaged() 를 사용합니다.
        //using (var aes = new AesCryptoServiceProvider ()) {
        using (var aes = new AesManaged ()) {
            aes.KeySize = 128;            
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            if (isPrivate) {
                aes.Key = PrivateKEY;
                aes.IV = PrivateIV;
            } else {
                aes.Key = joycity_key; // ("47dad345a6475f378071ae136347027d");
                aes.IV = joycity_iv; // ("ab39f2dafa2efe25734b2955d4dc6556");
            }

            // Create a decrytor to perform the stream transform.
            ICryptoTransform decryptor = aes.CreateDecryptor (aes.Key, aes.IV);
            //System.Text.UnicodeEncoding unicodeEncoding = new UnicodeEncoding ();

            byte[] bytes = Input.StringToByteArray (); // Encoding.ASCII.GetBytes (Input);
            //bytes.ShowEachBytes (" Utf8 ");

            using (MemoryStream msDecrypt = new MemoryStream (bytes)) {
                using (CryptoStream csDecrypt = new CryptoStream (msDecrypt, decryptor, CryptoStreamMode.Read)) {
                    using (StreamReader srDecrypt = new StreamReader (csDecrypt)) {
                        return srDecrypt.ReadToEnd ();
                        // System.Convert.FromBase64String
                    }
                }
            }
        }
    }

    public static string AESEncrypt128 (string plainText, bool isPrivate = false)
    {
        byte[] encrypted;
        using (AesManaged aes = new AesManaged ()) {
            if (isPrivate) {
                aes.Key = PrivateKEY;
                aes.IV = PrivateIV;
            } else {
                aes.Key = joycity_key; // ("47dad345a6475f378071ae136347027d");
                aes.IV = joycity_iv; // ("ab39f2dafa2efe25734b2955d4dc6556");
            }

            // Create a decrytor to perform the stream transform.
            ICryptoTransform encryptor = aes.CreateEncryptor (aes.Key, aes.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream ()) {
                using (CryptoStream csEncrypt = new CryptoStream (msEncrypt, encryptor, CryptoStreamMode.Write)) {
                    using (StreamWriter swEncrypt = new StreamWriter (csEncrypt)) {
                        //Write all data to the stream.
                        swEncrypt.Write (plainText);
                    }
                    encrypted = msEncrypt.ToArray ();
                }
            }
        }

        string hex = BitConverter.ToString (encrypted);
        return hex.Replace ("-", "");
    }
}
