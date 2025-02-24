using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using static System.Net.Mime.MediaTypeNames;

string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
int alphabetSize = alphabet.Length;
int[] GetKeyLetterPositions(string key)
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
string MakeShifText(string key, string my_text, int[] key_letters_pos)
{
	string shif_text = "";
	int cur_pos, let_pos;
	for (int i = 1; i <= key.Length; i++)
	{
		let_pos = Array.IndexOf(key_letters_pos, i);
		cur_pos = let_pos;
		while (cur_pos < my_text.Length)
		{
			shif_text += my_text[cur_pos];
			cur_pos += key.Length;
		}
	}
	return shif_text;
}
string GetStartfText(string key, string shif_text, int[] key_letters_pos)
{
	char[] start_text = new char[shif_text.Length];
	int index = 0;
	int let_pos = 1;
	int cur_pos;
	for (int i = 1; i <= key.Length; i++)
	{
		let_pos = Array.IndexOf(key_letters_pos, i);
		cur_pos = let_pos;
		while (cur_pos < start_text.Length)
		{
			start_text[cur_pos] = shif_text[index++];
			cur_pos += key.Length;
		}
	}
	return new string(start_text);
}

string key = "ГОРОД";
string text = "ШЛАСАША";
int[] key_letters_pos = GetKeyLetterPositions(key);
string shif_text = MakeShifText(key, text, key_letters_pos);

string key2 = "МЫЛО";
int[] key_letters_pos2 = GetKeyLetterPositions(key2);
string shif_text2 = MakeShifText(key2, shif_text, key_letters_pos2);

string start_text2 = GetStartfText(key2, shif_text2, key_letters_pos2);
string start_text = GetStartfText(key, start_text2, key_letters_pos);

Console.WriteLine("Текст: " + text);
Console.WriteLine("Ключ: " + key);
Console.WriteLine("Ключ2: " + key2);
Console.WriteLine("Зашифрованный текст1: " + shif_text);
Console.WriteLine("Зашифрованный текст2: " + shif_text2);
Console.WriteLine("Расшифрованный текст2: " + start_text2);
Console.WriteLine("Расшифрованный текст1: " + start_text);