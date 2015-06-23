using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;






namespace StarJune.Cryptography
{
    /// <summary>
    /// AES : 128 bit Rijndael
    /// </summary>
    public class JuneAES : IDisposable
    {
        private ICryptoTransform Encryptor;
        private ICryptoTransform Decryptor;
        private byte[] Key;
        private byte[] IV;

        #region Constuctor and Dispose

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Ps_Key">Cypher Key</param>
        /// <param name="Ps_IV">Initial Vector</param>
        public JuneAES (string Ps_Key, string Ps_IV)
        {
            if (Ps_Key == null || Ps_Key == string.Empty)
                throw new ArgumentException ("The key can't be null.", "Ps_Key");
            if (Ps_IV == null || Ps_IV == string.Empty)
                throw new ArgumentException ("The initial can't be null.", "Ps_IV");

            this.Key = MakeKey (Ps_Key);
            this.IV = MakeKey (Ps_IV); 


            using (RijndaelManaged M_Rijndael = new RijndaelManaged ()) {
                M_Rijndael.Mode = CipherMode.CBC; // CBC; CFB
                M_Rijndael.Padding = PaddingMode.None; //  .PKCS7;
                M_Rijndael.KeySize = 256; //128;
                M_Rijndael.BlockSize = 256; // 128





                Encryptor = M_Rijndael.CreateEncryptor ();
                Decryptor = M_Rijndael.CreateDecryptor ();
            }
        }

        private byte[] MakeKey (string Ps_Key)
        {
            byte[] tempKey = (new ASCIIEncoding ()).GetBytes (Ps_Key);
            //byte[] tempKey = Encoding.UTF8.GetBytes (Ps_Key);

            (" AES :: MakeKey  " + Ps_Key + "    Length : " + tempKey.Length).HtLog ();

            byte[] Lb_Ret = new byte[32];
            if (tempKey.Length < 16) {
                int Li_Count = 0;
                while (true) {
                    if (Li_Count > 15) {
                        Ag.LogIntenseWord ("  AES  ::   Li Count > 15  >>>>  break;    ");
                        break;
                    }
                    for (int i = 0; i < tempKey.Length; i++) {
                        if (Li_Count > 15)
                            break;
                        Lb_Ret [Li_Count++] = tempKey [i];
                    }
                }
            } else {
                for (int i = 0; i < 32; i++) {
                    Lb_Ret [i] = tempKey [i];
                }
            }
            return Lb_Ret;
        }

        /// <summary>
        /// Dispose Objct
        /// </summary>
        public void Dispose ()
        {
            if (Encryptor != null) {
                Encryptor.Dispose ();
                Encryptor = null;
            }

            if (Decryptor != null) {
                Decryptor.Dispose ();
                Decryptor = null;
            }
        }

        #endregion

        #region Encrypt & Decrypt Method

        public string EncryptToHex (string Ps_PlainText)
        {
            Ag.LogStartWithStr (1, "  AES :: EncryptToHex  >>>    " + Ps_PlainText);
            return BytesToHex (EncryptToBytes (Ps_PlainText));
        }

        public string DecryptFromHex (string Ps_HexString)
        {
            return DecryptFromBytes (HexToBytes (Ps_HexString));
        }

        public byte[] EncryptToBytes (string Ps_PlainText)
        {
            Ag.LogString ("  AES :: EncryptToBytes    :: " + Ps_PlainText);
            if (Ps_PlainText == null || Ps_PlainText.Length <= 0)
                throw new ArgumentNullException ("plainText");
            
            byte[] Lb_EncryptedBytes;

            using (MemoryStream ms = new MemoryStream ()) {
                using (CryptoStream cs = new CryptoStream (ms, Encryptor, CryptoStreamMode.Write)) {
                    using (StreamWriter sw = new StreamWriter (cs)) {
                        sw.Write (Ps_PlainText);
                    }
                    Lb_EncryptedBytes = ms.ToArray ();
                }
            }

            return Lb_EncryptedBytes;
        }

        public string DecryptFromBytes (byte[] Pb_CipherBytes)
        {
            if (Pb_CipherBytes == null || Pb_CipherBytes.Length <= 0)
                throw new ArgumentNullException ("cipherText");
 
            string Ls_PlainText = null;

            using (MemoryStream ms = new MemoryStream (Pb_CipherBytes)) {
                using (CryptoStream cs = new CryptoStream (ms, Decryptor, CryptoStreamMode.Read)) {
                    using (StreamReader sr = new StreamReader (cs)) {
                        Ls_PlainText = sr.ReadToEnd ();
                    }
                }
            }
            return Ls_PlainText;
        }

        #endregion

        #region Internal Method

        private string BytesToHex (byte[] P_Bytes)
        {
            Ag.LogString ("  BytesToHex :: ");
            StringBuilder SB = new StringBuilder ();
            for (int i = 0; i < P_Bytes.Length / 4; i++) {
                Ag.LogString ("  BytesToHex :: " + i);

                int iBase10 = BitConverter.ToInt32 (P_Bytes, i * 4);
                string strHex = string.Format ("{0:X4}", iBase10);
                SB.Append (strHex);

                Ag.LogString ("  BytesToHex :: " + i + "   " + iBase10 + "  >>   " + strHex + "    Builder  " + SB.ToString ());

            }
            return SB.ToString ();
        }

        private byte[] HexToBytes (string P_Hex)
        {
            Byte[] Lb_Ret = new Byte[P_Hex.Length / 2];
            for (int i = 0; i < P_Hex.Length / 8; i++) {
                int iBase10 = Convert.ToInt32 (P_Hex.Substring (i * 8, 8), 16);
                Byte[] Lb_Temp = BitConverter.GetBytes (iBase10);
                for (int j = 0; j < 4; j++) {
                    Lb_Ret [i * 4 + j] = Lb_Temp [j];
                }
            }
            return Lb_Ret;
        }

        #endregion

    }
}
