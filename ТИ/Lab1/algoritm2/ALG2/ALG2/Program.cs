using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Linq;

string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
int alphabetSize = alphabet.Length;

string MakeKey(string key, int len)
{
	StringBuilder extendedKey = new StringBuilder(key);
	int start_len = key.Length;
	for (int i = 0; i < len - start_len; i++)
	{
		extendedKey.Append(key[i % start_len]);
	}
	return extendedKey.ToString();
}

string ExtendKey(string keySeed, string text)
{
	StringBuilder key = new StringBuilder(keySeed);
	for (int i = 0; key.Length < text.Length; i++)
	{
		key.Append(text[i]);
	}
	return key.ToString();
}

string MakeShifText(string key, string text)
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

string GetStartfText(string key, string shif_text)
{
	StringBuilder start_text = new StringBuilder();
	for (int i = 0; i < shif_text.Length; i++)
	{
		int textPos = alphabet.IndexOf(shif_text[i]);
		int keyPos = alphabet.IndexOf(key[i]);

		if (textPos == -1 || keyPos == -1) continue;

		int decryptedPos = (textPos - keyPos + alphabet.Length) % alphabet.Length;
		start_text.Append(alphabet[decryptedPos]);
	}
	return start_text.ToString();
}

string text = "ЁЖИКВПОЛЕ";
string key = ExtendKey("ВОЛК", text);
string shif_text = MakeShifText(key, text);
string start_text = GetStartfText(key, shif_text);

Console.WriteLine("Текст: " + text);
Console.WriteLine("Ключ: " + key);
Console.WriteLine("Зашифрованный текст: " + shif_text);

Console.WriteLine("Расшифрованный текст: " + start_text);

