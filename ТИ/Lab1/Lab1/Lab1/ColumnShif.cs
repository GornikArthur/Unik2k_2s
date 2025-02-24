using System;
using System.Linq;

namespace Lab1
{
	internal class ColumnShif
	{
		private static string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
		private int alphabetSize = alphabet.Length;
		private string key1, key2, text;
		private int[] key_letters_pos, key_letters_pos2;

		public ColumnShif(string key1, string key2, string text)
		{
			this.key1 = key1.ToUpper();
			this.key2 = key2.ToUpper();
			this.text = text.ToUpper();
			this.key_letters_pos = GetKeyLetterPositions(this.key1);
			this.key_letters_pos2 = GetKeyLetterPositions(this.key2);
		}

		public string MakeShif()
		{
			string shif_text = MakeShifText(this.key1, this.text, this.key_letters_pos);
			return MakeShifText(this.key2, shif_text, this.key_letters_pos2);
		}

		public string GetStart()
		{
			string start_text2 = GetStartfText(this.key2, this.text, this.key_letters_pos2);
			return GetStartfText(this.key1, start_text2, this.key_letters_pos);
		}

		private int[] GetKeyLetterPositions(string key)
		{
			int[] key_letters_pos = new int[key.Length];
			int let_pos = 1;

			for (int let = 0; let < alphabetSize; let++)
			{
				for (int pos = 0; pos < key.Length; pos++)
				{
					if (alphabet[let] == key[pos])
					{
						key_letters_pos[pos] = let_pos++;
					}
				}
			}
			return key_letters_pos;
		}

		private string MakeShifText(string key, string my_text, int[] key_letters_pos)
		{
			string shif_text = "";
			int cur_pos, let_pos;
			for (int i = 1; i <= key.Length; i++)
			{
				let_pos = Array.IndexOf(key_letters_pos, i);
				if (let_pos == -1) continue; // защита от ошибки
				cur_pos = let_pos;
				while (cur_pos < my_text.Length)
				{
					shif_text += my_text[cur_pos];
					cur_pos += key.Length;
				}
			}
			return shif_text;
		}

		private string GetStartfText(string key, string shif_text, int[] key_letters_pos)
		{
			char[] start_text = new char[shif_text.Length];
			int index = 0;
			int let_pos = 1;
			int cur_pos;
			for (int i = 1; i <= key.Length; i++)
			{
				let_pos = Array.IndexOf(key_letters_pos, i);
				if (let_pos == -1) continue; // защита от ошибки
				cur_pos = let_pos;
				while (cur_pos < start_text.Length)
				{
					start_text[cur_pos] = shif_text[index++];
					cur_pos += key.Length;
				}
			}
			return new string(start_text);
		}
	}
}
