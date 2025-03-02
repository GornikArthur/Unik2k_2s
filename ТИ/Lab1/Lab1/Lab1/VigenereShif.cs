using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab1
{
	internal class VigenereShif
	{
		private static string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
		private int alphabetSize = alphabet.Length;
		private string key, text;
		public VigenereShif(string key, string text)
		{
			this.text = text;
			this.key = ExtendKey(key, this.text);
			//this.key = MakeKey(key, this.text.Length);
		}
		public string MakeShif()
		{
			return MakeShifText(this.key, this.text);
		}
		public string GetStart()
		{
			return GetStartfText(this.key, this.text);
		}
		private string MakeKey(string key, int len)
		{
			StringBuilder extendedKey = new StringBuilder(key);
			int start_len = key.Length;
			for (int i = 0; i < len - start_len; i++)
			{
				extendedKey.Append(key[i % start_len]);
			}
			return extendedKey.ToString();
		}
		private string ExtendKey(string keySeed, string text)
		{
			StringBuilder key = new StringBuilder(keySeed);
			for (int i = 0; key.Length < text.Length; i++)
			{
				key.Append(text[i]);
			}
			return key.ToString();
		}
		private string MakeShifText(string key, string text)
		{
			StringBuilder shif_text = new StringBuilder();
			for (int i = 0; i < text.Length; i++)
			{
				int textPos = alphabet.IndexOf(text[i]);
				int keyPos = alphabet.IndexOf(key[i]);

				if (textPos == -1 || keyPos == -1) continue;

				int encryptedPos = (textPos + keyPos) % alphabetSize;
				shif_text.Append(alphabet[encryptedPos]);
			}
			return shif_text.ToString();
		}
		private string GetStartfText(string key, string shif_text)
		{
			StringBuilder start_text = new StringBuilder();
			for (int i = 0; i < shif_text.Length; i++)
			{
				int textPos = alphabet.IndexOf(shif_text[i]);
				int keyPos = alphabet.IndexOf(key[i]);

				if (textPos == -1 || keyPos == -1) continue;

				int decryptedPos = (textPos - keyPos + alphabetSize) % alphabetSize;
				start_text.Append(alphabet[decryptedPos]);
			}
			return start_text.ToString();
		}
		public string GetKey()
		{
			return this.key; 
		}
	}
}
